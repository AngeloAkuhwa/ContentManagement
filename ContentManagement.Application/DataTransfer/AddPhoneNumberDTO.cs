using System.ComponentModel.DataAnnotations;

namespace ContentManagement.Application.DataTransfer
{
    public class AddPhoneNumberDTO
    {
        [Required(ErrorMessage = "input field can not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "input field can not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        public string LastName { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "input field can not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "input field can not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        public string Number { get; set; }
    }
}
