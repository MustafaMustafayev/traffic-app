using System;
using System.Collections.Generic;
using System.Text;

namespace traffic_app.DTO
{
    public class PostToListDTO
    {
        public int PostId { get; set; }
        public string PostText { get; set; }
        public List<PostImageToListDTO> PostImages { get; set; }
        public int Owner { get; set; }
        public bool isOwner { get; set; }
        public string UpdatedAt { get; set; }
    }
}
