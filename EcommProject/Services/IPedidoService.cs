using EcommProject.Dtos;

namespace EcommProject.Services
{
    public interface IPedidoService
    {
        Task AdicionaPedido(PedidoDto pedidoDto);
        Task ProcessaPedido(PedidoDto pedidoDto);
    }
}
