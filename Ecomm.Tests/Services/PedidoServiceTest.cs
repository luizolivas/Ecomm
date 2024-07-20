using AutoMapper;
using EcommProject.Context;
using EcommProject.Dtos;
using EcommProject.Models;
using EcommProject.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ecomm.Tests.Services
{
    public class PedidoServiceTest
    {
        private PedidoService pedidoService;
        private ApplicationDbContext context;
        private IMapper mapper;
        private Mock<IRabbitService> mockRabbitService;

        public PedidoServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            context = new ApplicationDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PedidoDto, Pedido>();
                cfg.CreateMap<Pedido, PedidoResponseDto>();
            });

            mapper = config.CreateMapper();
            mockRabbitService = new Mock<IRabbitService>();

            pedidoService = new PedidoService(context, mapper, mockRabbitService.Object);
        }

        [Fact]
        public async Task Post_CreatePedido()
        {
            PedidoDto pedido = new PedidoDto { ClienteId = 2, Preco = 3, ProdutoId = 4, Quantidade = 5 };


            await pedidoService.AdicionaPedido(pedido);

            var pedidoResult = await context.Pedidos.FirstOrDefaultAsync();
            Assert.NotNull(pedidoResult);
            Assert.Equal(2, 2);

            context.Database.EnsureDeleted();
        }

    }
}
