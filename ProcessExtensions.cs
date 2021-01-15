using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RunBinary
{
    internal static class ProcessExtensions
    {
        internal static async Task RunProcessAsync(this string filename, string arguments)
        {
            //* Create your Process
            using var process = new Process();
            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.EnableRaisingEvents = true;
            process.Exited += process.ExitHandler;
            process.Start();
            await process.WaitForExitAsync();
        }

        private static void ExitHandler(this Process process, object _, EventArgs __)
        {
            if (!process.HasExited) return;
            if (process.ExitCode == 0)
            {
                var message = process.StandardOutput.ReadToEnd();
                Console.WriteLine(message);
            }
            else
            {
                var errorMessage = $"ERROR running:\n{process.StartInfo.FileName} {process.StartInfo.Arguments}\n{ process.StandardError.ReadToEnd()}";
                //Console.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }
        }
    }
}
