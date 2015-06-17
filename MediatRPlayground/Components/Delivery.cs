using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MediatRPlayground.Messages;

namespace MediatRPlayground.Components
{
    public class Delivery: RequestHandler<DeliverItem>
    {
        protected override void HandleCore(DeliverItem message)
        {
            Console.WriteLine("Delivering " + message.Item + " to customer");
        }
    }
}
