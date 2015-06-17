using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MediatRPlayground.Messages;

namespace MediatRPlayground.Components
{
    public class CommunicationSystem: INotificationHandler<CommunicationWithClient>
    {
        public void Handle(CommunicationWithClient notification)
        {
            Console.WriteLine(notification.FormatContent());
        }
    }
}
