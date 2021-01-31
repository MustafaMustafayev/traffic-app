using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace traffic_app.DTO
{
    public class OnTheWayDriverPostToListDTO
    {
        public int OnTheWayDriverPostId { get; set; }
        public PostedByDTO PostedBy { get; set; }
        public int UserId { get; set; }
        public bool IsOwner { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public string StartDate { get; set; }
        public bool CanSmoke { get; set; }
        public string Payment { get; set; }
        public bool CanTakeLuggage { get; set; }
        public int CountOfEmptyPlace { get; set; }
        public string CarModel { get; set; }
        public string CarImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedAt { get; set; }
    }
}
