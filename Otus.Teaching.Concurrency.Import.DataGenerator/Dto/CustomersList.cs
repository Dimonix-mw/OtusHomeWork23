using System.Collections.Generic;
using System.Xml.Serialization;
using Otus.Teaching.Concurrency.Import.Handler.Entities;

namespace Otus.Teaching.Concurrency.Import.DataGenerator.Dto
{
    [XmlRoot("Customers")]
    public class CustomersList
    {
        public List<Customer> Customers { get; set; }
    }
}