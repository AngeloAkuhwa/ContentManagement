using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContentManagement.Application.DataTransfer
{
    public class AddPhoneNumberDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public string CountryCode { get; set; }

        [Required]
        public string Number { get; set; }
    }
}
