using System.Collections.Generic;
using Otus.Teaching.Concurrency.Import.Core.Parsers;
using Otus.Teaching.Concurrency.Import.Handler.Entities;
using System.IO;
using ServiceStack.Text;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Parsers
{
    public class CSVParser
        : IDataParser<List<Customer>>
    {
        private readonly string _fileName;
        
        public CSVParser(string fileName)
        {
            _fileName = fileName;
        }

        public List<Customer> Parse()
        {
            using var stream = File.OpenRead(_fileName);
            return CsvSerializer.DeserializeFromStream<List<Customer>>(stream);
        }
    }
}