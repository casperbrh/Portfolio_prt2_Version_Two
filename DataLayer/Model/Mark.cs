using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Model
{
     public class Mark
    {
        [ForeignKey("Mark")]
        public int PostId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Post posts { get; set; }
        public User User { get; set; }
    }
}
