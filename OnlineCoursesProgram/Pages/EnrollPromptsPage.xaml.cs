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

namespace OnlineCoursesProgram.Pages
{
    public sealed partial class EnrollPromptsPage : Page
    {

        List<string> listOfResults = new List<string>();
        List<CourseContent> courseList = new List<CourseContent>();
        DatabaseModel db = new DatabaseModel();

        public EnrollPromptsPage()
        {
            this.InitializeComponent();
        }

        // Function that renders the search result list whenever the text changes in the search box
        public void OnSearchChange(object sender, RoutedEventArgs e)
        {
            listOfResults = new List<string>();
            searchResultList.ItemsSource = null;

            // Simple search algorithm that checks if the course names contain the substring entered,
            // Will likely render a large amount of results when there are more courses in the database

            // **NOTE** Will need to include course descriptions in the future in order to diversify
            // results from the search
            foreach (CourseContent c in courseList)
            {
                if(searchField.Text != "" && c.CourseName.ToString().ToLower().Trim().Contains(searchField.Text.ToString().ToLower().Trim()))
                {
                    listOfResults.Add(c.CourseName);
                }
            }

            searchResultList.ItemsSource = listOfResults;
        } 

        // Function to handle when the page is navigated to, pulls all courses in database 
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            courseList = await db.GetAllCourses();
            base.OnNavigatedTo(e);
        }

        // Function to handle when the page is navigated from, closes database connection
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
