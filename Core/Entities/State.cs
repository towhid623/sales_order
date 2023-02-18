namespace Core.Entities
{
    public class State : Base
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
