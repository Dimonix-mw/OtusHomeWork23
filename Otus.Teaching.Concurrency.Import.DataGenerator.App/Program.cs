using System;
using System.IO;
using XmlDataGenerator = Otus.Teaching.Concurrency.Import.DataGenerator.Generators.XmlGenerator;

namespace Otus.Teaching.Concurrency.Import.XmlGenerator
{
    class Program
    {
        private static readonly string _dataFileDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static string _dataFileName; 
        private static int _dataCount = 100; 
        
        static void Main(string[] args)
        {
            if (!TryValidateAndParseArgs(args))
                return;
            
            Console.WriteLine("Generating xml data...");

            var generator = GeneratorFactory.GetGenerator(_dataFileName, _dataCount);
            
            generator.Generate();
            
            Console.WriteLine($"Generated xml data in {_dataFileName}...");
        }

        private static bool TryValidateAndParseArgs(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                _dataFileName = Path.Combine(_dataFileDirectory, $"{args[0]}.xml");
            }
            else
            {
                Console.WriteLine("Data file name without extension is required");
                return false;
            }
            
            if (args.Length > 1)
            {
                if (!int.TryParse(args[1], out _dataCount))
                {
                    Console.WriteLine("Data must be integer");
                    return false;
                }
            }

            return true;
        }
    }
}