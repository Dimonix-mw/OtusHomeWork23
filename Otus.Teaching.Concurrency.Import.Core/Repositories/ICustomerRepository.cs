using Otus.Teaching.Concurrency.Import.Handler.Entities;
using System.Collections.Generic;

namespace Otus.Teaching.Concurrency.Import.Handler.Repositories
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);

        void AddCustomers(IEnumerable<Customer> customer);
    }
}