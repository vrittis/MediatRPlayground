using MediatR;

namespace MediatRPlayground.Events
{
    public class SatisfactionExpressed : INotification
    {
        public string Item { get; set; }
        public bool IsHappy { get; set; }
    }
}