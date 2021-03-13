using System;
namespace traffic_app.DTO
{
    public class OnTheWayPassengerPostToListDTO
    {
        public int OnTheWayPassengerPostId { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public string StartDate { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentForEachPassenger { get; set; }
        public int CountOfPassenger { get; set; }
        public string CreatedAt { get; set; }
        public PostedByDTO PostedBy { get; set; }
        public int UserId { get; set; }
        public bool IsOwner { get; set; }
    }
}
