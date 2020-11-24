using System;
using System.Collections.Generic;
using System.Text;

namespace traffic_app.DTO
{
    public class UserToListDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CarNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string UserMail { get; set; }
        public string CreatedAt { get; set; }
    }
}
