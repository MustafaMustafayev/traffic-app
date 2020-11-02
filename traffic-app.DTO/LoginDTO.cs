
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace traffic_app.DTO
{
    public class LoginDTO
    {
        [Required]
        //[MinLength(9)]
        //[MaxLength(9)]
        public string CarNumber { get; set; }
        [Required]
        //[MinLength(13)]
        //[MaxLength(13)]
        public string PhoneNumber { get; set; }
    }
}
