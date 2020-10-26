﻿using Newtonsoft.Json;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnlineCoursesProgram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClassListPage : Page
    {
        CourseContent course = new CourseContent();
        List<ClassContent> classList = new List<ClassContent>();

        public ClassListPage()
        {
            this.InitializeComponent();
        }

        public void LoadClasses()
        {
            foreach (ClassContent x in classList)
            {
                Button b = new Button();
                b.Margin = new Thickness(0, 0, 0, 1);
                b.HorizontalAlignment = HorizontalAlignment.Stretch;
                b.Height = 50;
                b.Foreground = new SolidColorBrush(Colors.Black);
                b.Background = new SolidColorBrush(Colors.LightBlue);
                b.Content = course.CourseCode + " - " + x.ClassName;
                b.Click += (s, e) => { subFrame.Navigate(typeof(IndivClassPage), x); };

                classListPanel.Children.Add(b);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            course = e.Parameter as CourseContent;
            classList = course.ClassList;

            LoadClasses();

            base.OnNavigatedTo(e);
        }
    }
}
