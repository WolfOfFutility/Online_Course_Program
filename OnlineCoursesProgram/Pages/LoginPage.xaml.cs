using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Sockets;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnlineCoursesProgram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private DatabaseModel db = new DatabaseModel();

        public LoginPage()
        {
            this.InitializeComponent();
        }

        // Handler for when the login button is clicked.
        // Only handles the username for now, further verification will come later
        public void Login_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user = db.CheckLoginDetails(usernameField.Text, passwordField.Password);

            if(user == null)
            {
                System.Diagnostics.Debug.WriteLine("Login Failed");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Welcome Back, " + user.DisplayName + "!");

                if (user.Role == "Student")
                {
                    Student student = new Student();
                    student.UserID = user.UserID;
                    student.Username = user.DisplayName;
                    
                    this.Frame.Navigate(typeof(MainDirectoryPage), student);
                    
                    //new ToastContentBuilder()
                    //.AddText("Adaptive Tiles Meeting", hintMaxLines: 1)
                    //.AddText("Conf Room 2001 / Building 135")
                    //.AddText("10:00 AM - 10:30 AM");
                }
                else
                {
                    Teacher teacher = new Teacher();
                    teacher.UserID = user.UserID;
                    teacher.DisplayName = user.DisplayName;

                    //db.AssignCourseToUser((int)teacher.UserID, 1);

                    this.Frame.Navigate(typeof(MainDirectoryPage), teacher);
                }
                
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
