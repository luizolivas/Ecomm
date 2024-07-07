using AutoMapper;
using EcommProject.Context;
using EcommProject.Dtos;
using EcommProject.Models;

namespace EcommProject.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PedidoService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
        }

        public async Task AdicionaPedido(PedidoDto pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);
            await _context.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
