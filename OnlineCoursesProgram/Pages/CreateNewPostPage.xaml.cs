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

    public sealed partial class CreateNewPostPage : Page
    {
        private DatabaseModel db = new DatabaseModel();
        private List<CourseContent> listOfCourses = new List<CourseContent>();
        private Teacher teacher = new Teacher();

        public CreateNewPostPage()
        {
            this.InitializeComponent();
        }

        // Handler for when a selection is made in the combo box
        public void NewCourseSelected(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(((CourseContent)selectedCourseCombo.SelectedItem).CourseName);
        }

        // Handler for post creation button
        public async void SaveNewPost(object sender, RoutedEventArgs e)
        {
            int newPostID = await db.CreateNewPost(teacher.DisplayName, postContentField.Text, DateTime.Now.ToString(), (int)teacher.UserID);
            db.AddPostToCourse((int)((CourseContent)selectedCourseCombo.SelectedItem).CourseID, newPostID);

            ContentDialog successfulCreationDialog = new ContentDialog
            {
                Title = "Post Creation Successful!",
                Content = "Your creation of the post was successful!",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await successfulCreationDialog.ShowAsync();
            this.Frame.Navigate(typeof(ManagePostsPage), teacher);
        }

        // function to load all of the assigned courses for the teacher into the combo box
        private async void LoadCoursesIntoCombo()
        {
            listOfCourses = await db.GetAllAssignedCourses((int)teacher.UserID);
            selectedCourseCombo.ItemsSource = null;
            selectedCourseCombo.ItemsSource = listOfCourses;
        }

        // loading in the user details from the previous page
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            teacher = e.Parameter as Teacher;

            LoadCoursesIntoCombo();

            base.OnNavigatedTo(e);
        }

        // clearing junk from the database upon leaving
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
