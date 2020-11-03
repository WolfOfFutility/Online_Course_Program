using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace OnlineCoursesProgram
{
    public sealed partial class TeacherHomePage : Page
    {

        DatabaseModel db = new DatabaseModel();
        Teacher teacher = new Teacher();

        public TeacherHomePage()
        {
            this.InitializeComponent();
        }

        // Loading all of the courses that the user is responsible for
        private async void LoadCourses()
        {
            foreach (CourseContent c in await db.GetAllAssignedCourses((int)teacher.UserID))
            {
                System.Diagnostics.Debug.WriteLine(c.CourseName);
            }
        }

        // Closes the database connection when the page is changed
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }

        // pulls a teacher object passed from the menu, and loads all the courses for the user
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            teacher = e.Parameter as Teacher;
            LoadCourses();

            base.OnNavigatedTo(e);
        }
    }
}
