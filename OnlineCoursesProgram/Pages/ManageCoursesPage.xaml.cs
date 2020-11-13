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
    public sealed partial class ManageCoursesPage : Page
    {
        private DatabaseModel db = new DatabaseModel();
        private Teacher teacher = new Teacher();
        private List<CourseContent> listOfOwnedCourses = new List<CourseContent>();

        public ManageCoursesPage()
        {
            this.InitializeComponent();
        }

        public void ChangedListSelection(object sender, RoutedEventArgs e)
        {
            CourseContent selectedCourse = new CourseContent();
            selectedCourse = (CourseContent)ownedCoursesListBox.SelectedItem;
            System.Diagnostics.Debug.WriteLine(selectedCourse.CourseName);
        }

        private async void LoadOwnedCourses(int userID)
        {
            listOfOwnedCourses = await db.GetAllCourses();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            teacher = e.Parameter as Teacher;
            LoadOwnedCourses((int)teacher.UserID);

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
