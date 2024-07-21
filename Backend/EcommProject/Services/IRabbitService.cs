using RabbitMQ.Client;

namespace EcommProject.Services
{
    public interface IRabbitService
    {
        public void EnviarMensagem(string mensagem);
    }
}
