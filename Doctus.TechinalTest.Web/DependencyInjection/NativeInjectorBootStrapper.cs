﻿

namespace Doctus.TechinalTest.Web.DependencyInjection
{
    using AutoMapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using Serilog;
    using Serilog.Events;
    using Serilog.Sinks.MSSqlServer;
    using System.Data;
    using System.Collections.ObjectModel;
    using Doctus.TechnicalTest.Infrastructure.Framework.Instrumentation.Logging;
    using Doctus.TechnicalTest.Infrastructure.Data.DBFactory;
    using Doctus.TechnicalTest.Infrastructure.Framework.RepositoryPattern;
    using Doctus.TechnicalTest.Infrastructure.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Doctus.TechnicalTest.Infrastructure.Data.EntityFramework;

    public class NativeInjectorBootStrapper
    {

        /// <summary>
        /// Resolver la dependencia de los servicios
        /// </summary>
        /// <param name="services"></param>
        public void RegisterServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            // Build configuration
            /*var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .Build();*/

            services.AddSingleton(configuration);

            //Automapper
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            //services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

            // Add logging
            #region log
            var columnOptions = new ColumnOptions
            {
                AdditionalDataColumns = new Collection<DataColumn>
               {
                    new DataColumn {DataType = typeof (int), ColumnName = "Priority"},
                    new DataColumn {DataType = typeof (string), ColumnName = "Title"},
                    new DataColumn {DataType = typeof (string), ColumnName = "MachineName"},
                    new DataColumn {DataType = typeof (string), ColumnName = "AppDomainName"},
                    new DataColumn {DataType = typeof (string), ColumnName = "ProcessID"},
                    new DataColumn {DataType = typeof (string), ColumnName = "ProcessName"},
               }
            };

            columnOptions.Store.Remove(StandardColumn.MessageTemplate);
            columnOptions.Store.Remove(StandardColumn.Properties);

            columnOptions.Message.ColumnName = "Message";
            columnOptions.Level.ColumnName = "Severity";
            columnOptions.TimeStamp.ColumnName = "Timestamp";
            columnOptions.Exception.ColumnName = "FormattedMessage";
            columnOptions.LogEvent.ColumnName = "LogEvent";


            services.AddSingleton<Serilog.ILogger>
            (x => new LoggerConfiguration()
                 .MinimumLevel.Verbose()
                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                 .MinimumLevel.Override("System", LogEventLevel.Error)
                 .WriteTo.MSSqlServer(configuration["Serilog:ConnectionString"]
                 , configuration["Serilog:TableName"]
                 , LogEventLevel.Verbose
                 , columnOptions: columnOptions
                 )
                 .CreateLogger());

            //var file = File.CreateText("C:/Docs/Serilog.txt");
            //Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(file));
            #endregion


            // Application
            //services.AddScoped<ISecurityService, SecurityService>();

            //Domain
            //services.AddScoped<IChannelService, ChannelService>();

            //Repositories
            //services.AddScoped<IProductChannelRepository, ProductChannelRepository>();
            //services.AddScoped<IScheduledTaskHistoryRepository, ScheduledTaskHistoryRepository>();

            // Infrastructure

            services.AddScoped<ILoggerService, LoggerService>();

            // Infra - Data

            services.AddScoped<IDatabaseFactory, DatabaseFactory>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<DbContext>(sp => sp.GetService<DBContext>());
            //services.AddScoped<DBCatalogContext>();

            services.AddDbContext<DBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                //options.UseOpenIddict();
            });

            //Auth0
            //services.AddSingleton<IAuthorizationHandler, HasChannelAuthorizationHandler>();

        }
    }
}
