using earfest.API.Behaviours;
using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Domain.Interceptors;
using earfest.API.Features.Categories;
using earfest.API.Helpers;
using earfest.API.Middlewares;
using earfest.API.Models;
using earfest.API.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddSingleton<AuditInterceptor>();

builder.Services.AddDbContext<EarfestDbContext>((sp,options) =>
{
    var interceptor = sp.GetService<AuditInterceptor>()!;
    options.UseNpgsql(builder.Configuration.GetConnectionString("EarfestContext"))
    .AddInterceptors(interceptor); 
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<EarfestDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

var tokenOptions = builder.Configuration.GetSection("AppTokenOptions").Get<AppTokenOptions>();
builder.Services.Configure<AppTokenOptions>(builder.Configuration.GetSection("AppTokenOptions"));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions!.Issuer,
        ValidAudiences = tokenOptions.Audiences,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
    };
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateCategory.CommandHandler).Assembly);
    //cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailService, EmailService>();

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


var app = builder.Build();
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
