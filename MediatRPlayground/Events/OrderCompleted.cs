namespace MediatRPlayground.Events
{
    public class OrderCompleted
    {
        public string Item { get; set; }
        public int Id { get; set; }
    }
}