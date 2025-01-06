using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

using System.IO;

namespace MealOrderingApp.Server.Data.Context
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        private readonly IConfiguration _configuration;

        public DataContextFactory()
        {
            _configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory()) 
                                .AddJsonFile("appsettings.json") 
                                .Build();
        }

        public DataContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataContext CreateDbContext(string[] args)
        {


            //string connectionString = "Server=WINDOWS-7LLL8VI\\SQLEXPRESS;Database=MealOrderDb;Trusted_Connection=True";

            var builder = new DbContextOptionsBuilder<DataContext>();

            builder.UseSqlServer(_configuration.GetConnectionString("Default"));

            return new DataContext(builder.Options);
        }
    }

}
