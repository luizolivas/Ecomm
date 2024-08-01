using AutoMapper;
using EcommProject.Context;
using EcommProject.Dtos;
using EcommProject.Models;
using EcommProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm.Tests.Services
{
    public class ClienteServiceTest
    {
        private ClienteService _clienteService;
        private ApplicationDbContext _dbContext;
        private IMapper mapper;

        public ClienteServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _dbContext = new ApplicationDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cliente, ClienteDTO>().ReverseMap();
            });

            mapper = config.CreateMapper();

            _clienteService = new ClienteService(_dbContext, mapper);
        }

        [Fact]
        public async Task Post_AddCliente()
        {
            ClienteDTO cliente = new ClienteDTO { Name = "Test" };

            await _clienteService.AddCliente(cliente);
            await _dbContext.SaveChangesAsync();

            var clienteResult = await _dbContext.Clientes.FirstOrDefaultAsync();

            Assert.NotNull(clienteResult);
            Assert.Equal(cliente.Name, clienteResult.Name);
            
        }
        
    }
}
