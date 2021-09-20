using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ContentManagement.Application.DataTransfer
{
    public class AddImageDTO
    {
        [Required(ErrorMessage = "input field can not be empty")]
        public IFormFile Image { get; set; }
    }
}
