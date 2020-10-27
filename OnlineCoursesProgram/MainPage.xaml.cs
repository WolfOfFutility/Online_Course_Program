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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OnlineCoursesProgram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CourseContent courseContent = new CourseContent();
        Student student = new Student();

        // Initialising Main frame and Data
        public MainPage()
        {
            this.InitializeComponent();
            LoadData();
            mainFrame.Navigate(typeof(HomePage), student);
        }

        // Function to save video from a source to a byte array
        public byte[] SaveToByteStream(string source)
        {
            FileStream stream = File.OpenRead(source);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            return buffer;
        }

        // Loading data into class object instances, so thtat they can be passed into a frame navigation function
        // This functionality will be modified to accept SQL data reading instead of hardcoded class creation, once an SQL backend is set up
        public void LoadData()
        {
            // List of classes initialisation
            List<ClassContent> classList = new List<ClassContent>();
            
            // First class
            ClassContent c1 = new ClassContent();
            c1.Source = SaveToByteStream(@"Assets/testvideo2.mp4");
            c1.ClassName = "Lesson 1";
            classList.Add(c1);

            // Second class
            ClassContent c2 = new ClassContent();
            c2.Source = SaveToByteStream(@"Assets/testingvideo.mp4");
            c2.ClassName = "Lesson 2";
            classList.Add(c2);

            // Third class
            ClassContent c3 = new ClassContent();
            c3.Source = SaveToByteStream(@"Assets/testvideo3.mp4");
            c3.ClassName = "Lesson 3";
            classList.Add(c3);

            //ClassContent c4 = new ClassContent();
            //c4.Source = SaveToByteStream(@"Assets/recorded-video-1.mp4");
            //c4.ClassName = "Testing creating and viewing a video";
            //classList.Add(c4);

            // List of posts initialisation
            List<Post> postList = new List<Post>();

            // First post
            Post post1 = new Post();
            post1.Author = "Teacher Name 1";
            post1.DatePosted = DateTime.Now;
            post1.TextContent = "Hello there!";

            // Second post
            Post post2 = new Post();
            post2.Author = "Teacher Name 2";
            post2.DatePosted = DateTime.Now;
            post2.TextContent = "Testing second post!";

            // Adding all the posts to the post list
            postList.Add(post1);
            postList.Add(post2);

            // Initialising a new course, adding classes and posts to it
            CourseContent courseContent = new CourseContent();
            courseContent.CourseName = "Test Course 1";
            courseContent.CourseCode = "TEST101";
            courseContent.ClassList = classList;
            courseContent.PostList = postList;

            List<ClassContent> classList1 = new List<ClassContent>();
            classList1.Add(c1);
            classList1.Add(c2);

            // Initialising another course
            CourseContent courseContent1 = new CourseContent();
            courseContent1.ClassList = classList1;
            courseContent1.CourseName = "Test Course 2";
            courseContent1.CourseCode = "TEST11022";

            List<Post> postList1 = new List<Post>();
            postList1.Add(post1);

            courseContent1.PostList = postList1;

            List<CourseContent> courseList = new List<CourseContent>();
            courseList.Add(courseContent);
            courseList.Add(courseContent1);

            // Adding details to the student object initialised at the start of the class, appending the list of courses that the test student is enrolled in
            student.CoursesList = courseList;
            student.UserID = 123456789;
            student.Username = "Ceejay";
        }

        // Handler for the "Courses" menu button
        private void Courses_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
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
