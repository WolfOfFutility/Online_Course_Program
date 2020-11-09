using OnlineCoursesProgram.Pages;
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
    public sealed partial class HomePage : Page
    {
        DatabaseModel db = new DatabaseModel();
        public HomePage()
        {
            this.InitializeComponent();
        }

        // Handling object passing on frame navigation, loads a list of courses that the student is currently enrolled in
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Student student = e.Parameter as Student;
            subFrame1.Navigate(typeof(EnrollPromptsPage), student);

            foreach (CourseContent c in await db.ViewAllUserCourses((int)student.UserID))
            {
                Button b = new Button();
                b.Content = c.CourseName;
                b.HorizontalAlignment = HorizontalAlignment.Stretch;
                b.Height = 100;
                b.Background = new SolidColorBrush(Windows.UI.Colors.DodgerBlue);
                b.Margin = new Thickness(1, 0.5, 1, 0.5);
                b.Click += (s, n) => { subFrame1.Navigate(typeof(ClassListPage), c); };

                courseListPanel.Children.Add(b);
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
