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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnlineCoursesProgram.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IndivEnrollPage : Page
    {

        private CourseContent currentCourse = new CourseContent();
        private List<int> idList = new List<int>();

        private string courseName = "Testing Course Name";
        private string authorName = "Testing Author Name";
        private string courseDesc = "Testing Course Description";

        private DatabaseModel db = new DatabaseModel();

        public IndivEnrollPage()
        {
            this.InitializeComponent();
        }
        
        // Handler for backing
        public async void BackHandler(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EnrollPromptsPage), await db.GetStudentByID(idList[0]));
        }

        // Handler for enrolling
        public async void EnrollHandler(object sender, RoutedEventArgs e)
        {
            int confirmed = await db.EnrollInCourse(idList[0], idList[1]);

            if(confirmed > 0)
            {
                System.Diagnostics.Debug.WriteLine("Enrolled in Course Successfully.");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Enroll Failure.");
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            idList = e.Parameter as List<int>;

            currentCourse = await db.GetCourseByID(idList[1]);
            courseName = currentCourse.CourseName;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
