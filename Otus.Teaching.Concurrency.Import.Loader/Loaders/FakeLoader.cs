using System;
using System.Threading;

namespace Otus.Teaching.Concurrency.Import.Core.Loaders
{
    public class FakeLoader
        : ILoader
    {
        public void LoadData()
        {
            Console.WriteLine("Loading data...");
            Thread.Sleep(10000);
            Console.WriteLine("Loaded data...");
        }
    }
}