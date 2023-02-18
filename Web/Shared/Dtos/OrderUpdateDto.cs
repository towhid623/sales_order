using System.ComponentModel.DataAnnotations;

namespace Web.Shared.Dtos
{
    public class OrderUpdateDto
    {
        public long Id { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Customer is required")]
        public long CustomerId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "State is required")]
        public long StateId { get; set; }
        public List<OrderUpdateWindowDto>? Windows { get; set; }
    }

    public class OrderUpdateWindowDto
    {
        public long? Id { get; set; }
        public long WindowId { get; set; }
        public int Quantity { get; set; }
        public List<OrderUpdateWindowSubElementDto>? SubElements { get; set; }
    }

    public class OrderUpdateWindowSubElementDto
    {
        public long? Id { get; set; }
        public string? Type { get; set; }
        public int Element { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
    }
}
