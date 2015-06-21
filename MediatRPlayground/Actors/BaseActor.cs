using System;

namespace MediatRPlayground.Actors
{
    public abstract class BaseActor
    {
        public static object _lock = new object();
        public abstract int Position { get; }
        public abstract ConsoleColor Color { get; }

        public void Say(string text)
        {
            lock (_lock)
            {
                var previousColor = Console.ForegroundColor;
                Console.ForegroundColor = Color;
                Console.SetCursorPosition(Position, Console.CursorTop);
                Console.WriteLine(text);
                Console.ForegroundColor = previousColor;
            }
        }
    }
}