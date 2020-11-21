using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCoursesProgram
{
    class CourseContent
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public List<ClassContent> ClassList { get; set; }
        public List<Post> PostList { get; set; }

        public CourseContent()
        {

        }

        public CourseContent(int cid, string cn, string cc)
        {
            this.CourseID = cid;
            this.CourseName = cn;
            this.CourseCode = cc;
        }
    }
}
