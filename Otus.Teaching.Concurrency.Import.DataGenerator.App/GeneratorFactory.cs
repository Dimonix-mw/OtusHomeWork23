using Otus.Teaching.Concurrency.Import.Handler.Data;
using XmlDataGenerator = Otus.Teaching.Concurrency.Import.DataGenerator.Generators.XmlGenerator;

namespace Otus.Teaching.Concurrency.Import.XmlGenerator
{
    public static class GeneratorFactory
    {
        public static IDataGenerator GetGenerator(string fileName, int dataCount)
        {
            return new XmlDataGenerator(fileName, dataCount);
        }
    }
}