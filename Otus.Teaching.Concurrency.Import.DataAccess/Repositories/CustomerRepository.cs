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
        private readonly MongoClient _mongoClient;
        public CustomerRepository(ICustomersDatabaseSettings customersDatabaseSettings)
        {
            _mongoClient = new MongoClient(
                customersDatabaseSettings.ConnectionString);

            var mongoDatabase = _mongoClient.GetDatabase(
                customersDatabaseSettings.DatabaseName);
            
            _customersCollection = mongoDatabase.GetCollection<Customer>(
                customersDatabaseSettings.CustomersCollectionName);

        }
        public bool AddCustomer(Customer customer)
        {
            try
            {
                _customersCollection.InsertOne(customer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddCustomers(IEnumerable<Customer> customers)
        {
            try
            {
                _customersCollection.InsertMany(customers);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}