﻿namespace EcommProject.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public int ClienteId { get; set; }
        public bool Processado { get; set; }
    }
}
