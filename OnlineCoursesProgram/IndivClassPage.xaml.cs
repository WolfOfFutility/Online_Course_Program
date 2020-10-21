using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class IndivClassPage : Page
    {
        string m1source = "";
        bool isPlaying = false;

        public IndivClassPage()
        {
            this.InitializeComponent();
        }

        private void PlayVideo(object sender, RoutedEventArgs e)
        {
            if(!isPlaying)
            {
                videoPlayer.MediaPlayer.Play();
                playVideoButton.Content = "Pause";
                isPlaying = true;
            }
            else
            {
                videoPlayer.MediaPlayer.Pause();
                playVideoButton.Content = "Play";
                isPlaying = false;
            }
        }

        private void JumpBack(object sender, RoutedEventArgs e)
        {
            // Jump back 10 seconds
        }

        private void JumpForward(object sender, RoutedEventArgs e)
        {
            // Jump forward 10 seconds
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ClassContent cc = e.Parameter as ClassContent;
            m1source = cc.Source;
            lessonTitle.Text = cc.ClassName;

            videoPlayer.Source = MediaSource.CreateFromUri(new Uri(m1source));

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            videoPlayer.Source = null;
            base.OnNavigatedFrom(e);
        }

        //protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    base.OnNavigatingFrom(e);
        //    videoPlayer.Source = null;
        //}
    }
}
