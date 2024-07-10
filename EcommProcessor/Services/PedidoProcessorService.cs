using EcommProject.Dtos;
using EcommProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommProcessor.Services
{
    public class PedidoProcessorService
    {
        public async Task ProcessaPedido(PedidoDto pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);
            pedido.Processado = true;
            _context.Update(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
