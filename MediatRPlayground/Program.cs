using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MediatR;
using MediatRPlayground.Actors;
using MediatRPlayground.Tools;

namespace MediatRPlayground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = RegisterIOC();
            var customer = ResolveCustomer(container);
            ListenToCustomer(customer);
        }

        private static WindsorContainer RegisterIOC()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IMediator>().ImplementedBy<Mediator>());
            container.Register(
                Classes.FromAssemblyInThisApplication().InSameNamespaceAs<Customer>().WithServiceAllInterfaces());

            container.Kernel.AddHandlersFilter(new ContravariantFilter());
            container.Register(
                Component.For<SingleInstanceFactory>().UsingFactoryMethod<SingleInstanceFactory>(k => t => k.Resolve(t)));
            container.Register(
                Component.For<MultiInstanceFactory>()
                    .UsingFactoryMethod<MultiInstanceFactory>(k => t => (IEnumerable<object>)k.ResolveAll(t)));
            return container;
        }


        private static Customer ResolveCustomer(WindsorContainer container)
        {
            return container.Resolve<Customer>();
        }

        private static void ListenToCustomer(Customer customer)
        {
            Console.WriteLine("Type what you want: q to exit");
            var item = Console.ReadLine();
            while (item != "q")
            {
                customer.Order(item);
                item = Console.ReadLine();
            }
        }




       
    }
}