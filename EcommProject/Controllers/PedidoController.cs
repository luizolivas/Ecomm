﻿using EcommProject.Dtos;
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

    }
}
