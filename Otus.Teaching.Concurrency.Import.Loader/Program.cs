using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Otus.Teaching.Concurrency.Import.Core.Loaders;
using Otus.Teaching.Concurrency.Import.DataAccess.Parsers;
using Otus.Teaching.Concurrency.Import.DataAccess.Repositories;
using Otus.Teaching.Concurrency.Import.DataGenerator.Generators;
using Otus.Teaching.Concurrency.Import.Handler.Entities;
using Otus.Teaching.Concurrency.Import.Loader.Settings;
using Otus.Teaching.Concurrency.Import.Loader.WorkProcess;

namespace Otus.Teaching.Concurrency.Import.Loader
{
    class Program
    {
        private static string _dataFileName = "customers";
        private static string _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _dataFileName + ".xml");
        private static int _dataCount;
        private static IConfigurationRoot _config;
        private static WaitHandle[] _waitHandles;
        private static Barrier _barrier;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _config = builder.Build();

            _dataCount = SettingsApp.GetDataCount(_config);
            bool useProcess = SettingsApp.GetUseProcess(_config);
            //генерация файла с данными, либо созданием отдельного процесса, либо вызовом метода
            GenerateCustomersData(useProcess);
            //десериализация данных с файла в коллекцию
            var customers = DesializeXmlCustomers();
            //загрузка десериализованных данных 
            LoadCustomersData(customers);
        }

        /// <summary>
        /// загрузка данных 
        /// </summary>
        /// <param name="customers"></param>
        private static void LoadCustomersData(List<Customer> customers)
        {
            int countThread = SettingsApp.GetCountThread(_config, customers.Count);

            var useThreadpool = SettingsApp.GetUseThreadpool(_config);
            var customersDatabaseSettings = SettingsApp.GetDataBaseSettings(_config);

            var partitions = customers.Partition(customers.Count / countThread);
            _waitHandles = new WaitHandle[countThread];
            _barrier = new Barrier(countThread + 1); 

            for (int i = 0; i < countThread; i++)
            {
                _waitHandles[i] = new AutoResetEvent(false);
                var loader = new FakeDataLoader(new CustomerRepository(customersDatabaseSettings));
                if (useThreadpool) 
                    ThreadPool.QueueUserWorkItem(LoadPartition, new ThreadContext(_waitHandles[i], loader, partitions[i]));
                else
                    new Thread(LoadPartition).Start(new ThreadContext(_waitHandles[i], loader, partitions[i]));
            }
            Console.WriteLine($"Loading data in {countThread} threads...");

            _barrier.SignalAndWait();
            var sw = new Stopwatch();
            sw.Start();
            WaitHandle.WaitAll(_waitHandles);
            sw.Stop();
            Console.WriteLine("Loaded data...");
            Console.WriteLine($"Elapsed time {sw.ElapsedMilliseconds} мс");
        }

        private static void LoadPartition(object threadContext)
        {
            ThreadContext context = (ThreadContext)threadContext;
            AutoResetEvent are = (AutoResetEvent)context.State;
            _barrier.SignalAndWait();
            //context.Loader.LoadData(context.Partition);
            context.Loader.LoadDataEnumerable(context.Partition);
            are.Set();
        }

        /// <summary>
        /// генерация данных в файл
        /// </summary>
        /// <param name="useProcess">true - генерация через запуск отдельного процесса, false - через метод</param>
        private static void GenerateCustomersData(bool useProcess)
        {
            if (useProcess)
            {
                var pathProcess = SettingsApp.GetPathProcess(_config);
                GenerateCustomersDataFileInProcess(pathProcess);
            }
            else
            {
                GenerateCustomersDataFile();
            }
        }

        /// <summary>
        /// метод генерации данных файла
        /// </summary>
        private static void GenerateCustomersDataFile()
        {
            Console.WriteLine($"Loader started from method...");
            Console.WriteLine("Generating xml data...");
            var xmlGenerator = new XmlGenerator(_dataFilePath, _dataCount);
            xmlGenerator.Generate();
            Console.WriteLine($"Generated xml data in {_dataFilePath}...");
        }

        /// <summary>
        /// запуск генерации данных файла отдельным процессом
        /// </summary>
        /// <param name="pathProcess"></param>
        private static void GenerateCustomersDataFileInProcess(string pathProcess)
        {
            var process = WorkWithProcess.StartProcess(pathProcess, _dataFileName, _dataCount);
            Console.WriteLine($"Loader started with process Id {process.Id}...");
            Console.WriteLine("Generating xml data...");
            while (!process.StandardOutput.EndOfStream)
            {
                _dataFilePath = process.StandardOutput.ReadLine();
                Console.WriteLine($"Generated xml data in {_dataFilePath}...");
            }
        }

        /// <summary>
        /// десериализация данных файла
        /// </summary>
        /// <returns>List<Customer></returns>
        private static List<Customer> DesializeXmlCustomers()
        {
            return new XmlParser(_dataFilePath).Parse();
        }

    }
}