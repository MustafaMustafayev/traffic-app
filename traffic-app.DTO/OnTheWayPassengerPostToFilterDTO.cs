using System;
namespace traffic_app.DTO
{
    public class OnTheWayPassengerPostToFilterDTO
    {
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
