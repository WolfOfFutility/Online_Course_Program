using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCoursesProgram
{
    class Post
    {
        public string Author { get; set; }
        public DateTime DatePosted { get; set; }
        public string TextContent { get; set; }

        public Post()
        {
            // Default
        }

        public Post(string a, DateTime dp, string tc)
        {
            this.Author = a;
            this.DatePosted = dp;
            this.TextContent = tc;
        }
    }
}
