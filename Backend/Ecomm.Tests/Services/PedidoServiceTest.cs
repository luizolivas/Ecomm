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
                cfg.CreateMap<PedidoDto, Pedido>().ReverseMap();
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

        [Fact]
        public async Task Get_GetAllPedidos()
        {
            List<PedidoDto> pedidoDtos = new List<PedidoDto>
            {
                new PedidoDto { ClienteId = 5, Preco=5 , ProdutoId = 5 , Quantidade= 5 },
                new PedidoDto { ClienteId = 6, Preco=6 , ProdutoId = 6 , Quantidade= 6 }
            };

            await context.Pedidos.AddRangeAsync();

            foreach(var pedido in pedidoDtos)
            {
                context.Pedidos.Add(mapper.Map<Pedido>(pedido));
            }
            await context.SaveChangesAsync();

            var result = await pedidoService.GetAllPedidos();

            Assert.Equal(2, result.Count);       
            Assert.NotNull(result);
            Assert.Equal(pedidoDtos.Count, result.Count);

            for (int i = 0; i < pedidoDtos.Count; i++)
            {
                Assert.Equal(pedidoDtos[i].ClienteId , result[i].ClienteId);
                Assert.Equal(pedidoDtos[i].Preco , result[i].Preco);
                Assert.Equal(pedidoDtos[i].ProdutoId , result[i].ProdutoId);
                Assert.Equal(pedidoDtos[i].Quantidade , result[i].Quantidade);
            }

            context.Database.EnsureDeleted();
        }

    }
}
