using Otus.Teaching.Concurrency.Import.Handler.Entities;
using Otus.Teaching.Concurrency.Import.Handler.Repositories;
using System.Collections.Generic;

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

        public void LoadData(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                _customerRepository.AddCustomer(customer);
            }
        }

        public void LoadDataEnumerable(IEnumerable<Customer> customers)
        {
            _customerRepository.AddCustomers(customers);
        }

    }
}