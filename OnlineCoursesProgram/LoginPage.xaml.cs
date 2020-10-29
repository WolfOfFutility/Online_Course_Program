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
        DatabaseModel db = new DatabaseModel();

        public LoginPage()
        {
            this.InitializeComponent();
            //db.CreateNewUser(4, "Test1", "pass", "TestingAccount", "Student");
            //db.CreateNewClass(1, "Testing Class", SaveToByteStream(@"Assets/testvideo3.mp4"));
            //db.CreateNewCourse(1, "Test110111", "Testing Course");
            db.ViewAllUserCourses(1);
        }

        // Handler for when the login button is clicked.
        // Only handles the username for now, further verification will come later
        public void Login_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student();

            if(db.CheckLoginDetails(usernameField.Text, passwordField.Text) == null)
            {
                System.Diagnostics.Debug.WriteLine("Login Failed");
            }
            else
            {
                student = db.CheckLoginDetails(usernameField.Text, passwordField.Text);
                System.Diagnostics.Debug.WriteLine("Welcome Back, " + student.Username + "!");
                this.Frame.Navigate(typeof(MainDirectoryPage), student);
            }
        }
    }
}
