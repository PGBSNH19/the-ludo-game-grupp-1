using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ludo
{
    public static class ConnectionSetup
    {
        public static string GetConnectionString()
        { 
            var environmentName = Environment.GetEnvironmentVariable("ConfigEnv");
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            var config = builder.Build();

            return config.GetConnectionString("DefaultConnection");
        }
    }
}
