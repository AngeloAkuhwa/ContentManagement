using ContactManagement.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManagement.Persistence.ContactContextSetup
{
    public static class ContactContextSetup
    {
        public static void AddContactDbService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContactDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ContactConnection"),
                getAssembly => getAssembly.MigrationsAssembly(typeof(ContactDbContext).Assembly.FullName)));
        }
    }
}
