using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRPlayground.Tools
{
    public class BackgroundQueue
    {
        private readonly object key = new object();
        private Task previousTask = Task.FromResult(true);

        public Task<T> QueueTask<T>(Func<T> work)
        {
            lock (key)
            {
                var task = previousTask.ContinueWith(t => work()
                    , CancellationToken.None
                    , TaskContinuationOptions.None
                    , TaskScheduler.Default);
                previousTask = task;
                return task;
            }
        }
    }
}
