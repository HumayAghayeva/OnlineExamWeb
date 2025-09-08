using Abstraction.Command;
using Abstraction.Queries;
using Abstraction;
using Business.Repositories.Command;
using Business.Repositories;
using Infrastructure.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Abstraction.Interfaces;
using Business.Services;
using Domain.OptionDP;
using Microsoft.Extensions.Options;
using OnlineExamWeb.Controllers;
using Infrastructure.Persistent.Read;
using Domain.DTOs.Read;
using MongoDB.Driver;
using Business.BackGroundServices;
using FluentValidation;
using System;
using Business.ValidationRules.FluentValidations.StudentValidator;
using Domain.DTOs.Write;
using Microsoft.EntityFrameworkCore;
using Hazelcast;
using Business.Mapper;
using AutoMapper;
using System.Configuration;
using OnlineExamPaymentAPI.Interfaces;
using OnlineExamPaymentAPI.Services;
using OnlineExamPaymentAPI.DbConn;
using Infrastructure.DataContext.Write;
using RabbitMQ.Client;

namespace OnlineExamWeb.Utilities
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfileDto());
            });

            services.AddSingleton(mappingConfig.CreateMapper()); // yes, keep it if you need AutoMapper globally
            #endregion

            // Register your specific DbContexts instead of raw DbContext
            services.AddDbContext<OEPWriteDB>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WriteDbContext")));

         

            services.AddDbContext<OnlineExamDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OnlineExamDbContext")));

            // JWT config
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // Unit of Work with Write DB
            services.AddScoped<IUnitOfWork>(provider =>
            {
                var context = provider.GetRequiredService<OEPWriteDB>();
                return new UnitOfWork(context);
            });

            // Generic repository + specific repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IStudentCommandRepository, StudentCommandRepository>();
            services.AddScoped<IStudentQueryRepository, StudentQueryRepository>();
            services.AddScoped<ITransferDataToReadDb, ITransferDataToReadDbServices>();
            services.AddScoped<IFileManager, IConfigureImageServices>();
            services.AddScoped<IPlasticCardServices, PlasticCardServices>();

            // Mongo
            services.AddSingleton<MongoDBContext>();
            services.AddScoped<IMongoCollection<StudentResponseDto>>(sp =>
            {
                var mongoDbContext = sp.GetRequiredService<MongoDBContext>();
                return mongoDbContext.GetCollection<StudentResponseDto>("Students");
            });

            // Email
            services.Configure<EmailSettings>(configuration.GetSection("SenderEmail"));
            services.AddScoped<IEmailOperations, IEmailOperationServices>();

            // Validators
            services.AddScoped<IValidator<StudentRequestDto>, StudentValidator>();
            services.AddSingleton<IConnectionFactory>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return new ConnectionFactory()
                {
                    HostName = config["RabbitMQ:Connection"],
                    UserName = config["RabbitMQ:UserName"],
                    Password = config["RabbitMQ:Password"],
                    Port = int.Parse(config["RabbitMQ:Port"] ?? "5672"),
                    DispatchConsumersAsync = true
                };
            });
            services.AddTransient<IPublisher, Publisher>();
            services.AddTransient<IConsumer, Consumer>();
            // Hosted service if needed
            // services.AddSingleton<IHostedService, TransferDataFromWriteToRead>();

            return services;
        }

        private static void servicesAddSingleton<T>(Func<object, ConnectionFactory> value)
        {
            throw new NotImplementedException();
        }
    }
}
