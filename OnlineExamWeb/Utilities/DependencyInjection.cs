using Abstraction.Command;
using Abstraction.Queries;
using Abstraction;
using Business.Repositories.Command;
using Business.Repositories;
using Infrastructure.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Abstraction.Interfaces;
using Business.Services;

namespace OnlineExamWeb.Utilities
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IStudentCommandRepository, StudentCommandRepository>();
            services.AddScoped<IStudentQueryRepository, StudentQueryRepository>();
            services.AddScoped<IEmailOperations, IEmailOperationServices>();
            return services;
        }
       
    }
}
