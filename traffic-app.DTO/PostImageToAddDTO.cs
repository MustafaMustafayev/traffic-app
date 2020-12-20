using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace traffic_app.DTO
{
    public class PostImageToAddDTO
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string ImageFullName { get; set; }
        public string ImageExtension { get; set; }
    }
}
