using System.Collections.Generic;
using System.IO;
using Otus.Teaching.Concurrency.Import.DataGenerator.Dto;
using Otus.Teaching.Concurrency.Import.Handler.Data;
using Otus.Teaching.Concurrency.Import.Handler.Entities;
using ServiceStack.Text;

namespace Otus.Teaching.Concurrency.Import.DataGenerator.Generators
{
    public class CSVGenerator : IDataGenerator
    {
        private readonly string _fileName;
        private readonly int _dataCount;

        public CSVGenerator(string fileName, int dataCount)
        {
            _fileName = fileName;
            _dataCount = dataCount;
        }
        
        public void Generate()
        {
            var customers = RandomCustomerGenerator.Generate(_dataCount);
            using var stream = File.Create(_fileName);
            CsvSerializer.SerializeToStream(customers, stream);
        }
    }
}