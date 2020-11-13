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

        // Instance variables initialisation
        private CourseContent currentCourse = new CourseContent();
        private List<int> idList = new List<int>();

        // ** NOTE ** Need to implement a course author and course description throughout the application
        private string courseName = "Testing Course Name";
        private string authorName = "Testing Author Name";
        private string courseDesc = "Testing Course Description";

        private DatabaseModel db = new DatabaseModel();

        public IndivEnrollPage()
        {
            this.InitializeComponent();
        }
        
        // simple handler for backing
        public async void BackHandler(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EnrollPromptsPage), await db.GetStudentByID(idList[0]));
        }

        // Handler for enrolling
        // Opens a dialog box to say when enrollment has been successful and navigates back to the home page
        public async void EnrollHandler(object sender, RoutedEventArgs e)
        {
            int confirmed = await db.EnrollInCourse(idList[0], idList[1]);

            if(confirmed > 0)
            {
                System.Diagnostics.Debug.WriteLine("Enrolled in Course Successfully.");
                ContentDialog successfulEnrollDialog = new ContentDialog
                {
                    Title = "Enrollment Successful!",
                    Content = "Your enrollment in " + courseName + " was successful!",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await successfulEnrollDialog.ShowAsync();
                this.Frame.Navigate(typeof(EnrollPromptsPage), await db.GetStudentByID(idList[0]));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Enroll Failure.");
            }
        }

        // navigates to and pulls the userID and the CourseID and applies them across the rest of the page
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            idList = e.Parameter as List<int>;

            currentCourse = await db.GetCourseByID(idList[1]);
            courseName = currentCourse.CourseName;

            base.OnNavigatedTo(e);
        }

        // Cleans up the database connection and navigates away
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
