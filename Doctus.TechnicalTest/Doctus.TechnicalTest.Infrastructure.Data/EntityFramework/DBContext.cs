﻿namespace Doctus.TechnicalTest.Infrastructure.Data.EntityFramework
{
    using Doctus.TechnicalTest.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

    public class DBContext : DbContext, IDisposable
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //var serviceProvider = ((IInfrastructure<IServiceProvider>)this).Instance;
                //var config = this.GetService<IConfigurationRoot>();

                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


                // get the configuration from the app settings
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                    .Build();


                // define the database to use
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Activity>()
                .ToTable("Activities");

            modelBuilder.Entity<Hour>()
                .ToTable("Hours");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Hour> Hours { get; set; }
    }
}
