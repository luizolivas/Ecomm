using EcommProject.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EcommProject.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<Pedido> Pedidos { get; set; }
    }
}
