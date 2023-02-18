using System.ComponentModel.DataAnnotations;

namespace Web.Shared.Dtos
{
    public class CustomerAddDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
