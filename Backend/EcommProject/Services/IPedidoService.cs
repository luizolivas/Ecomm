using EcommProject.Dtos;

namespace EcommProject.Services
{
    public interface IPedidoService
    {
        Task AdicionaPedido(PedidoDto pedidoDto);
        Task<List<PedidoDto>> GetAllPedidos();
        Task<List<PedidoResponseDto>> GetPedidosResponse();
    }
}
