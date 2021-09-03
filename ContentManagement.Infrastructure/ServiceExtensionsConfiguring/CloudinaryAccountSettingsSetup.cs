using ContentManagement.Application.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContentManagement.Infrastructure.ServiceExtensionsConfiguring
{
    public  static class CloudinaryAccountSettingsSetup
    {
        public static void AddCloudinaryccountSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AccountSettings>(configuration.GetSection("AccountSettings"));
        }
    }
}
