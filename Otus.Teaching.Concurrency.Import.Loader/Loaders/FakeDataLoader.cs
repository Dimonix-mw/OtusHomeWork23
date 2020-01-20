using System;
using System.Threading;

namespace Otus.Teaching.Concurrency.Import.Core.Loaders
{
    public class FakeDataLoader
        : IDataLoader
    {
        public void LoadData()
        {
            Console.WriteLine("Loading data...");
            Thread.Sleep(10000);
            Console.WriteLine("Loaded data...");
        }
    }
}