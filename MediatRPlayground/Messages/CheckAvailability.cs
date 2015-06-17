using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MediatRPlayground.Messages
{
    public class CheckAvailability: IRequest<bool>
    {
        public string Item { get; set; }
    }
}
