using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class OrderWindowElement : Base
    {
        [ForeignKey(nameof(OrderWindow))]
        public long OrderWindowId { get; set; }
        public OrderWindow OrderWindow { get; set; }

        public string Type { get; set; }
        public int Element { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
    }
}
