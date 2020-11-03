using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class PostsPage : Page
    {
        // Initialising Instance Variables
        CourseContent course = new CourseContent();
        List<Post> postList = new List<Post>();

        DatabaseModel db = new DatabaseModel();

        public PostsPage()
        {
            this.InitializeComponent();
        }

        // Handling object passing when the frame navigates
        // Creates an individual post box, showing who posted it, when, and the post content
        // A student will see posts made for the courses that they are currently enrolled in

        // **NOTE** Needs to be redesigned to look better graphically, and to present images, etc
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            course = e.Parameter as CourseContent;
            postList = new List<Post>();
            postList = course.PostList;

            foreach(Post x in postList)
            {
                Grid g = new Grid();
                g.MinHeight = 300;
                g.Margin = new Thickness(50, 25, 50, 20);
                g.Background = new SolidColorBrush(Colors.CadetBlue);

                RowDefinition row1 = new RowDefinition();
                row1.Height = new GridLength(1, GridUnitType.Star);
                RowDefinition row2 = new RowDefinition();
                row2.Height = new GridLength(5, GridUnitType.Star);

                g.RowDefinitions.Add(row1);
                g.RowDefinitions.Add(row2);

                StackPanel stackPanel = new StackPanel();
                stackPanel.Background = new SolidColorBrush(Colors.DodgerBlue);
                stackPanel.Orientation = Orientation.Horizontal;

                TextBlock textBlock = new TextBlock();
                textBlock.Text = x.Author + " at " + x.DatePosted.ToString();
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Margin = new Thickness(10, 0, 0, 0);
                textBlock.FontSize = 20;

                stackPanel.Children.Add(textBlock);
                g.Children.Add(stackPanel);
                Grid.SetRow(stackPanel, 0);

                TextBlock textBlock1 = new TextBlock();
                textBlock1.Text = x.TextContent;
                textBlock1.Margin = new Thickness(10);
                textBlock1.FontSize = 20;

                g.Children.Add(textBlock1);
                Grid.SetRow(textBlock1, 1);

                postListPanel.Children.Add(g);
            }

            base.OnNavigatedTo(e);
        }

        // Overrides the on navigate to, in order to close the database connection when navigated away
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
