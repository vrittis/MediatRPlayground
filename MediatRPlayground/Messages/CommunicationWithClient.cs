using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MediatRPlayground.Messages
{
    public abstract class CommunicationWithClient: INotification
    {
        public abstract string FormatContent();
        public string MessageContent { get; set; }
    }
}
