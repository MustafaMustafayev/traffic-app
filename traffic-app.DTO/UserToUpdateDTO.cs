﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace traffic_app.DTO
{
    public class UserToUpdateDTO
    {
        [Required]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserMail { get; set; }
        public string CarNumber { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
