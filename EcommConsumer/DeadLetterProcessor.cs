using EcommConsumer.Context;
using EcommProject.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcommConsumer
{
    public class DeadLetterProcessor : BackgroundService
    {
        private readonly ILogger<DeadLetterProcessor> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly IModel _channel;


        public DeadLetterProcessor(ILogger<DeadLetterProcessor> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;


            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"Received message: {message}");

                try
                {
                    await ReprocessMessageAsync(message);
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing message: {ex.Message}");
                    _channel.BasicNack(ea.DeliveryTag, false, false);
                }

            };

            _channel.BasicConsume(queue: "dead_letter_queue",
                                 autoAck: false,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task ReprocessMessageAsync(string message)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                //Simular uma falha para criar uma dead-letter 
                Random randNum = new Random();
                //int testeFalha = 5;

                if (randNum.Next(10) == 5)
                {
                    throw new Exception("Simulated processing failure");
                }

                var pedido = JsonSerializer.Deserialize<Pedido>(message);
                var dbPedido = await dbContext.Pedidos.FindAsync(pedido.Id);

                if (dbPedido != null)
                {
                    dbPedido.Processado = true;
                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    _logger.LogInformation($"Order {pedido.Id} marked as processed.");
                }
                else
                {
                    _logger.LogWarning($"Order {pedido.Id} not found in the database.");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing message: {ex.Message}");
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
