using System.ComponentModel.DataAnnotations;

namespace Web.Shared.Dtos
{
    public class OrderAddDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Customer is required")]
        public long CustomerId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "State is required")]
        public long StateId { get; set; }
        public List<OrderAddWindowDto>? Windows { get; set; }
    }

    public class OrderAddWindowDto
    {
        public long WindowId { get; set; }
        public int Quantity { get; set; }
        public List<OrderAddWindowSubElementDto>? SubElements { get; set; }
    }

    public class OrderAddWindowSubElementDto
    {
        public string? Type { get; set; }
        public int Element { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
    }
}
