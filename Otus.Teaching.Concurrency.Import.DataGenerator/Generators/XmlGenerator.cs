using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Otus.Teaching.Concurrency.Import.DataGenerator.Dto;
using Otus.Teaching.Concurrency.Import.Handler.Data;
using Otus.Teaching.Concurrency.Import.Handler.Entities;

namespace Otus.Teaching.Concurrency.Import.DataGenerator.Generators
{
    public class XmlGenerator : IDataGenerator
    {
        private readonly string _fileName;
        private readonly int _dataCount;

        public XmlGenerator(string fileName, int dataCount)
        {
            _fileName = fileName;
            _dataCount = dataCount;
        }
        
        public void Generate()
        {
            var customers = RandomCustomerGenerator.Generate(_dataCount);
            using var stream = File.Create(_fileName);
            new XmlSerializer(typeof(CustomersList)).Serialize(stream, new CustomersList()
            {
                Customers = customers
            });
        }
    }
}