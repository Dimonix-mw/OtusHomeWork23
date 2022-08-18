using System.Collections.Generic;
using Otus.Teaching.Concurrency.Import.Core.Parsers;
using Otus.Teaching.Concurrency.Import.Handler.Entities;
using System.Xml.Serialization;
using System.IO;
using Otus.Teaching.Concurrency.Import.DataGenerator.Dto;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Parsers
{
    public class XmlParser
        : IDataParser<List<Customer>>
    {
        private readonly string _fileName;
        
        public XmlParser(string fileName)
        {
            _fileName = fileName;
        }

        public List<Customer> Parse()
        {
            using var stream = File.OpenRead(_fileName);
            var customersList = (CustomersList)new XmlSerializer(typeof(CustomersList)).Deserialize(stream);
            return customersList.Customers;
        }
    }
}