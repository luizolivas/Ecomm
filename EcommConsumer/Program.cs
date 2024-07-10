using EcommConsumer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddHttpClient("ProductApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:PedidoApi"]);
});

builder.Services.AddHttpClient();

var host = builder.Build();
host.Run();
