using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SubjectsAndDistrictsDbContext.Connection
{
    public class ConnectionStringConfig
    {
        public string ConnectionString { get; set; }

        public ConnectionStringConfig(string connectionStringName = "DefaultConnection",
            string environmentalVariableConnectionString = "SNDDb_ConnectionString")
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();
            
            string server = "";
            string userId = "";
            string password = "";

            config.Providers.Any(p => p.TryGet("SNDDb:Server", out server));
            config.Providers.Any(p => p.TryGet("SNDDb:UserId", out userId));
            config.Providers.Any(p => p.TryGet("SNDDb:Password", out password));

            ConnectionString = string.Format(config.GetConnectionString(connectionStringName) 
                ?? Environment.GetEnvironmentVariable(environmentalVariableConnectionString), 
                server,
                userId,
                password);
        }
    }
}
