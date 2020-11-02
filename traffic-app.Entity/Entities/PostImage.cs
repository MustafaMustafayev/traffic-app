using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace traffic_app.Entity.Entities
{
    public class PostImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PostImageId { get; set; }
        public virtual Post Post { get; set; }
        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string ImageFullName { get; set; }
        public string ImageExtension { get; set; }
    }
}
