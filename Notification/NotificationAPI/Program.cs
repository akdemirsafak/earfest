using MassTransit;
using NotificationAPI.Consumers;
using NotificationAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



// Configure EmailSettings from appsettings.json
builder.Services.Configure<NotificationAPI.Services.EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Register EmailService
builder.Services.AddScoped<IEmailService, EmailService>();

// Register the Worker Service

// Add MassTransit configuration
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ConfirmEmailEventConsumer>();
    x.AddConsumer<ForgotPasswordEventConsumer>();
    x.AddConsumer<PasswordChangedEmailEventConsumer>();
    x.AddConsumer<SubscribedEventConsumer>();
    x.AddConsumer<UnSubscribedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQ");
        var connectionString = rabbitMqConfig["ConnectionString"];
        var username = rabbitMqConfig["UserName"];
        var password = rabbitMqConfig["Password"];

        if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new InvalidOperationException("RabbitMQ configuration is missing or invalid.");
        }

        cfg.Host(connectionString, h =>
        {
            h.Username(username);
            h.Password(password);
        });

        cfg.ReceiveEndpoint("send-confirm-email-queue", e =>
        {
            e.ConfigureConsumer<ConfirmEmailEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("forgot-password-email-queue", e =>
        {
            e.ConfigureConsumer<ForgotPasswordEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("password-changed-email-queue", e =>
        {
            e.ConfigureConsumer<PasswordChangedEmailEventConsumer>(context);
        });

        cfg.ReceiveEndpoint("send-subscription-email-queue", e =>
        {
            e.ConfigureConsumer<SubscribedEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("send-unsubscription-email-queue", e =>
        {
            e.ConfigureConsumer<UnSubscribedEventConsumer>(context);
        });
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
