using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCoursesProgram
{
    class Teacher
    {
        public long UserID { get; set; }
        public string DisplayName { get; set; }
        public List<CourseContent> ResponsibleCourses { get; set; }

    }
}
