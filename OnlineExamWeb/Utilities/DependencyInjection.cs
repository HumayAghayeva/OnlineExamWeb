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

namespace OnlineExamWeb.Utilities
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IStudentCommandRepository, StudentCommandRepository>();
            //services.AddScoped<ITransferDataToReadDb,ITransferDataToReadDbServices > ();
            services.Configure<EmailSettings>(configuration.GetSection("SenderEmail"));
            services.AddScoped<IEmailOperations, IEmailOperationServices>();
            services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));
            // Logger usually Singleton
            //services.AddScoped<IEmailOperations>(provider =>
            //{
            //    var studentQueryRepository = provider.GetRequiredService<IStudentQueryRepository>();
            //    var senderEmail = provider.GetRequiredService<IOptions<EmailSettings>>();

            //    return new IEmailOperationServices(
            //        studentQueryRepository,
            //        senderEmail

            //    );
            //});
            services.AddScoped<IStudentQueryRepository, StudentQueryRepository>();
            services.AddScoped<IFileManager, IConfigureImageServices>();
            services.PostConfigure<EmailSettings>(senderEmail =>
            {
              
            });
            return services;
        }
       
    }
}
