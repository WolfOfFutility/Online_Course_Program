using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.NumberFormatting;
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
    /// A page that renders the content for a specific class for a specific course.
    /// </summary>
    /// 
    public sealed partial class IndivClassPage : Page
    {
        bool isPlaying = false;
        byte[] buffer;
        DatabaseModel db = new DatabaseModel();

        public IndivClassPage()
        {
            this.InitializeComponent();
        }

        // Play/Pause video playback
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

        // Jump the video Back 
        private void JumpBack(object sender, RoutedEventArgs e)
        {
            // Jump back 10 seconds
        }

        // Jump the video Forward
        private void JumpForward(object sender, RoutedEventArgs e)
        {
            // Jump forward 10 seconds
        }

        // Load video from the byte stream stored in the object class
        public async void LoadFromByteStream()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("vid.mp4", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
            await Windows.Storage.FileIO.WriteBufferAsync(sampleFile, buffer.AsBuffer());

            videoPlayer.Source = MediaSource.CreateFromStorageFile(sampleFile);
        }

        // Load parameters sent down the chain from the main page, in order to render the correct video for the class
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ClassContent cc = e.Parameter as ClassContent;
            //buffer = cc.Source;
            lessonTitle.Text = cc.ClassName;
            buffer = cc.Source;

            LoadFromByteStream();

            base.OnNavigatedTo(e);
        }

        // Whenever a new class is chosen, dispose of the video source
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            videoPlayer.MediaPlayer.Dispose();
            base.OnNavigatedFrom(e);
        }
    }
}
