using System.ComponentModel.DataAnnotations;

namespace Web.Shared.Dtos
{
    public class WindowAddDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
