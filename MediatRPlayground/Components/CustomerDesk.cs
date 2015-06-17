using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MediatRPlayground.Messages;

namespace MediatRPlayground.Components
{
    public class CustomerDesk 
    {
        private IMediator _mediator;
        public CustomerDesk(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void ReceiveOrderFromCustomer(string item)
        {
            var itemIsAvailable = _mediator.Send(new CheckAvailability() { Item = item });
            if (itemIsAvailable)
            {
                _mediator.Publish(new InformClient() { MessageContent = "Item " + item + " is available, you will receive it very soon" });
            }
            else
            {
                _mediator.Publish(new AlertClient() { MessageContent = "Item " + item + " is not available, expect it in some time" });
            }
            _mediator.Send(new ProcessOrder() { Item = item });
        }

    }
}
