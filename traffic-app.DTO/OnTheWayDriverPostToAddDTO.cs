using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace traffic_app.DTO
{
    public class OnTheWayDriverPostToAddDTO
    {
        [Required]
        public string FromPlace { get; set; }
        [Required]
        public string ToPlace { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public bool CanSmoke { get; set; }
        [Required]
        public string Payment { get; set; }
        public bool CanTakeLuggage { get; set; }
        [Required]
        public int CountOfEmptyPlace { get; set; }
        [Required]
        public string CarModel { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public IFormFile file { get; set; }
    }
}
