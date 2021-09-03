﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CloudinaryImageCrudHandler.DTO
{
    public class AddImageDTO
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
