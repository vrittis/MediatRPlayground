using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MediatRPlayground.Components;
using MediatRPlayground.Messages;
using StructureMap;

namespace MediatRPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    //scanner.AssemblyContainingType<ProcessOrder>(); 
                    scanner.IncludeNamespaceContainingType<CommunicationSystem>();
                    scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
                });
                
                cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
                cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
                cfg.For<IMediator>().Use<Mediator>();

                cfg.For<CustomerDesk>().Use<CustomerDesk>();
                
            });

            var customerDesk = container.GetInstance<CustomerDesk>();

            Console.WriteLine("Type what you want: q to exit");
            var itemOrdered = Console.ReadLine();
            while (itemOrdered != "q")
            {
                customerDesk.ReceiveOrderFromCustomer(itemOrdered);
                itemOrdered = Console.ReadLine();
            }
        }
    }
}
