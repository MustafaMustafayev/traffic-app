using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace traffic_app.DTO
{
    public class MessageToAddDTO
    {
        [Required]
        public string Text { get; set; }
    }
}
