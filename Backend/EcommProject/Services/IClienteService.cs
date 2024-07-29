using EcommProject.Dtos;

namespace EcommProject.Services
{
    public interface IClienteService
    {
        Task AddCliente(ClienteDTO clienteDTO);
        Task<List<ClienteDTO>> GetAllClientes();
        Task<ClienteDTO> GetCliente(int id);
    }
}
