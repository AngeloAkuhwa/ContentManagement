using ContentManagement.Application.Mapper;
using ContentManagement.Identity.IdentityContext;
using ContentManagement.Identity.IdentityServiceSetup;
using ContentManagement.Identity.RoleSeeder;
using ContentManagement.Infrastructure.Persistence;
using ContentManagement.Infrastructure.ServiceExtensionsConfiguring;
using ContentManagement.Presentation.ExceptionHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContentManagement.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddIdentityDbServices(Configuration);
            services.AddContactDbService(Configuration);
            services.AddDependencyInjection();
            services.AddAuthenticationConfiguring(Configuration);
            services.AddAuthorizationConfiguring();
            services.AddCloudinaryccountSettings(Configuration);
            services.AddConfigureSwagger();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, IWebHostEnvironment env, 
            ApplicationDbContext identityContext, ContactDbContext contactContext,
            RoleManager<IdentityRole> roleManager
            )
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleWare>();

            app.UseSwagger();

            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "ContentManagement.Presentation"));

            app.UseHttpsRedirection();

            identityContext.Database.MigrateAsync().Wait();

            contactContext.Database.MigrateAsync().Wait();

            Seeder.SeedValues(roleManager, identityContext).Wait();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
