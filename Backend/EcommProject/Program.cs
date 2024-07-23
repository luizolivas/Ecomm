using AutoMapper;
using EcommProject.Context;
using EcommProject.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using SharedSettings;
using System;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();
builder.Services.AddSingleton(rabbitMqSettings);

var factory = new ConnectionFactory()
{
    HostName = rabbitMqSettings.HostName,
    UserName = rabbitMqSettings.UserName,
    Password = rabbitMqSettings.Password
};
builder.Services.AddSingleton(factory);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IRabbitService, RabbitService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
