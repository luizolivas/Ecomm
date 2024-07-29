using AutoMapper;
using EcommProject.Context;
using EcommProject.Dtos;
using EcommProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommProject.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClienteService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddCliente(ClienteDTO clienteDTO)
        {
            Cliente cliente = new Cliente();
            cliente = _mapper.Map<Cliente>(clienteDTO);

            if(cliente != null)
            {
                await _context.AddAsync(cliente);
                await _context.SaveChangesAsync();
            }         
        }

        public async Task<List<ClienteDTO>> GetAllClientes()
        {
            List<Cliente> clientes = await _context.Clientes.ToListAsync();

            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        public async Task<ClienteDTO> GetCliente(int id)
        {
            Cliente cliente = await _context.Clientes.FindAsync(id);

            if(cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            return _mapper.Map<ClienteDTO>(cliente);
        }
    }
}
