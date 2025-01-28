using System.Text;
using earfest.API.Behaviours;
using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Interceptors;
using earfest.API.Features.Categories;
using earfest.API.Helpers;
using earfest.API.Middlewares;
using earfest.API.Services;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddSingleton<AuditInterceptor>();

builder.Services.AddDbContext<EarfestDbContext>((sp,options) =>
{
    var interceptor = sp.GetService<AuditInterceptor>()!;
    options.UseNpgsql(builder.Configuration.GetConnectionString("EarfestDbContext"))
    .AddInterceptors(interceptor); 
});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });



builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateCategory.CommandHandler).Assembly);
    //cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
});

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser,CurrentUser>();



builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
//builder.Services.AddValidatorsFromAssembly(typeof(CategoryCreate.CommandValidator).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<Program>();



string logdbConnectionString=builder.Configuration.GetConnectionString("LogDbConnection"); // Bu database'i kendimiz oluşturuyoruz.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Seq("http://localhost:5341/") // UI için geçerli adres 5341 Seq'in default adresidir. Seq kullanmadan önce docker'da ayağa kaldırmamız gerekir.
    .WriteTo.MSSqlServer(logdbConnectionString, tableName: "Logs", autoCreateSqlTable: true)
    .WriteTo.File("logs/myBeatifulLog-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();



builder.Services.AddMassTransit(x => 
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:ConnectionString"], h =>
        {
            h.Username(builder.Configuration["RabbitMQ:UserName"]!);
            h.Password(builder.Configuration["RabbitMQ:Password"]!);
        });
    });

});


var app = builder.Build();

app.UseStaticFiles();

app.UseGlobalExceptionMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
