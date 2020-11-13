using OnlineCoursesProgram.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Printing3D;
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
        private CourseContent courseContent = new CourseContent();
        private Student student = new Student();
        private Teacher teacher = new Teacher();
        private DatabaseModel db = new DatabaseModel();
        private bool isStudent = false;
        private bool isTeacher = false;

        public MainDirectoryPage()
        {
            this.InitializeComponent();
        }

        private void LoadStudentMenu()
        {
            // Courses Button
            Button coursesButton = new Button();
            coursesButton.VerticalAlignment = VerticalAlignment.Stretch;
            coursesButton.Width = 100;
            coursesButton.Content = "Courses";
            coursesButton.Click += (s, e) =>
            {
                Courses_Click(s, e);
            };

            // Posts Button
            Button postsButton = new Button();
            postsButton.VerticalAlignment = VerticalAlignment.Stretch;
            postsButton.Width = 100;
            postsButton.Content = "Posts";
            postsButton.Click += (s, e) =>
            {
                Posts_Click(s, e);
            };

            menuButtonPanel.Children.Add(coursesButton);
            menuButtonPanel.Children.Add(postsButton);
        }

        private void LoadTeacherMenu()
        {
            // Record Button
            Button recordButton = new Button();
            recordButton.VerticalAlignment = VerticalAlignment.Stretch;
            recordButton.Width = 100;
            recordButton.Content = "Record";
            recordButton.Click += (s, e) =>
            {
                Record_Click(s, e);
            };

            // New Post Button
            Button newPostButton = new Button();
            newPostButton.VerticalAlignment = VerticalAlignment.Stretch;
            newPostButton.Width = 200;
            newPostButton.Content = "Manage Posts";
            newPostButton.Click += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("New Post Button Clicked");
            };

            // Manage Courses Button
            Button manageCoursesButton = new Button();
            manageCoursesButton.VerticalAlignment = VerticalAlignment.Stretch;
            manageCoursesButton.Width = 200;
            manageCoursesButton.Content = "Manage Courses";
            manageCoursesButton.Click += (s, e) =>
            {
                Manage_Courses_Click(s, e);
            };

            menuButtonPanel.Children.Add(recordButton);
            menuButtonPanel.Children.Add(newPostButton);
            menuButtonPanel.Children.Add(manageCoursesButton);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is Student)
            {
                LoadStudentMenu();
                System.Diagnostics.Debug.WriteLine("Student Object");
                student = e.Parameter as Student;
                mainFrame.Navigate(typeof(HomePage), student);
                isStudent = true;
            }
            else
            {
                LoadTeacherMenu();
                System.Diagnostics.Debug.WriteLine("Teacher Object");
                teacher = e.Parameter as Teacher;
                mainFrame.Navigate(typeof(TeacherHomePage), teacher);
                isTeacher = true;
            }

            
            base.OnNavigatedTo(e);
        }

        // Handler for the "Courses" menu button
        private void Courses_Click(object sender, RoutedEventArgs e)
        {
            if(isStudent)
            {
                mainFrame.Navigate(typeof(HomePage), student);
            }
            else
            {
                mainFrame.Navigate(typeof(TeacherHomePage), teacher);
            }
            
        }

        // Handler for the "Posts" menu button
        private async void Posts_Click(object sender, RoutedEventArgs e)
        {
            if(isStudent)
            {
                List<Post> postList = new List<Post>();

                foreach (CourseContent c in await db.ViewAllUserCourses((int)student.UserID))
                {
                    foreach (Post p in await db.GetAllPostsForCourse(1))
                    {
                        postList.Add(p);
                    }
                }

                courseContent.PostList = postList;

                mainFrame.Navigate(typeof(PostsPage), courseContent);
            }
        }

        // Handler for the "Record" menu button
        private void Record_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("User ID At Directory Page: " + teacher.UserID);
            mainFrame.Navigate(typeof(RecordingPage), teacher);
        }

        // Handler for the "Manage Courses" menu button
        private void Manage_Courses_Click(object sender, RoutedEventArgs e)
        {
            if(!isStudent)
            {
                mainFrame.Navigate(typeof(ManageCoursesPage), teacher);
            }
        }
    }
}
