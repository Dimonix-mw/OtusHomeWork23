using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Otus.Teaching.Concurrency.Import.Core.Loaders;
using Otus.Teaching.Concurrency.Import.DataGenerator.Generators;


namespace Otus.Teaching.Concurrency.Import.Loader
{
    class Program
    {
        private static string _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "customers.xml");
        
        static void Main(string[] args)
        {
            if (args != null && args.Length == 1)
            {
                _dataFilePath = args[0];
            }

            Console.WriteLine($"Loader started with process Id {Process.GetCurrentProcess().Id}...");

            GenerateCustomersDataFile();
            
            var loader = new FakeDataLoader();

            loader.LoadData();
        }

        static void GenerateCustomersDataFile()
        {
            var xmlGenerator = new XmlGenerator(_dataFilePath, 1000);
            xmlGenerator.Generate();
        }
    }
}