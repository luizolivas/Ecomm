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


            mockRabbitService = new Mock<IRabbitService>();

            pedidoService = new PedidoService(context, mapper, mockRabbitService.Object);
        }
    }
}
