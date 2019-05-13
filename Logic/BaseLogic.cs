using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.DbContext;

namespace Logic
{
    public class BaseLogic
    {
        protected DbContextOptionsBuilder<OracleDbContext> oracleBuilder = null;
        protected DbContextOptionsBuilder<MsSqlDbContext> mssqlBuilder = null;

        protected BaseLogic()
        {
            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json");
            //var configuration = builder.Build();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                //.SetBasePath(Directory.GetCurrentDirectory()) //当前目录
                .AddJsonFile("appsettings.json")
                .Build();
            oracleBuilder = new DbContextOptionsBuilder<OracleDbContext>();
            oracleBuilder.UseOracle(configuration.GetConnectionString("OracleDatabase"));


            mssqlBuilder = new DbContextOptionsBuilder<MsSqlDbContext>();
            mssqlBuilder.UseSqlServer(configuration.GetConnectionString("MsSqlDatabase"));

            //System.Console.WriteLine({configuration["ConnectionStrings"]})
        }
    }
}
