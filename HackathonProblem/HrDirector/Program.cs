using HrDirector.Controllers;
using HrDirector.Models.Data;
using Microsoft.EntityFrameworkCore;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Установка строки подключения к базе данных
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<HrDirectorConsumer>();
    
    x.UsingRabbitMq((context, cfg) =>
    {
        
        var rabbitMqUser = builder.Configuration["RabbitMQ:Username"];
        var rabbitMqPass = builder.Configuration["RabbitMQ:Password"];
        var rabbitMqHost = builder.Configuration["RabbitMQ:Host"];
        cfg.Host(rabbitMqHost, "/", h =>
        {
            h.Username(rabbitMqUser);
            h.Password(rabbitMqPass);
        });

        cfg.ConfigureEndpoints(context);
    });
});


// Создание приложения
var app = builder.Build();

// Запуск приложения
app.Run();