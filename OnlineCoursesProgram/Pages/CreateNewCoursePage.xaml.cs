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

    public sealed partial class CreateNewCoursePage : Page
    {
        private DatabaseModel db = new DatabaseModel();
        private Teacher teacher = new Teacher();

        public CreateNewCoursePage()
        {
            this.InitializeComponent();
        }

        // Handler for the create button, initiates database functions
        public async void CreateNewCourse(object sender, RoutedEventArgs e)
        {
            if(newCourseNameField.Text != "" && newCourseCodeField.Text != "")
            {
                int newCourseID = await db.CreateNewCourse(newCourseCodeField.Text, newCourseNameField.Text);
                db.AssignCourseToUser((int)teacher.UserID, newCourseID);

                ContentDialog successfulCreationDialog = new ContentDialog
                {
                    Title = "Course Creation Successful!",
                    Content = "Your creation of " + newCourseCodeField.Text + " was successful!",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await successfulCreationDialog.ShowAsync();
                this.Frame.Navigate(typeof(ManageCoursesPage), teacher);
            }
        }

        // pulls in user details from the previous navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            teacher = e.Parameter as Teacher;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
