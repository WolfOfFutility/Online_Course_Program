using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnlineCoursesProgram
{
    /// <summary>
    /// A page that renders a list of classes for the given course
    /// Each class, when clicked on, will load the related video and information
    /// </summary>
    public sealed partial class ClassListPage : Page
    {
        // Initialising instance variables
        CourseContent course = new CourseContent();
        List<ClassContent> classList = new List<ClassContent>();
        DatabaseModel db = new DatabaseModel();


        public ClassListPage()
        {
            this.InitializeComponent();
        }

        // Loading a list of all of the classes in the course passed to the page in the navigation function
        public async void LoadClasses()
        {
            foreach (ClassContent x in await db.GetCourseClassList(course.CourseID))
            {
                Button b = new Button();
                b.Margin = new Thickness(1, 0.5, 1, 0.5);
                //b.CornerRadius = new CornerRadius(10);
                b.HorizontalAlignment = HorizontalAlignment.Stretch;
                b.HorizontalContentAlignment = HorizontalAlignment.Left + 30;
                b.Height = 50;
                b.Foreground = new SolidColorBrush(Colors.Black);
                b.Background = new SolidColorBrush(Colors.LightBlue);
                b.Content = course.CourseCode + " - " + x.ClassName;
                b.Click += (s, e) => { subFrame.Navigate(typeof(IndivClassPage), x, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft }); };

                classListPanel.Children.Add(b);
            }
        }

        // Handling object passing through navigation function
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            course = e.Parameter as CourseContent;

            LoadClasses();

            base.OnNavigatedTo(e);
        }
    }
}
