using System;
using System.Collections.Generic;
using System.Text;

namespace traffic_app.DTO
{
    public class UserToElasticsearchListDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserMail { get; set; }
        public string CarNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}
