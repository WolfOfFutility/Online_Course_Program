using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCoursesProgram
{
    class Student
    {
        public long UserID { get; set; }
        public string Username { get; set; }
        public List<CourseContent> CoursesList { get; set; }

    }
}
