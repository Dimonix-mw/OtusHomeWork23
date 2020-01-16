using Otus.Teaching.Concurrency.Import.Handler.Entities;

namespace Otus.Teaching.Concurrency.Import.Handler.Repositories
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
    }
}