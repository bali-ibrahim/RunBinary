using System;
using System.Diagnostics;

namespace RunBinary
{
    internal static class ProcessExtensions
    {
        internal static void RunProcessAsync(this string filename, string arguments)
        {
            //* Create your Process
            using var process = new Process();
            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.EnableRaisingEvents = true;
            //process.Exited += process.ExitHandler;

            var errorMessage = $"ERROR running:\n{ process.StartInfo.FileName} { process.StartInfo.Arguments}\n";
            process.ErrorDataReceived += (s, e) =>
            {
                Console.WriteLine(e.Data);
                errorMessage += e.Data;
            };
            process.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            Console.WriteLine("Process Started!");
            //await process.WaitForExitAsync();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                throw new Exception(errorMessage);
            };
            process.Close();
            Console.WriteLine("Process Ended!");
        }

        [Obsolete("Deprecated as it doesn't seem to work in publsih.")]
        private static void ExitHandler(this Process process, object _, EventArgs __)
        {
            if (!process.HasExited) return;
            if (process.ExitCode == 0)
            {
                var message = process.StandardOutput.ReadToEnd();
                Console.Out.WriteLine(message);
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
