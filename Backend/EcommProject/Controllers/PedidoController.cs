using EcommProject.Dtos;
using EcommProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<ActionResult> AdicionaPedido(PedidoDto pedidoDto)
        {
            try
            {
                await _pedidoService.AdicionaPedido(pedidoDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao adicionar Pedido: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPedidos()
        {
            try
            {
                List<PedidoDto> pedidoDtos = await _pedidoService.GetAllPedidos();
                return Ok(pedidoDtos);
            }
            catch (Exception ex)
            {

                return BadRequest($"Falha ao buscar Pedidos: {ex.Message}");
            }

        }

        [HttpGet("/api/pedidoresponse")]
        public async Task<ActionResult> GetPedidosFront()
        {
            try
            {
                List<PedidoResponseDto> pedidoDtos = await _pedidoService.GetPedidosResponse();
                return Ok(pedidoDtos);
            }
            catch (Exception ex)
            {

                return BadRequest($"Falha ao buscar Pedidos: {ex.Message}");
            }
        }
    }
}
