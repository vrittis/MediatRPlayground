using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRPlayground.Messages
{
    public class InformClient:  CommunicationWithClient
    {
        public override string FormatContent()
        {
            return string.Format("Message: " + MessageContent);
        }
    }
}
