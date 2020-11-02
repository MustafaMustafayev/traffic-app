using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace traffic_app.DTO
{
    public class UserElasticsearchDTO
    {
        [Required]
        public int Id{ get; set; }
        [Required]
        public string CarNumber{ get; set; }
    }
}
