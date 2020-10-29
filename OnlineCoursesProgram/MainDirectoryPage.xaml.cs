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

namespace OnlineCoursesProgram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainDirectoryPage : Page
    {
        CourseContent courseContent = new CourseContent();
        Student student = new Student();
        DatabaseModel db = new DatabaseModel();

        public MainDirectoryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            student = e.Parameter as Student;
            mainFrame.Navigate(typeof(HomePage), student);
            base.OnNavigatedTo(e);
        }

        // Function to save video from a source to a byte array
        //public byte[] SaveToByteStream(string source)
        //{
        //    FileStream stream = File.OpenRead(source);
        //    byte[] buffer = new byte[stream.Length];

        //    stream.Read(buffer, 0, buffer.Length);
        //    stream.Close();

        //    return buffer;
        //}

        // Handler for the "Courses" menu button
        private void Courses_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(HomePage), student);
        }

        // Handler for the "Posts" menu button
        private void Posts_Click(object sender, RoutedEventArgs e)
        {
            List<Post> postList = new List<Post>();

            foreach (CourseContent c in student.CoursesList)
            {
                foreach (Post p in c.PostList)
                {
                    postList.Add(p);
                }
            }

            courseContent.PostList = postList;

            mainFrame.Navigate(typeof(PostsPage), courseContent);
        }

        // Handler for the "Record" menu button
        private void Record_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(RecordingPage));
        }
    }
}
