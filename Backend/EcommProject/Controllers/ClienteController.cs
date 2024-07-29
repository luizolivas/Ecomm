using EcommProject.Dtos;
using EcommProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<ActionResult> AddCliente(ClienteDTO clienteDTO)
        {
            try
            {
                await _clienteService.AddCliente(clienteDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCliente()
        {
            try
            {
                List<ClienteDTO> clientes = await _clienteService.GetAllClientes();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCliente(int id)
        {
            try
            {
                ClienteDTO cliente = await _clienteService.GetCliente(id);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
