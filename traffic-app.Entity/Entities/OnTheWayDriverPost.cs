using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace traffic_app.Entity.Entities
{
    public class OnTheWayDriverPost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int OnTheWayDriverPostId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
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
        [Required]
        public bool CanTakeLuggage { get; set; }
        [Required]
        public int CountOfEmptyPlace { get; set; }
        [Required]
        public string CarModel { get;set; }
        public string CarImageUrl { get;set; }
        [Required]
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
