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
        public HomePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Student student = e.Parameter as Student;
            foreach(CourseContent c in student.CoursesList)
            {
                Button b = new Button();
                b.Content = c.CourseName;
                b.HorizontalAlignment = HorizontalAlignment.Stretch;
                b.Height = 100;
                b.Background = new SolidColorBrush(Windows.UI.Colors.DodgerBlue);
                b.Margin = new Thickness(0, 0, 0, 1);
                b.Click += (s, n) => { subFrame1.Navigate(typeof(ClassListPage), c); };

                courseListPanel.Children.Add(b);
            }

            base.OnNavigatedTo(e);
        }
    }
}
