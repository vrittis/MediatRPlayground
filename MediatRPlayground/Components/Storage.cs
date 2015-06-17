using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MediatRPlayground.Messages;

namespace MediatRPlayground.Components
{
    public class Storage : 
        IRequestHandler<CheckAvailability, bool>, 
        IRequestHandler<ProcessOrder, Unit>, 
        IRequestHandler<StockRefillDelivery, Unit>
    {
        private Dictionary<string, int> stock = new Dictionary<string,int>();
        private IMediator _mediator;

        public Storage(IMediator mediator)
        {
            stock.Add("food", 3);
            _mediator = mediator;
        }

        public bool Handle(CheckAvailability message)
        {
            return ItemIsInStock(message.Item);
        }

        private bool ItemIsInStock(string item)
        {
            if (!stock.ContainsKey(item))
            {
                stock.Add(item, 0);
            }
            return stock[item] > 0;
        }

        public Unit Handle(ProcessOrder message)
        {
            if (!ItemIsInStock(message.Item))
            {
                _mediator.Send(new RefillStock() { Item = message.Item });
            }

            stock[message.Item]--;
            _mediator.Send(new DeliverItem() { Item = message.Item });
            return Unit.Value;
        }

        public Unit Handle(StockRefillDelivery message)
        {
            Console.WriteLine(String.Format("Stock is being filled with {0} {1}", message.Quantity, message.Item));
            stock[message.Item]  = message.Quantity;
            return Unit.Value;
        }
    }
}
