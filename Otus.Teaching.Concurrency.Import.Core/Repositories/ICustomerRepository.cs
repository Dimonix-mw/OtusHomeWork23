using Otus.Teaching.Concurrency.Import.Handler.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.Concurrency.Import.Handler.Repositories
{
    public interface ICustomerRepository
    {
        bool AddCustomer(Customer customer);

        bool AddCustomers(IEnumerable<Customer> customer);
    }
}