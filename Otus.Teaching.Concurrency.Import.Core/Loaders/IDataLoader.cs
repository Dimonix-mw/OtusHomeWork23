using Otus.Teaching.Concurrency.Import.Handler.Entities;
using System.Collections.Generic;

namespace Otus.Teaching.Concurrency.Import.Core.Loaders
{
    public interface IDataLoader
    {
        bool LoadData(Customer customer);
        bool LoadDataList(List<Customer> customers);
    }
}