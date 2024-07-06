namespace EcommProject.Dtos
{
    public class PedidoResponseDto
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public int ClienteId { get; set; }
        public bool Processado { get; set; }
    }
}
