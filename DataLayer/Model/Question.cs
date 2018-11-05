using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Model
{
    public class Question : Post
    {
        //public new string Title { get; set; }

        public List<Comment> Comments { get; set; }
       // public Answer Answer { get; set; }
        

    }
}
