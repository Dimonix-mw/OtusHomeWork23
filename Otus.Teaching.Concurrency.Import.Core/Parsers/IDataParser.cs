namespace Otus.Teaching.Concurrency.Import.Core.Parsers
{
    public interface IDataParser<T>
    {
        T Parse();
    }
}