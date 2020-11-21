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


namespace OnlineCoursesProgram.Pages
{

    public sealed partial class ManageCoursesPage : Page
    {
        private DatabaseModel db = new DatabaseModel();
        private Teacher teacher = new Teacher();
        private List<CourseContent> listOfOwnedCourses = new List<CourseContent>();

        public ManageCoursesPage()
        {
            this.InitializeComponent();
        }

        // Handles the list selection being changed 
        public void ChangedListSelection(object sender, RoutedEventArgs e)
        {
            CourseContent selectedCourse = new CourseContent();
            selectedCourse = (CourseContent)ownedCoursesListBox.SelectedItem;
            System.Diagnostics.Debug.WriteLine(selectedCourse.CourseName);
        }

        // loads all of the courses owned by the current user
        private async void LoadOwnedCourses(int userID)
        {
            listOfOwnedCourses = await db.GetAllAssignedCourses((int)teacher.UserID);
        }

        // handles the button click for "Create new course"  button
        public void NavigateToCreateCourse(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateNewCoursePage), teacher);
        }

        // handles being navigated to in frame, takes neccesary user data
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            teacher = e.Parameter as Teacher;
            LoadOwnedCourses((int)teacher.UserID);

            base.OnNavigatedTo(e);
        }

        // handles navigation from, handles clean up of database connection
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
