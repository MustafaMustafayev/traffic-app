using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace traffic_app.DTO
{
    public class UserChangePasswordDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifrə düzgün daxil edilməyib")]
        [Compare("Password")]
        public string ConfirmPassowrd { get; set; }
    }
}
