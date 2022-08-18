namespace Otus.Teaching.Concurrency.Import.Handler.Repositories
{
    public interface ICustomersDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CustomersCollectionName { get; set; }
    }
}