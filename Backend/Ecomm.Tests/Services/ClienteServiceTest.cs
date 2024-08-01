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

            _dbContext.Database.EnsureDeleted();
            
        }


        [Fact]
        public async Task Get_GetAllPedidos()
        {
            List<ClienteDTO> clientesDto = new List<ClienteDTO>
            {
                new ClienteDTO { Name = "Test1" },
                new ClienteDTO { Name = "Test2" }
            };



            foreach (var cliente in clientesDto)
            {
                await _clienteService.AddCliente(cliente);
            }
            await _dbContext.SaveChangesAsync();

            var result = await _clienteService.GetAllClientes();

            Assert.Equal(2, result.Count);
            Assert.NotNull(result);
            Assert.Equal(clientesDto.Count, result.Count);

            for (int i = 0; i < clientesDto.Count; i++)
            {
                Assert.Equal(clientesDto[i].Name, result[i].Name);
            }

            _dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task Get_GetClienteById()
        {
            List<ClienteDTO> clientesDto = new List<ClienteDTO>
            {
                new ClienteDTO { Name = "Test1" },
                new ClienteDTO { Name = "Test2" },
                new ClienteDTO { Name = "Test3" },
                new ClienteDTO { Name = "Test4" }
            };

            foreach (var cliente in clientesDto)
            {
                await _clienteService.AddCliente(cliente);
            }
            await _dbContext.SaveChangesAsync();


            var result = await _clienteService.GetCliente(3);

            Assert.NotNull(result);
            Assert.Equal("Test3", result.Name);

            _dbContext.Database.EnsureDeleted();

        }

    }
}
