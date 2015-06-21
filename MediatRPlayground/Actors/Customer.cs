using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;
using MediatRPlayground.Events;

namespace MediatRPlayground.Actors
{
    public class Customer : BaseActor
    {
        private readonly IMediator _mediator;

        public Customer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override int Position
        {
            get { return 0; }
        }

        public override ConsoleColor Color
        {
            get { return ConsoleColor.Cyan; }
        }

        public async Task Order(string item)
        {
            Say("I'd like some " + item);
            var ts = new Stopwatch();
            ts.Start();
            await _mediator.SendAsync(new OrderPassed(item));
            Say("Thanks for the " + item);
            ts.Stop();
            if (ts.ElapsedMilliseconds >= 21000)
            {
                Say("It took too long to get the " + item);
            }
            _mediator.Publish(new SatisfactionExpressed {Item = item, IsHappy = ts.ElapsedMilliseconds < 21000});
        }
    }
}