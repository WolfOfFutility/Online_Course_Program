using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;

namespace OnlineCoursesProgram
{
    class User
    {
        public long UserID { get; set; }
        public string DisplayName { get; set; }
        public string Role { get; set; }
    }
}
