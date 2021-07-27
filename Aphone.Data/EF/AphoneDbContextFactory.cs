using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Aphone.Data.EF
{
    public class AphoneDbContextFactory : IDesignTimeDbContextFactory<AphoneDbContext>
    {
        public AphoneDbContext CreateDbContext(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AphoneDb");

            var optionsBuilder = new DbContextOptionsBuilder<AphoneDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AphoneDbContext(optionsBuilder.Options);
        }
    }
}
