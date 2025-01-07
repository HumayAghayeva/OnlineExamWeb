﻿using Abstraction.Command;
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

namespace OnlineExamWeb.Utilities
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IStudentCommandRepository, StudentCommandRepository>();
            services.AddSingleton<MongoDBContext>(); 

            services.AddScoped<IMongoCollection<StudentResponseDTO>>(sp =>
            {
                var mongoDbContext = sp.GetRequiredService<MongoDBContext>();
                return mongoDbContext.GetCollection<StudentResponseDTO>("Students");
            });
            services.Configure<EmailSettings>(configuration.GetSection("SenderEmail"));
            services.AddScoped<IEmailOperations, IEmailOperationServices>();
            services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));          
            services.AddScoped<IStudentQueryRepository, StudentQueryRepository>();
            services.AddScoped<ITransferDataToReadDb, ITransferDataToReadDbServices>();
            services.AddScoped<IFileManager, IConfigureImageServices>();       
            return services;
        }
       
    }
}
