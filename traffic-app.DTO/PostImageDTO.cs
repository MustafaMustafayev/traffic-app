using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace traffic_app.DTO
{
    public class PostImageDTO
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public IFormFile file { get; set; }
    }
}
