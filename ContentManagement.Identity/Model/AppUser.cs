using Microsoft.AspNetCore.Identity;
using System;

namespace ContentManagement.Identity
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public bool IsProfileCompleted { get; set; }
        public string AvatarUrl { get; set; }
        public string PublicId { get; set; }
        public string ExternalLoginProvider { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Updated_at { get; set; } = DateTime.Now;
    }
}
