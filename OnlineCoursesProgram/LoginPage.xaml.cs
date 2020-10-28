using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Sockets;
using Windows.UI.Composition;
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
    public sealed partial class LoginPage : Page
    {
        string sqlConnectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=TestingDatabase";
        SqlConnection conn;
        SqlCommand command;
        SqlDataReader reader;

        public LoginPage()
        {
            this.InitializeComponent();
            ConnectToDatabase();
        }

        // Initialising the database connection, which will remain open until navigating away from the page
        // ** NOTE ** Need to look into passing the same database connection through the rest of the application to save on resources
        private void ConnectToDatabase()
        {
            conn = new SqlConnection(sqlConnectionString);
            conn.Open();
        }

        // Collects the login details from the form, and requests sql information matching that in the user table
        // If the data returned is anything except verified, the login automatically fails
        private long CheckLoginDetails(string enteredUsername)
        {
            long specifiedID = -1;
            command = new SqlCommand("SELECT * FROM Users WHERE Username=@username", conn);
            command.Parameters.AddWithValue("@username", enteredUsername);
            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("{0}", reader["UserID"]);
                    specifiedID = long.Parse(reader["UserID"].ToString());
                }
            }
            finally
            {
                reader.Close();
            }

            return specifiedID;
        }

        // Handler for when the login button is clicked.
        // Only handles the username for now, further verification will come later
        public void Login_Click(object sender, RoutedEventArgs e)
        {
            if(CheckLoginDetails(usernameField.Text) == -1)
            {
                System.Diagnostics.Debug.WriteLine("Login Failed");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Welcome Back!");
                this.Frame.Navigate(typeof(MainDirectoryPage));
            }
        }
    }
}
