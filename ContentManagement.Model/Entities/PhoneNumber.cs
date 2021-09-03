using ContentManagement.Domain;
using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Domain.Entities
{
    public class PhoneNumber : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FullName { get; set; } 

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public string CountryCode { get; set; }

        [Required]
        public string Number { get; set; }

       
       
    }
}
