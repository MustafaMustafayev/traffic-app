using System;
using System.Collections.Generic;
using System.Text;

namespace traffic_app.DTO
{
    public class OnTheWayDriverPostToFilterDTO
    {
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime StartDate { get; set; }
    }
}
