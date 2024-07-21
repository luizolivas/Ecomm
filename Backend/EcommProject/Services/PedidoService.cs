using AutoMapper;
using EcommProject.Context;
using EcommProject.Dtos;
using EcommProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EcommProject.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        private readonly IRabbitService _rabbitService;

        public PedidoService(ApplicationDbContext applicationDbContext, IMapper mapper, IRabbitService rabbitService)
        {
            _context = applicationDbContext;
            _mapper = mapper;
            _rabbitService = rabbitService;
        }

        public async Task AdicionaPedido(PedidoDto pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);
            await _context.AddAsync(pedido);
            await _context.SaveChangesAsync();

            var mensagem = JsonSerializer.Serialize(pedido);
            _rabbitService.EnviarMensagem(mensagem);
        }

        public async Task<List<PedidoDto>> GetAllPedidos()
        {
            List<Pedido> pedidos = await _context.Pedidos.ToListAsync();

            return _mapper.Map<List<PedidoDto>>(pedidos);
        }
    }
}
