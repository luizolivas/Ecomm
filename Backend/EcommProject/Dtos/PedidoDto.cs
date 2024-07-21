namespace EcommProject.Dtos
{
    public class PedidoDto
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public int ClienteId { get; set; }
    }
}
