using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
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
    public sealed partial class ClassListPage : Page
    {
        public ClassListPage()
        {
            this.InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ClassContent c1 = new ClassContent();
            c1.Source = "ms-appx:///Assets/testvideo2.mp4";
            c1.ClassName = "[Class Code] - Lesson 1";

            subFrame.Navigate(typeof(IndivClassPage), c1);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            ClassContent c2 = new ClassContent();
            c2.Source = "ms-appx:///Assets/testingvideo.mp4";
            c2.ClassName = "[Class Code] - Lesson 2";

            subFrame.Navigate(typeof(IndivClassPage), c2);
        }
    }
}
