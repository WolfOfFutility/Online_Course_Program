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

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Courses_Click(object sender, RoutedEventArgs e)
        {
            List<ClassContent> classList = new List<ClassContent>();
            ClassContent c1 = new ClassContent();
            c1.Source = "ms-appx:///Assets/testvideo2.mp4";
            c1.ClassName = "Lesson 1";
            classList.Add(c1);

            ClassContent c2 = new ClassContent();
            c2.Source = "ms-appx:///Assets/testingvideo.mp4";
            c2.ClassName = "Lesson 2";
            classList.Add(c2);

            CourseContent courseContent = new CourseContent();
            courseContent.CourseName = "Test Course 1";
            courseContent.CourseCode = "TEST101";
            courseContent.ClassList = classList;
            
            mainFrame.Navigate(typeof(ClassListPage), courseContent);
        }

        private void Posts_Click(object sender, RoutedEventArgs e)
        {
            List<Post> postList = new List<Post>();

            Post post1 = new Post();
            post1.Author = "Teacher Name 1";
            post1.DatePosted = DateTime.Now;
            post1.TextContent = "Hello there!";

            Post post2 = new Post();
            post2.Author = "Teacher Name 2";
            post2.DatePosted = DateTime.Now;
            post2.TextContent = "Testing second post!";

            postList.Add(post1);
            postList.Add(post2);

            courseContent.PostList = postList;

            mainFrame.Navigate(typeof(PostsPage), courseContent);
        }
    }
}
