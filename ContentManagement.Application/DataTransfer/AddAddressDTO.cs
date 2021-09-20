using System.ComponentModel.DataAnnotations;

namespace ContentManagement.Application.DataTransfer
{
    public class AddAddressDTO
    {

        [Required(ErrorMessage = "input field can not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        public string StreetInfo { get; set; }

        [Required]
        public bool IsContactFilled { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "input field can not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        public string City { get; set; }

        [Required(ErrorMessage = "input field can not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        public string State { get; set; }

        [Required(ErrorMessage = "input field can not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        public string Country { get; set; }
    }
}
