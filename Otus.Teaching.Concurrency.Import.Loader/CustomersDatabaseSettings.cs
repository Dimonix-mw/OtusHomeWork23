using Otus.Teaching.Concurrency.Import.Handler.Repositories;

namespace Otus.Teaching.Concurrency.Import.Loader
{
    public class CustomersDatabaseSettings : ICustomersDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CustomersCollectionName { get; set; } = null!;
        
    }
}
