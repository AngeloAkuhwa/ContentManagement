using ContentManagement.Application.Contracts;
using ContentManagement.Application.Persistence.Repositories.Interfaces;
using ContentManagement.Infrastructure.Repositories;
using ContentManagement.Infrastructure.ServiceImplementations;
using Microsoft.Extensions.DependencyInjection;

namespace ContentManagement.Infrastructure.ServiceExtensionsConfiguring
{
    public static class DIContainer
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IIMageService, ImageService>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            services.AddScoped<IDapperRepository, DapperRepository>();
            services.AddScoped<IPhoneNumberService, PhoneNumberService>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAddressService, AddressService>();
        }
    }
}
