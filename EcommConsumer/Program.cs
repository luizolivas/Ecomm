using EcommConsumer;
using EcommConsumer.Context;
using EcommProject.Context;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using SharedSettings;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();
builder.Services.AddSingleton(rabbitMqSettings);

var factory = new ConnectionFactory()
{
    HostName = rabbitMqSettings.HostName,
    UserName = rabbitMqSettings.UserName,
    Password = rabbitMqSettings.Password
};
builder.Services.AddSingleton(factory);

builder.Services.AddHttpClient();

var host = builder.Build();
host.Run();
