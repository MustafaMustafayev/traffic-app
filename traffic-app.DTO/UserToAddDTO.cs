using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace traffic_app.DTO
{
    public class UserToAddDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        //[MinLength(9)]
        //[MaxLength(9)]
        public string CarNumber { get; set; }
        [Required]
        //[MinLength(9)]
        //[MaxLength(9)]
        public string PhoneNumber { get; set; }
    }
}
