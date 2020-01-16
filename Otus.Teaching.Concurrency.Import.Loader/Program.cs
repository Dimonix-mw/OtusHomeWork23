using System;
using System.Diagnostics;
using System.IO;
using Otus.Teaching.Concurrency.Import.Core.Loaders;


namespace Otus.Teaching.Concurrency.Import.Loader
{
    class Program
    {
        private static string _dataFilePath;
        
        static void Main(string[] args)
        {
            if (args != null && args.Length == 1)
            {
                _dataFilePath = args[0];
            }
            else
            {
                Console.WriteLine("DataFilePath is required");
            }
            
            Console.WriteLine($"Loader started with process Id {Process.GetCurrentProcess().Id}...");
            
            var loader = new FakeLoader();

            loader.LoadData();
        }

        static void GenerateCustomersDataFile()
        {

        }
    }
}