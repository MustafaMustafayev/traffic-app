using System;
using System.Collections.Generic;
using System.Text;

namespace traffic_app.DTO
{
    public class LoginResponseDTO
    {
        public UserToListDTO User { get; set; }
        public string Token { get; set; }
    }
}
