using System.Collections.Generic;
using Otus.Teaching.Concurrency.Import.Core.Parsers;
using Otus.Teaching.Concurrency.Import.Handler.Entities;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Parsers
{
    public class XmlParser
        : IDataParser<List<Customer>>
    {
        public List<Customer> Parse()
        {
            //Parse data
            return new List<Customer>();
        }
    }
}