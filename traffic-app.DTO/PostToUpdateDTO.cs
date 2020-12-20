using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace traffic_app.DTO
{
    public class PostToUpdateDTO
    {
        [Required]
        public int PostId { get; set; }
        public string PostText { get; set; }
    }
}
