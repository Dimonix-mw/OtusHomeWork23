using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Otus.Teaching.Concurrency.Import.Handler.Entities;
using Otus.Teaching.Concurrency.Import.Handler.Repositories;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Repositories
{
    public class CustomerRepository
        : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customersCollection;
        public CustomerRepository(ICustomersDatabaseSettings customersDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                customersDatabaseSettings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                customersDatabaseSettings.DatabaseName);

            _customersCollection = mongoDatabase.GetCollection<Customer>(
                customersDatabaseSettings.CustomersCollectionName);
        }
        public void AddCustomer(Customer customer)
        {
            _customersCollection.InsertOne(customer);
        }

        public void AddCustomers(IEnumerable<Customer> customers)
        {
            _customersCollection.InsertMany(customers);
        }
    }
}