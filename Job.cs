using Quartz;
using System.Threading.Tasks;

namespace RunBinary
{
    public class Job : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var batchConfig = Utility.Configuration;
            await batchConfig.File.RunProcessAsync(batchConfig.Arguments);
        }
    }
}
