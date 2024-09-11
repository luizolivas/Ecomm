using System.ComponentModel.DataAnnotations;

namespace EcommProject.Dtos
{
    public class ClienteDTO
    {
        [Required]
        public string? Name { get; set; }
    }
}
