using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRPlayground.Events;
using MediatRPlayground.Tools;

namespace MediatRPlayground.Actors
{
    public class SensitiveCook : BaseActor
        , IAsyncRequestHandler<ItemOrdered, ItemReady>
        , IAsyncNotificationHandler<StaffWarned>
    {
        private const int CookingCapacity = 5;

        private readonly ConcurrentDictionary<string, CookingOperation> _currentOperations =
            new ConcurrentDictionary<string, CookingOperation>();

        private readonly BackgroundQueue _kitchen = new BackgroundQueue();

        public override int Position
        {
            get { return 20; }
        }

        public override ConsoleColor Color
        {
            get { return ConsoleColor.Red; }
        }

        public Task Handle(StaffWarned notification)
        {
            _kitchen.QueueTask(() =>
            {
                Say("This is an outrage, I'll be in my kitchen");
                Thread.Sleep(4000);
                Say("I'm back at work");
                return 0;
            });
            return new Task(() => { });
        }

        public Task<ItemReady> Handle(ItemOrdered message)
        {
            lock (_currentOperations)
            {
                var operation = _currentOperations.GetOrAdd(message.Item, new CookingOperation
                {
                    Quantity = 0,
                    Refilling = null
                });

                operation.Quantity = (operation.Quantity + 1)%CookingCapacity;
                if (operation.Quantity == 1)
                {
                    Say("I'm going to cook " + CookingCapacity + " " + message.Item + " ASAP");
                    operation.Refilling = _kitchen.QueueTask(() =>
                    {
                        Say("Starting to cook " + CookingCapacity + " " + message.Item);
                        Thread.Sleep(10000);
                        Say("I'm done cooking " + CookingCapacity + " " + message.Item);
                        return new ItemReady();
                    });
                }
                return operation.Refilling;
            }
        }

        public class CookingOperation
        {
            public int Quantity { get; set; }
            public Task<ItemReady> Refilling { get; set; }
        }
    }
}