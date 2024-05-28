using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Dane
{
    public class Logger
    {
        private readonly string logFilePath;
        private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public Logger(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        public async Task LogAsync(string message)
        {
            await semaphore.WaitAsync();
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    await writer.WriteLineAsync($"{DateTime.Now}: {message}");
                }
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
