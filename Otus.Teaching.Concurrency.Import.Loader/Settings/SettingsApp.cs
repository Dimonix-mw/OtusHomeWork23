using Microsoft.Extensions.Configuration;
using Otus.Teaching.Concurrency.Import.Handler.Repositories;
using System.IO;

namespace Otus.Teaching.Concurrency.Import.Loader.Settings
{
    public static class SettingsApp
    {
        /// <summary>
        /// получение количества потоков из файла настроек appsettings.json
        /// </summary>
        /// <param name="countCustomers"></param>
        /// <returns>int</returns>
        public static int GetCountThread(IConfigurationRoot config, int countCustomers)
        {
            int countThread = 1;
            var threadSettings = config.GetSection("ThreadSettings");
            if (countCustomers >= 200_000)
                countThread = threadSettings.GetValue<int>("CountThreadMax");
            else if (countCustomers >= 50_000)
                countThread = threadSettings.GetValue<int>("CountThread");
            else
                countThread = threadSettings.GetValue<int>("CountThreadMin");

            return countThread;
        }

        /// <summary>
        /// получение настроек базы данных из файла настроек appsettings.json
        /// </summary>
        /// <returns>CustomersDatabaseSettings</returns>
        public static ICustomersDatabaseSettings GetDataBaseSettings(IConfigurationRoot config)
        {
            var customersDatabaseSettingsSection = config.GetSection("CustomersDatabase");
            var customersDatabaseSettings = new CustomersDatabaseSettings
            {
                ConnectionString = customersDatabaseSettingsSection.GetValue<string>("ConnectionString"),
                DatabaseName = customersDatabaseSettingsSection.GetValue<string>("DatabaseName"),
                CustomersCollectionName = customersDatabaseSettingsSection.GetValue<string>("CustomersCollectionName")
            };
            return customersDatabaseSettings;
        }

        public static bool GetUseThreadpool(IConfigurationRoot config)
        {
            return config.GetValue<bool>("UseThreadpool");
        }

        public static int GetDataCount(IConfigurationRoot config)
        {
            return config.GetValue<int>("CountObjectGeneration");
        }

        public static bool GetUseProcess(IConfigurationRoot config)
        {
            return config.GetValue<bool>("UseProcess");
        }

        /// <summary>
        /// получение пути к исполняемому файлу процесса
        /// </summary>
        /// <param name="processDirectory"></param>
        /// <param name="processFileName"></param>
        /// <returns></returns>
        public static string GetPathProcess(IConfigurationRoot config)
        {
            var processSettings = config.GetSection("ProcessSettings");
            var processDirectory = processSettings.GetValue<string>("ProcessDirectory");
            var processFileName = processSettings.GetValue<string>("ProcessFileName");
            return Path.Combine(processDirectory, processFileName);
        }
    }
}
