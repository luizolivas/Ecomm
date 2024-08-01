using System.ComponentModel.DataAnnotations;

namespace EcommProject.Dtos
{
    public class ClienteDTO
    {
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
