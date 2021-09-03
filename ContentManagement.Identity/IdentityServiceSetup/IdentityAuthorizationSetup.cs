using FacilityManagement.Services.API.Policy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentManagement.Identity.IdentityServiceSetup
{
         /// <summary>
        /// AuthorizationServiceExtensions class
        /// </summary>
        public static class IdentityAuthorizationSetup
        {
            /// <summary>
            /// AddAuthorizationConfiguring method
            /// </summary>
            /// <param name="services"></param>
            public static void AddAuthorizationConfiguring(this IServiceCollection services)
            {
                services.AddAuthorization(config =>
                {
                    config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                    config.AddPolicy(Policies.GeneralUser, Policies.GeneralUserPolicy());
                });
            }
        }
}
