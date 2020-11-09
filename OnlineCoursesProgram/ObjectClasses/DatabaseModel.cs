using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Display;

namespace OnlineCoursesProgram
{
    class DatabaseModel
    {
        string sqlConnectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=TestingDatabase";
        SqlConnection conn;
        SqlCommand command;
        SqlDataReader reader;

        public DatabaseModel()
        {
            ConnectToDatabase();
            //CreateNewPost(3, "Ceej", "Test content 3", "29/10/2020");
            //AddPostToCourse(3, 1, 3);
            //CreateNewClass(2, "Testing Class 2", SaveToByteStream(@"Assets/testingvideo.mp4"));
            //AddClassToCourse(2, 1, 2);
        }

        // Initialising the database connection
        public void ConnectToDatabase()
        {
            conn = new SqlConnection(sqlConnectionString);
            conn.Open();
        }

        // Collects the login details from the form, and requests sql information matching that in the user table
        // If the data returned is anything except verified, the login automatically fails
        public User CheckLoginDetails(string enteredUsername, string enteredPassword)
        {
            User user = new User();

            command = new SqlCommand("SELECT * FROM Users WHERE Username=@username AND Password=@password", conn);
            command.Parameters.AddWithValue("@username", enteredUsername);
            command.Parameters.AddWithValue("@password", enteredPassword);
            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("{0}", reader["UserID"]);
                    user.UserID = (int)reader["UserID"];
                    user.DisplayName = reader["DisplayName"].ToString();
                    user.Role = reader["Role"].ToString();
                }
            }
            finally
            {
                reader.Close();
            }

            //student.CoursesList = ViewAllUserCourses((int)student.UserID);

            //return student;
            return user;
        }

        // returns a Student objects according to student ID
        public async Task<Student> GetStudentByID(int userID)
        {
            Student student = new Student();

            command = new SqlCommand("SELECT * FROM Users WHERE UserID=@userID", conn);
            command.Parameters.AddWithValue("@userID", (long)userID);
            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("{0}", reader["UserID"]);
                    student.UserID = (int)reader["UserID"];
                    student.Username = reader["DisplayName"].ToString();
                }
            }
            finally
            {
                reader.Close();
            }

            return student;
        }

        // Creates a new user entity in the database
        public void CreateNewUser(int userID, string username, string password, string displayName, string role)
        {
            command = new SqlCommand("INSERT INTO Users (UserID, Username, Password, DisplayName, Role) VALUES (@userid, @username, @password, @displayName, @role)", conn);
            command.Parameters.AddWithValue("@userid", userID);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@displayName", displayName);
            command.Parameters.AddWithValue("@role", role);

            int numberOfRowsAffected = command.ExecuteNonQuery();
            //reader = command.ExecuteReader();

            //System.Diagnostics.Debug.WriteLine(numberOfRowsAffected);
        }

        // processes video files into a byte array, ready to save to the database
        public byte[] SaveToByteStream(string source)
        {
            FileStream stream = File.OpenRead(source);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            return buffer;
        }

        // Creates a new class entity in the database
        public async Task<int> CreateNewClass(string className, byte[] video)
        {
            command = new SqlCommand("INSERT INTO Classes (ClassName, Video) OUTPUT INSERTED.ClassID VALUES (@className, @video)", conn);
            command.Parameters.AddWithValue("@className", className);
            command.Parameters.AddWithValue("@video", video);

            //int affectedRows = command.ExecuteNonQuery();
            int newClassID = 0;

            reader = command.ExecuteReader();

            try
            {
                while(reader.Read())
                {
                    newClassID = (int)reader["ClassID"];
                }
            }
            catch(Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
            finally
            {
                reader.Close();
            }

            return newClassID;
        }

        // creates a new course entity in the database
        public void CreateNewCourse(int courseID, string courseCode, string courseName)
        {
            command = new SqlCommand("INSERT INTO Courses (CourseID, CourseCode, CourseName) VALUES (@courseID, @courseCode, @courseName)", conn);
            command.Parameters.AddWithValue("@courseID", courseID);
            command.Parameters.AddWithValue("@courseCode", courseCode);
            command.Parameters.AddWithValue("@courseName", courseName);

            int affectedRows = command.ExecuteNonQuery();

            System.Diagnostics.Debug.WriteLine(affectedRows);
        }

        // Links a course with a user
        public async Task<int> EnrollInCourse(int userID, int courseID)
        {
            command = new SqlCommand("INSERT INTO Enrolled (UserID, CourseID) VALUES (@userID, @courseID)", conn);
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@courseID", courseID);

            int affectedRows = command.ExecuteNonQuery();
            System.Diagnostics.Debug.WriteLine("Inserted " + affectedRows + " new enrolled row");

            return affectedRows;
        }

        // Links a class with a course
        public async Task AddClassToCourse(int courseID, int classID)
        {
            command = new SqlCommand("INSERT INTO CourseClass (CourseID, ClassID) VALUES (@courseID, @classID)", conn);
            command.Parameters.AddWithValue("@courseID", courseID);
            command.Parameters.AddWithValue("@classID", classID);

            int affectedRows = command.ExecuteNonQuery();
            System.Diagnostics.Debug.WriteLine(affectedRows);
        }

        // creates a new post entity in the database
        public void CreateNewPost(int postID, string author, string content, string datePosted)
        {
            command = new SqlCommand("INSERT INTO Posts (PostID, Author, DatePosted, Content) VALUES (@postID, @author, @datePosted, @content)", conn);
            command.Parameters.AddWithValue("@postID", postID);
            command.Parameters.AddWithValue("@author", author);
            command.Parameters.AddWithValue("@datePosted", datePosted);
            command.Parameters.AddWithValue("@content", content);

            int affectedRows = command.ExecuteNonQuery();
            System.Diagnostics.Debug.WriteLine(affectedRows);
        }

        // links a post with a course
        public void AddPostToCourse(int coursePostID, int courseID, int postID)
        {
            command = new SqlCommand("INSERT INTO CoursePost (CoursePostID, CourseID, PostID) VALUES (@coursePostID, @courseID, @postID)", conn);
            command.Parameters.AddWithValue("@coursePostID", coursePostID);
            command.Parameters.AddWithValue("@courseID", courseID);
            command.Parameters.AddWithValue("@postID", postID);

            int affectedRows = command.ExecuteNonQuery();
            System.Diagnostics.Debug.WriteLine(affectedRows);
        }

        // returns all courses that a user is enrolled in 
        public async Task<List<CourseContent>> ViewAllUserCourses(int userID)
        {
            List<CourseContent> studentCourses = new List<CourseContent>();
            List<int> courseIDs = new List<int>();

            command = new SqlCommand("SELECT CourseID FROM Enrolled WHERE UserID=@userID", conn);
            command.Parameters.AddWithValue("@userID", userID);

            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    courseIDs.Add((int)reader["CourseID"]);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                reader.Close();
            }

            foreach (int x in courseIDs)
            {
                System.Diagnostics.Debug.WriteLine(x);
                studentCourses.Add(await GetCourseByID(x));
            }

            return studentCourses;
        }

        // returns course details for a course ID
        public async Task<CourseContent> GetCourseByID(int courseID)
        {
            CourseContent course = new CourseContent();

            command = new SqlCommand("SELECT * FROM Courses WHERE CourseID=@courseID", conn);
            command.Parameters.AddWithValue("@courseID", courseID);

            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    course.CourseID = (int)reader["CourseID"];
                    course.CourseCode = reader["CourseCode"].ToString();
                    course.CourseName = reader["CourseName"].ToString();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                reader.Close();
            }

            //course.ClassList = await GetCourseClassList(courseID);
            //course.PostList = await GetAllPostsForCourse(courseID);

            return course;
        }

        // returns a list of classes that are associated with a course, given a course ID
        public async Task<List<ClassContent>> GetCourseClassList(int courseID)
        {
            List<ClassContent> classList = new List<ClassContent>();
            List<int> classIDList = new List<int>();

            command = new SqlCommand("SELECT * FROM CourseClass WHERE CourseID=@courseID", conn);
            command.Parameters.AddWithValue("@courseID", courseID);

            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    classIDList.Add((int)reader["ClassID"]);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                reader.Close();
            }

            foreach(int x in classIDList)
            {
                classList.Add(await GetClassByID(x));
            }

            return classList;
        }

        // return a class object, given a class ID
        public async Task<ClassContent> GetClassByID(int classID)
        {
            ClassContent classContent = new ClassContent();

            command = new SqlCommand("SELECT * FROM Classes WHERE ClassID=@classID", conn);
            command.Parameters.AddWithValue("@classID", classID);

            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    classContent.ClassName = reader["ClassName"].ToString();
                    classContent.Source = (byte[])reader["Video"];
                    classContent.ClassID = (int)reader["ClassID"];
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                reader.Close();
            }

            return classContent;
        }

        // return a list of posts associated with a course, given a course ID
        public async Task<List<Post>> GetAllPostsForCourse(int courseID)
        {
            List<Post> postList = new List<Post>();
            List<int> postIdList = new List<int>();

            command = new SqlCommand("SELECT * FROM CoursePost WHERE CourseID=@courseID", conn);
            command.Parameters.AddWithValue("@courseID", courseID);

            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    postIdList.Add((int)reader["PostID"]);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                reader.Close();
            }

            foreach(int x in postIdList)
            {
                postList.Add(await GetPostByID(x));
            }

            return postList;
        }

        // return post details, given a post ID
        public async Task<Post> GetPostByID(int postID) 
        {
            Post post = new Post();

            command = new SqlCommand("SELECT * FROM Posts WHERE PostID=@postID", conn);
            command.Parameters.AddWithValue("@postID", postID);

            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    post.Author = reader["Author"].ToString();
                    post.DatePosted = DateTime.Parse(reader["DatePosted"].ToString());
                    post.TextContent = reader["Content"].ToString();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                reader.Close();
            }

            return post;
        }

        public async Task<List<CourseContent>> GetAllAssignedCourses(int userID)
        {
            List<CourseContent> courseList = new List<CourseContent>();
            List<int> courseIdList = new List<int>();

            command = new SqlCommand("SELECT CourseID FROM Assigned WHERE UserID=@userID", conn);
            command.Parameters.AddWithValue("@userID", userID);

            reader = command.ExecuteReader();

            try
            {
                while(reader.Read())
                {
                    courseIdList.Add((int)reader["CourseID"]);
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                reader.Close();
            }

            foreach(int x in courseIdList)
            {
                courseList.Add(await GetCourseByID(x));
            }

            return courseList;
        }

        public async void AssignCourseToUser(int userID, int courseID)
        {
            command = new SqlCommand("INSERT INTO Assigned (CourseID, UserID) VALUES (@courseID, @userID)", conn);
            command.Parameters.AddWithValue("@courseID", courseID);
            command.Parameters.AddWithValue("@userID", userID);

            int affectedRows = command.ExecuteNonQuery();

            System.Diagnostics.Debug.WriteLine("Courses Added: " + affectedRows);
        }

        public async Task<List<CourseContent>> GetAllCourses()
        {
            List<CourseContent> courseList = new List<CourseContent>();
            List<int> courseIdList = new List<int>();

            command = new SqlCommand("SELECT * From Courses", conn);

            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    courseIdList.Add((int)reader["CourseID"]);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                reader.Close();
            }

            foreach (int x in courseIdList)
            {
                courseList.Add(await GetCourseByID(x));
            }

            return courseList;
        }

        public void Close()
        {
            conn.Close();
        }
    }
}
