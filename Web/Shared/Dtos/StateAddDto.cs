using System.ComponentModel.DataAnnotations;

namespace Web.Shared.Dtos
{
    public class StateAddDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
