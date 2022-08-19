using Otus.Teaching.Concurrency.Import.Handler.Entities;
using Otus.Teaching.Concurrency.Import.Handler.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.Concurrency.Import.Core.Loaders
{
    public class FakeDataLoader
        : IDataLoader
    {
        private readonly ICustomerRepository _customerRepository;
        public FakeDataLoader(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public bool LoadData(Customer customer)
        {
             return _customerRepository.AddCustomer(customer);
        }

        public bool LoadDataList(List<Customer> customers)
        {
            return _customerRepository.AddCustomers(customers);
        }

    }
}