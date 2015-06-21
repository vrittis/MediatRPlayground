using MediatR;

namespace MediatRPlayground.Events
{
    public class OrderPassed : IAsyncRequest<OrderCompleted>
    {
        private static int _orderNumber = 1;

        public OrderPassed(string item)
        {
            Id = _orderNumber++;
            Item = item;
        }

        public string Item { get; set; }
        public int Id { get; set; }
    }
}