namespace Core.Entities
{
    public class Customer : Base
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
