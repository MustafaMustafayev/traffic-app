using System;
using System.ComponentModel.DataAnnotations;

namespace traffic_app.DTO
{
    public class OnTheWayPassengerPostToAddDTO
    {
        [Required]
        public string FromPlace { get; set; }
        [Required]
        public string ToPlace { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string PaymentForEachPassenger { get; set; }
        [Required]
        public int CountOfPassenger { get; set; }
    }
}
