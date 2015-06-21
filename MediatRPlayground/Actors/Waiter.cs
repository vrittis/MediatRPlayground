using System;
using System.Threading.Tasks;
using MediatR;
using MediatRPlayground.Events;

namespace MediatRPlayground.Actors
{
    public class Waiter : BaseActor
        , IAsyncRequestHandler<OrderPassed, OrderCompleted>
        , INotificationHandler<SatisfactionExpressed>
        , IAsyncNotificationHandler<StaffWarned>
    {
        private readonly IMediator _mediator;

        public Waiter(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override int Position
        {
            get { return 10; }
        }

        public override ConsoleColor Color
        {
            get { return ConsoleColor.Yellow; }
        }

        public Task Handle(StaffWarned notification)
        {
            return new Task(() => { });
        }

        public async Task<OrderCompleted> Handle(OrderPassed message)
        {
            Say("Right away. Cook, i need 1 " + message.Item);
            await _mediator.SendAsync(new ItemOrdered {Item = message.Item});
            Say("Here is your " + message.Item);
            return new OrderCompleted {Item = message.Item, Id = message.Id};
        }

        public void Handle(SatisfactionExpressed notification)
        {
            if (!notification.IsHappy)
            {
                Say("I'm sorry you're disappointed with your " + notification.Item);
            }
        }
    }
}