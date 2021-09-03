using Microsoft.AspNetCore.Authorization;

namespace FacilityManagement.Services.API.Policy
{
    /// <summary>
    /// Policies according to roles
    /// </summary>
    public static class Policies
    {
        /// <summary>
        /// Admin role for our policy
        /// </summary>
        public const string Admin = "admin";
        /// <summary>
        /// Decadev role for our policy
        /// </summary>
        public const string GeneralUser = "generalUser";
        /// <summary>
        /// Vendor role for our policy
        /// </summary>
      

        /// <summary>
        /// Grants Admin right to User
        /// </summary>
        /// <returns></returns>
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        /// <summary>
        /// Grants Decadev Access to a User
        /// </summary>
        /// <returns></returns>
        public static AuthorizationPolicy GeneralUserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(GeneralUser).Build();
        }

       
    }
}