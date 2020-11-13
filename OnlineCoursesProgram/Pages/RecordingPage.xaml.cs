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
    /// 
    // **NOTE** Functionality needs to be added to import existing files in as class videos
    // **NOTE** Functionality needs to be added to live stream videos, and then saving 
    // them after the fact
    public sealed partial class RecordingPage : Page
    {
        // Initialising Instance Variables
        private MediaCapture mediaCapture = new MediaCapture();
        private bool isPreviewing = false;
        private DisplayRequest displayRequest = new DisplayRequest();
        private LowLagMediaRecording _mediaRecording;
        private string newFilePath = "";
        private Teacher teacher = new Teacher();
        private DatabaseModel db = new DatabaseModel();

        public RecordingPage()
        {
            this.InitializeComponent();
        }

        // Start recording, requesting access to the camera and microphone
        // At the end of the recording, the file is transferred to a byte array and entered into the database
        // ** NOTE ** Need to add functionality to capture the screen at the same time
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

                    Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    Windows.Storage.StorageFile file = await storageFolder.CreateFileAsync("vid1.mp4", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
                    _mediaRecording = await mediaCapture.PrepareLowLagRecordToStorageFileAsync(
                    MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Auto), file);
                    await _mediaRecording.StartAsync();
                    recordButton.Content = "Stop Recording";

                    newFilePath = file.Path;
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

        // Wipes all storage in the current camera and media playback objects, clears the screen, turns off the camera
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

        // Handler for when the "Record" button is clicked
        public async void Record_Click(object sender, RoutedEventArgs e)
        {
            StartPreviewAsync();
        }

        // Handler for when the "Save" Button is clicked
        public async void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int newClassID = await db.CreateNewClass(newClassName.Text, db.SaveToByteStream(newFilePath));
                await db.AddClassToCourse((int)selectedCourseCombo.SelectedValue, newClassID);
            }
            catch(Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
            
            File.Delete(newFilePath);
        }

        // Loads all courses that the user is responsible for into the combo box
        private async void LoadCoursesIntoCombo()
        {
            selectedCourseCombo.DisplayMemberPath = "Text";
            selectedCourseCombo.SelectedValuePath = "Value";

            foreach (CourseContent c in await db.GetAllAssignedCourses((int)teacher.UserID))
            {
                selectedCourseCombo.Items.Add(new { Text = c.CourseName, Value = c.CourseID });
            }
        }

        // Overrides the on navigated to function to take an object passed from the previous page
        // and loads the combo box with all of the available courses for the user
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            teacher = e.Parameter as Teacher;
            LoadCoursesIntoCombo();

            base.OnNavigatedTo(e);
        }

        // Closes the database connection when the page is navigated away
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            db.Close();
            base.OnNavigatedFrom(e);
        }
    }
}
