using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Order : Base
    {
        [ForeignKey(nameof(Customer))]
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey(nameof(State))]
        public long StateId { get; set; }
        public State State { get; set; }

        public List<OrderWindow> OrderWindows { get; set; }
    }
}
