using Otus.Teaching.Concurrency.Import.Handler.Data;
using XmlDataGenerator = Otus.Teaching.Concurrency.Import.DataGenerator.Generators.XmlGenerator;
using CSVDataGenerator = Otus.Teaching.Concurrency.Import.DataGenerator.Generators.CSVGenerator;

namespace Otus.Teaching.Concurrency.Import.XmlGenerator
{
    public static class GeneratorFactory
    {
        public static IDataGenerator GetGenerator(string fileName, int dataCount)
        {
            return new XmlDataGenerator(fileName, dataCount);
        }

        public static IDataGenerator GetGeneratorCSV(string fileName, int dataCount)
        {
            return new CSVDataGenerator(fileName, dataCount);
        }
    }
}