using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRPlayground.Messages
{
    public class AlertClient: CommunicationWithClient
    {
        public override string FormatContent()
        {
            return "ALERT: " + MessageContent;
        }
    }
}
