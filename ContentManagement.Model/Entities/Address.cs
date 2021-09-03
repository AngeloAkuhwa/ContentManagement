using ContentManagement.Domain;
using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Domain.Entities
{
    public class Address:BaseEntity
    {

        [Required]
        public string UserId { get; set; }

        [Required]
        public string StreetInfo { get; set; }

        [Required]
        public bool IsContactFilled { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
