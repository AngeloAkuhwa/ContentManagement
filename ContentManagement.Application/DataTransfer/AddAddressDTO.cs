using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContentManagement.Application.DataTransfer
{
    public class AddAddressDTO
    {

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
