namespace Core.Entities
{
    public class Window : Base
    {
        public string Name { get; set; }
        public List<OrderWindow> OrderWindows { get; set; }
    }
}
