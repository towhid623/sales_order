namespace Web.Shared.Dtos
{
    public class OrderGetByIdDto
    {
        public long Id { get; set; }
        public string? Customer { get; set; }
        public long CustomerId { get; set; }
        public string? State { get; set; }
        public long StateId { get; set; }
        public List<OrderGetByIdWindowDto>? Windows { get; set; }
    }

    public class OrderGetByIdWindowDto
    {
        public long Id { get; set; }
        public long WindowId { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int NoOfSubElements { get; set; }
        public List<OrderGetByIdSubElementDto> SubElements { get; set; }
    }

    public class OrderGetByIdSubElementDto
    {
        public long Id { get; set; }
        public string? Type { get; set; }
        public int Element { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
    }
}
