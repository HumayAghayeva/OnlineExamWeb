
using Abstraction.Interfaces;
using Abstraction.PaymentApi.Interfaces.CardOperations;
using AutoMapper;
using Business.Services;
using Domain.OptionDP;
using Infrastructure.DataContext.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineExamPaymentAPI.DbConn;
using OnlineExamPaymentAPI.Interfaces;
using OnlineExamPaymentAPI.Mapper;
using OnlineExamPaymentAPI.Services;
using OnlineExamPaymentAPI.Services.PaymentApiServices;

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

            services.AddSingleton(mappingConfig.CreateMapper()); 
            #endregion

            // Register DbContexts
            services.AddDbContext<OEPWriteDB>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WriteDbContext")));

            services.AddDbContext<OnlineExamDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OnlineExamDbContext")));

            // Business Services
            services.AddScoped<ICardValidator, CardValidatorServices>();
            services.AddScoped<IPlasticCardServices, PlasticCardServices>();

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddScoped<IJwtTokenService,JwtTokenService>();
            return services;
        }

    }

}
