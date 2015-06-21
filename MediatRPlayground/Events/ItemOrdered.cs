using MediatR;

namespace MediatRPlayground.Events
{
    public class ItemOrdered : IAsyncRequest<ItemReady>
    {
        public string Item { get; set; }
    }
}