using System;
using MediatR;
using MediatRPlayground.Events;

namespace MediatRPlayground.Actors
{
    public class Manager :
        BaseActor
        , INotificationHandler<SatisfactionExpressed>
    {
        private readonly IMediator _mediator;

        public Manager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override int Position
        {
            get { return 30; }
        }

        public override ConsoleColor Color
        {
            get { return ConsoleColor.Green; }
        }

        public void Handle(SatisfactionExpressed notification)
        {
            if (!notification.IsHappy)
            {
                Say("The customer is not happy. Work harder");
                _mediator.PublishAsync(new StaffWarned());
            }
        }
    }
}