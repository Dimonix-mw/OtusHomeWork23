using System.Diagnostics;

namespace Otus.Teaching.Concurrency.Import.Loader.WorkProcess
{
    public static class WorkWithProcess
    {
        /// <summary>
        /// запуск процесса генерации данных файла
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="argumentdataFileName"></param>
        /// <param name="argumentDataCount"></param>
        /// <returns>Process</returns>
        public static Process StartProcess(string fileName, string argumentdataFileName, int argumentDataCount)
        {
            var startInfo = new ProcessStartInfo()
            {
                ArgumentList = { argumentdataFileName, argumentDataCount.ToString() },
                FileName = fileName,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            var process = Process.Start(startInfo);

            return process;
        }
    }
}
