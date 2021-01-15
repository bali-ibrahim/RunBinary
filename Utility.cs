using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace RunBinary
{
    internal static class Utility
    {
        internal static readonly Configuration Configuration;

        private static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().Location;
                //var codeBase = AppContext.BaseDirectory;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        static Utility()
        {
            var assemblyConfigFile = $"{AssemblyDirectory}{Path.DirectorySeparatorChar}{AssemblyName}.json";
            var configuration = new ConfigurationBuilder()
                // current dll config
                .AddJsonFile(assemblyConfigFile)
                .Build()
                ;
            var configurationInstance = configuration.GetSection(typeof(Configuration).Name).Get<Configuration>();
            Configuration = configurationInstance;

        }
    }
}
