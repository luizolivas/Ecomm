using EcommProcessor.Services;
using EcommProject.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoProcessorController : ControllerBase
    {
        private readonly PedidoProcessorService _service;

        public PedidoProcessorController(PedidoProcessorService service)
        {
            _service = service;
        }

        [Route("resp/")]
        [HttpPost]
        public async Task ProcessaPedido(PedidoDto pedidoDto)
        {
            try
            {
                await _service.ProcessaPedido(pedidoDto);

            }
            catch (Exception ex)
            {
            }
        }
    }
}
