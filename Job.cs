using Quartz;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RunBinary
{
    public class Job : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Start Job.");
            var batchConfig = Utility.Configuration;
            Console.WriteLine(JsonSerializer.Serialize(batchConfig));
            batchConfig.File.RunProcessAsync(batchConfig.Arguments);
        }
    }
}
