using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.System.Display;
using Windows.UI.Core;
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
    public sealed partial class RecordingPage : Page
    {
        MediaCapture mediaCapture = new MediaCapture();
        bool isPreviewing = false;
        DisplayRequest displayRequest = new DisplayRequest();
        LowLagMediaRecording _mediaRecording;

        public RecordingPage()
        {
            this.InitializeComponent();
        }

        private async Task StartPreviewAsync()
        {
            if(!isPreviewing)
            {
                try
                {
                    isPreviewing = true;
                    mediaCapture = new MediaCapture();
                    await mediaCapture.InitializeAsync();

                    displayRequest.RequestActive();
                    DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;

                    var myVideos = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Videos);
                    Windows.Storage.StorageFolder newFolder = await myVideos.RequestAddFolderAsync();

                    Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    Windows.Storage.StorageFile file = await newFolder.CreateFileAsync("vid.mp4", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
                    _mediaRecording = await mediaCapture.PrepareLowLagRecordToStorageFileAsync(
                    MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Auto), file);
                    await _mediaRecording.StartAsync();
                    recordButton.Content = "Stop Recording";
                }
                catch (UnauthorizedAccessException)
                {
                    System.Diagnostics.Debug.WriteLine("The app was denied access to the camera");
                    return;
                }

                try
                {
                    videoRecorder.Source = mediaCapture;
                    await mediaCapture.StartPreviewAsync();
                }
                catch (System.IO.FileLoadException)
                {
                    System.Diagnostics.Debug.WriteLine("Playback failed.");
                }
            }
            else
            {
                await _mediaRecording.StopAsync();
                await _mediaRecording.FinishAsync();
                await mediaCapture.StopPreviewAsync();
                videoRecorder.Source = null;
                isPreviewing = false;
                recordButton.Content = "Record";
            }

        }

        private async Task CleanupCameraAsync()
        {
            if (mediaCapture != null)
            {
                if (isPreviewing)
                {
                    await mediaCapture.StopPreviewAsync();
                }

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    videoRecorder.Source = null;
                    if (displayRequest != null)
                    {
                        displayRequest.RequestRelease();
                    }

                    mediaCapture.Dispose();
                    mediaCapture = null;
                });
            }

        }

        public async void Record_Click(object sender, RoutedEventArgs e)
        {
            StartPreviewAsync();
            
            

            //if (isPreviewing)
            //{
            //    await _mediaRecording.StopAsync();
            //    CleanupCameraAsync();
            //    isPreviewing = false;
            //}
            //else
            //{
               
            //}
            
            
        }
    }
}
