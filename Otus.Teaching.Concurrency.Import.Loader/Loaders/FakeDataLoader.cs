using MongoDB.Driver;
using Otus.Teaching.Concurrency.Import.Core.Parsers;
using Otus.Teaching.Concurrency.Import.Handler.Entities;
using Otus.Teaching.Concurrency.Import.Handler.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Otus.Teaching.Concurrency.Import.Core.Loaders
{
    public class FakeDataLoader
        : IDataLoader
    {
        private readonly List<Customer> _customers;
        private readonly int _countThread;
        private readonly ICustomerRepository _customerRepository;
        private readonly bool _useThreadpool;
        private WaitHandle[] _waitHandles;
        private Barrier _barrier;
        public FakeDataLoader(ICustomerRepository customerRepository, List<Customer> customers, int countThread, bool useThreadpool = true)
        {
            _customers = customers;
            _countThread = countThread;
            _customerRepository = customerRepository;
            _useThreadpool = useThreadpool;
        }
        public void LoadData()
        {
            var partitions = _customers.Partition(_customers.Count/_countThread);
            _waitHandles = new WaitHandle[_countThread];
            _barrier = new Barrier(_countThread + 1);

            for (int i = 0; i < _countThread; i++)
            {
                _waitHandles[i] = new AutoResetEvent(false);

                if (_useThreadpool)
                    ThreadPool.QueueUserWorkItem(LoadPartition, new ThreadContext(_waitHandles[i], partitions[i]));
                else
                    new Thread(LoadPartition).Start(new ThreadContext(_waitHandles[i], partitions[i]));
            }
            Console.WriteLine($"Loading data in {_countThread} threads...");
            _barrier.SignalAndWait();
            var sw = new Stopwatch();
            sw.Start();
            WaitHandle.WaitAll(_waitHandles);
            sw.Stop();
            Console.WriteLine("Loaded data...");
            Console.WriteLine($"Elapsed time {sw.ElapsedMilliseconds} мс");
        }

        private void LoadPartition(object threadContext)
        {
            ThreadContext context = (ThreadContext)threadContext;
            AutoResetEvent are = (AutoResetEvent)context.State;
            _barrier.SignalAndWait();
            foreach (var consumer in context.Partition)
            {
                _customerRepository.AddCustomer(consumer);
            }
            are.Set();
        }
    }

    public static class Extensions
    {
        public static List<List<T>> Partition<T>(this List<T> values, int chunkSize)
        {
            return values.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }

    internal class ThreadContext
    {
        public WaitHandle State { get; }
        public List<Customer> Partition { get; }

        public ThreadContext(WaitHandle state, List<Customer> partition)
        {
            State = state;
            Partition = partition;
        }

    }
}