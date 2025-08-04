
using Abstraction.PaymentApi.Interfaces.CardOperations;
using AutoMapper;
using Domain.OptionDP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineExamPaymentAPI.DbConn;
using OnlineExamPaymentAPI.Mapper;
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

            services.AddSingleton(mappingConfig.CreateMapper());    // should i use it ?
            #endregion

            services.AddDbContext<OnlineExamDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("OnlineExamDbContext")));
            services.AddScoped<ICardValidator, CardValidatorServices>();


            return services;
        }
       
    }
}
