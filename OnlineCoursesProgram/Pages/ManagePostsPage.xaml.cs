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


namespace OnlineCoursesProgram.Pages
{
    public sealed partial class ManagePostsPage : Page
    {
        private DatabaseModel db = new DatabaseModel();
        private Teacher teacher = new Teacher();
        private List<Post> listOfPosts = new List<Post>();

        public ManagePostsPage()
        {
            this.InitializeComponent();
        }

        // Load all the associated posts from the user
        private async void LoadPosts()
        {
            listOfPosts = await db.GetAllUsersPosts((int)teacher.UserID);
            postListBox.ItemsSource = null;
            postListBox.ItemsSource = listOfPosts;
        }

        // Handler for when the add new post button is clicked
        // Redirects to the create new post page
        public void AddNewPostClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateNewPostPage), teacher);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            teacher = e.Parameter as Teacher;
            LoadPosts();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
