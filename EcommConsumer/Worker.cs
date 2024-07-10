using EcommProject.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Text;
using System.Text.Json;

namespace EcommConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IHttpClientFactory _clientFactory;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _clientFactory = clientFactory;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        private void ConfigureRabbitMq()
        {
            _channel.QueueDeclare(queue: "dead_letter_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var args = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", "" }, 
                { "x-dead-letter-routing-key", "dead_letter_queue" }
            };

            _channel.QueueDeclare(queue: "pedidos",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: args);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                //_logger.LogInformation($"Received message: {message}");


                await ProcessMessageAsync(message);
            };

            _channel.BasicConsume(queue: "pedidos",
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(string message)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            string url = client.BaseAddress.ToString();
            StringContent content = new StringContent(message, System.Text.Encoding.UTF8, "application/json");


            using (var response = await client.PostAsync(url, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    string ok = "OK";
                }
                else
                {
                    string ok = "NOT OK";
                }
            }
            
        }
    }
}
