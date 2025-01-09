using MassTransit;
using Notification.Service;
using Notification.Service.Consumers;
using Notification.Service.Notifications;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        // Configure EmailSettings from appsettings.json
        services.Configure<EmailSettings>(hostContext.Configuration.GetSection("EmailSettings"));

        // Register EmailService
        services.AddScoped<IEmailService, EmailService>();

        // Register the Worker Service
        services.AddHostedService<Worker>();

        // Add MassTransit configuration
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ConfirmEmailEventConsumer>();
            x.AddConsumer<ForgotPasswordEventConsumer>();
            x.AddConsumer<PasswordChangedEmailEventConsumer>();
            x.AddConsumer<SubscribedEventConsumer>();
            x.AddConsumer<UnSubscribedEventConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqConfig = hostContext.Configuration.GetSection("RabbitMQ");
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

                cfg.ReceiveEndpoint("send-subscribed-email-queue", e =>
                {
                    e.ConfigureConsumer<SubscribedEventConsumer>(context);
                });
                cfg.ReceiveEndpoint("send-unsubscribed-email-queue", e =>
                {
                    e.ConfigureConsumer<UnSubscribedEventConsumer>(context);
                });
            });
        });
    })
    .Build();

// Run the host
await host.RunAsync();
