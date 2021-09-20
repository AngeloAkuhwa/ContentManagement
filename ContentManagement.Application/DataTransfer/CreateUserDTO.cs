using System.ComponentModel.DataAnnotations;

namespace ContentManagement.Application.DataTransfer
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "input field can not be empty"), MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        [DataType(DataType.Text)]
        [Display(Name = "Student Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "input field can not be empty"), MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "input field can not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only letters are required")]
        public string Gender { get; set; }

        public bool IsActive { get; set; }

        public bool IsProfileCompleted { get; set; }

        public string AvatarUrl { get; set; }

        public string PublicId { get; set; }
        [Required(ErrorMessage = "input field can not be empty")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "please input a valid phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "input field can not be empty")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        public string UserName { get { return Email; } }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
