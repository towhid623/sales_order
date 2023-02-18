using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class OrderWindow : Base
    {
        [ForeignKey(nameof(Order))]
        public long OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey(nameof(Window))]
        public long WindowId { get; set; }
        public Window Window { get; set; }

        public int Quantity { get; set; }
        public List<OrderWindowElement> OrderWindowElements { get; set; }
    }
}
