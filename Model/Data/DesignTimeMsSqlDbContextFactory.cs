using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Model.DbContext;
using System;
using System.IO;

namespace Model.Data
{
    public class DesignTimeMsSqlDbContextFactory : IDesignTimeDbContextFactory<MsSqlDbContext>
    {
        public MsSqlDbContext CreateDbContext(string[] args)
        {
            //string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
            //.SetBasePath(AppDomain.CurrentDomain.BaseDirectory) //运行目录
             .SetBasePath(Directory.GetCurrentDirectory()) //当前目录
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<MsSqlDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("MsSqlDatabase"));
            return new MsSqlDbContext(builder.Options);
        }
    }
}