using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel;

namespace MediatRPlayground.Tools
{
    public class ContravariantFilter : IHandlersFilter
    {
        public bool HasOpinionAbout(Type service)
        {
            if (!service.IsGenericType)
                return false;

            var genericType = service.GetGenericTypeDefinition();
            var genericArguments = genericType.GetGenericArguments();
            return genericArguments.Count() == 1
                   &&
                   genericArguments.Single()
                       .GenericParameterAttributes.HasFlag(GenericParameterAttributes.Contravariant);
        }

        public IHandler[] SelectHandlers(Type service, IHandler[] handlers)
        {
            return handlers;
        }
    }
}
