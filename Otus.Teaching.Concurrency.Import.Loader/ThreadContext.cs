using Otus.Teaching.Concurrency.Import.Core.Loaders;
using Otus.Teaching.Concurrency.Import.Handler.Entities;
using System.Collections.Generic;
using System.Threading;

namespace Otus.Teaching.Concurrency.Import.Loader
{
    internal class ThreadContext
    {
        public WaitHandle State { get; }
        public List<Customer> Partition { get; }

        public FakeDataLoader Loader { get; }

        public ThreadContext(WaitHandle state, FakeDataLoader loader, List<Customer> partition)
        {
            State = state;
            Partition = partition;
            Loader = loader;
        }

    }
}