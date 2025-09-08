
using Abstraction;
using Abstraction.Interfaces;
using Abstraction.PaymentApi.Interfaces.CardOperations;
using Autofac.Core;
using AutoMapper;
using Business.Services;
using Domain.OptionDP;
using Infrastructure.DataContext.Write;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineExamPaymentAPI.DbConn;
using OnlineExamPaymentAPI.Interfaces;
using OnlineExamPaymentAPI.Mapper;
using OnlineExamPaymentAPI.Services;
using OnlineExamPaymentAPI.Services.PaymentApiServices;
using RabbitMQ.Client;

namespace OnlineExamWebAPI.Utilities
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

            IMapper varOcg = mappingConfig.CreateMapper();
            services.AddSingleton(varOcg);

            #endregion
            services.AddScoped<IUnitOfWork>(provider =>
            {
                var context = provider.GetRequiredService<DbContext>();
                return new UnitOfWork(context);
            });

            // Register DbContexts
            services.AddDbContext<OEPWriteDB>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WriteDbContext")));

            services.AddDbContext<OnlineExamDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OnlineExamDbContext")));


            // Business Services
            services.AddScoped<ICardValidator, CardValidatorServices>();
            services.AddScoped<IPlasticCardServices, PlasticCardServices>();
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

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddScoped<IJwtTokenService,JwtTokenService>();
            return services;
        }

    }

}
