using RabbitMQ.Client;
using System.Text;

namespace EcommProject.Services
{
    public class RabbitService : IRabbitService
    {
        private readonly ConnectionFactory _factory;

        public RabbitService(ConnectionFactory factory)
        {
            _factory = factory;
        }

        public void EnviarMensagem(string mensagem)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();


            channel.QueueDeclare(queue: "dead_letter_queue",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);


            var args = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", "" }, 
                { "x-dead-letter-routing-key", "dead_letter_queue" }
            };

            channel.QueueDeclare(queue: "pedidos",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: args);

            var body = Encoding.UTF8.GetBytes(mensagem);


            channel.BasicPublish(exchange: "",
                                 routingKey: "pedidos",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
