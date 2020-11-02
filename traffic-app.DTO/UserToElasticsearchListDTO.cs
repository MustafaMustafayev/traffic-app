using System;
using System.Collections.Generic;
using System.Text;

namespace traffic_app.DTO
{
    public class UserToElasticsearchListDTO
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
    }
}
