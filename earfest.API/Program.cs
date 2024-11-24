using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Features.Categories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using earfest.API.Behaviours;
using earfest.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<EarfestDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EarfestContext")));

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

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateCategory.CommandHandler).Assembly);
    //cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
});


builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
//builder.Services.AddValidatorsFromAssembly(typeof(CategoryCreate.CommandValidator).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();
app.UseGlobalExceptionMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
