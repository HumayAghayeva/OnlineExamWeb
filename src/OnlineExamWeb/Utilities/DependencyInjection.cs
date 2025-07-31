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

            services.AddSingleton(mappingConfig.CreateMapper());    // should i use it ?
            #endregion

            // Register your specific DbContext
            services.AddDbContext<DbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WriteDbContext")));

            services.AddScoped<JwtTokenService>();
            // Use the specific DbContext in the UnitOfWork registration
            services.AddScoped<IUnitOfWork>(provider =>
            {
                var context = provider.GetRequiredService<DbContext>(); // Replace with your specific DbContext
                return new UnitOfWork(context);
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IStudentCommandRepository, StudentCommandRepository>();
            services.AddSingleton<MongoDBContext>(); 

            services.AddScoped<IMongoCollection<StudentResponseDto>>(sp =>
            {
                var mongoDbContext = sp.GetRequiredService<MongoDBContext>();
                return mongoDbContext.GetCollection<StudentResponseDto>("Students");
            });
            services.Configure<EmailSettings>(configuration.GetSection("SenderEmail"));
            services.AddScoped<IEmailOperations, IEmailOperationServices>();        
            services.AddScoped<IStudentQueryRepository, StudentQueryRepository>();
            services.AddScoped<ITransferDataToReadDb, ITransferDataToReadDbServices>();
            services.AddScoped<IFileManager, IConfigureImageServices>();
   
            //services.AddSingleton<IHostedService, TransferDataFromWriteToRead>();

            //Validators
            services.AddScoped<IValidator<StudentRequestDto>, StudentValidator>();

           
            return services;
        }
       
    }
}
