using System;
using MediatR;
using MediatRPlayground.Messages;

namespace MediatRPlayground.Components
{
    public class Sellers : RequestHandler<RefillStock>
    {
        private readonly IMediator _mediator;

        public Sellers(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override void HandleCore(RefillStock message)
        {
            Console.WriteLine("Sending deliver order for " + message.Item);
            _mediator.Send(new StockRefillDelivery { Item = message.Item, Quantity = 5 });
        }
    }
}