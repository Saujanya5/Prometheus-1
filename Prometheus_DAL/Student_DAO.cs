using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Prometheus_Entities;
namespace Prometheus_DAL
{
    /// <summary>
    /// Written by Hrithik and Navpreet
    /// </summary>
    public class Student_DAO
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader sdr;
        DataTable dt;

        public Student_DAO() {
            con = new SqlConnection();
            con.ConnectionString = "Server=.\\SqlExpress;Integrated Security=true;Database=PROJECT_SPRINT1";
        }
        /// <summary>
        /// Return availabe courses
        /// </summary>
        /// <param name="studentID">Student ID</param>
        /// <returns>list of courses</returns>
        public List<Course> ShowAllCoursesAvailable(int studentID)
        {
            List<Course> courseList = null;
            con.Open();
            SqlParameter param = new SqlParameter("@studentID", studentID);
            cmd = new SqlCommand();
            cmd.CommandText = "USP_ShowAvailableCourses";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param);
            cmd.Connection = con;

            sdr = cmd.ExecuteReader();

            //Loading Data from Reader in to Table
            dt = new DataTable();
            dt.Load(sdr);

            if (dt.Rows.Count > 0)
            {
                courseList = new List<Course>();

                foreach (DataRow item in dt.Rows)
                {
                    Course cObj = new Course();

                    cObj.CourseID = Convert.ToInt32(item[0]);
                    cObj.CourseName = item[1].ToString();
                    cObj.StartDate = Convert.ToDateTime(item[2]);
                    cObj.EndDate = Convert.ToDateTime(item[3]);

                    //Adding Vendor object to List
                    courseList.Add(cObj);

                }
            }

            sdr.Close();
            con.Close();

            return courseList;
        }
        /// <summary>
        /// Return Applied courses
        /// </summary>
        /// <param name="studentID">Student ID</param>
        /// <returns>list of courses</returns>
        public List<Course> ShowAllCoursesApplied(int studentID)
        {
            List<Course> courseList = null;
            con.Open();
            SqlParameter param = new SqlParameter("@studentID", studentID);
            cmd = new SqlCommand();
            cmd.CommandText = "USP_ShowCoursesApplied";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param);
            cmd.Connection = con;

            sdr = cmd.ExecuteReader();

            dt = new DataTable();
            dt.Load(sdr);

            if (dt.Rows.Count > 0)
            {
                courseList = new List<Course>();

                foreach (DataRow item in dt.Rows)
                {
                    Course cObj = new Course();

                    cObj.CourseID = Convert.ToInt32(item[0]);
                    cObj.CourseName = item[1].ToString();
                    cObj.StartDate = Convert.ToDateTime(item[2]);
                    cObj.EndDate = Convert.ToDateTime(item[3]);

                    //Adding Vendor object to List
                    courseList.Add(cObj);

                }
            }

            sdr.Close();
            con.Close();

            return courseList;
        }
        /// <summary>
        /// Emroll student into a course
        /// </summary>
        /// <param name="courseID">Course id</param>
        /// <param name="studentID">Student id</param>
        /// <returns>returns true if enrolled successfully</returns>
        public bool ApplyCourse(int courseID,int studentID)
        {
            con.Open();
            SqlParameter param1=  new SqlParameter("@courseID", courseID);
            SqlParameter param2 = new SqlParameter("@studentID", studentID);
            cmd = new SqlCommand();
            cmd.CommandText = "USP_EnrollForCourses";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Connection = con;

            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            if (affectedRows > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Update student details
        /// </summary>
        /// <param name="studentObj">object of student entity</param>
        /// <returns>returns true if updated successfully</returns>
        public bool UpdateDeatails(Student studentObj)
        {
            con.Open();
            SqlParameter param1 = new SqlParameter("@Fname", studentObj.FName);
            SqlParameter param2 = new SqlParameter("@Lname", studentObj.LName);
            SqlParameter param3 = new SqlParameter("@address", studentObj.Address);
            SqlParameter param4 = new SqlParameter("@DOB", studentObj.DOB);
            SqlParameter param5 = new SqlParameter("@city", studentObj.City);
            SqlParameter param6 = new SqlParameter("@MobileNo", studentObj.MobileNo);
            SqlParameter param7 = new SqlParameter("@studentID", studentObj.StudentID);
        
            cmd = new SqlCommand();
            cmd.CommandText = "usp_UpdateStudent";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);
            cmd.Parameters.Add(param4);
            cmd.Parameters.Add(param5);
            cmd.Parameters.Add(param6);
            cmd.Parameters.Add(param7);
            cmd.Connection = con;

            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            if (affectedRows > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Function to change the password
        /// </summary>
        /// <param name="studentID">Student id</param>
        /// <param name="password">new password</param>
        /// <returns>true if password updated successfully</returns>

        public bool ChangePassword(int studentID,string password)
        {
            con.Open();
            SqlParameter param1 = new SqlParameter("@studentID", studentID);
            SqlParameter param = new SqlParameter("@password", password);
            cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Student SET password = @password WHERE StudentID = @StudentID";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param);
            cmd.Connection = con;

            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            if (affectedRows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Show all the hw availabe
        /// </summary>
        /// <param name="studentID">Student Id</param>
        /// <returns>List of availabe hw</returns>
        public List<HomeWork> ShowAllHomework(int studentID)
        {
            List<HomeWork> hwList = null;
            con.Open();
            SqlParameter param = new SqlParameter("@studentID", studentID);
            cmd = new SqlCommand();
            cmd.CommandText = "USP_ShowHomeWork";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(param);
            sdr = cmd.ExecuteReader();

            dt = new DataTable();
            dt.Load(sdr);

            if (dt.Rows.Count > 0)
            {
                hwList = new List<HomeWork>();

                foreach (DataRow item in dt.Rows)
                {
                    HomeWork hObj = new HomeWork();

                    hObj.HomeWorkID = Convert.ToInt32(item[0]);
                    hObj.Description = item[1].ToString();
                    hObj.Deadline = Convert.ToDateTime(item[2]);
                    hObj.ReqTime = Convert.ToInt32(item[3]);
                    hObj.LongDescription = item[4].ToString();
                    hObj.CourseName = item[5].ToString();
                    //Adding Vendor object to List
                    hwList.Add(hObj);

                }
            }

            sdr.Close();
            con.Close();

            return hwList;
        }
        /// <summary>
        /// get password from the database
        /// </summary>
        /// <param name="studentID">integer value for student id</param>
        /// <returns>string value of password</returns>
        public string GetPasswordStudent(int studentID)
        {
            con.Open();
            string password=null;
            SqlParameter param = new SqlParameter("@studentID", studentID);
            cmd = new SqlCommand();
            cmd.CommandText = "select password from student where StudentId = @studentID";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.Parameters.Add(param);
            sdr = cmd.ExecuteReader();

            dt = new DataTable();
            dt.Load(sdr);

            if (dt.Rows.Count > 0)
            {


                foreach (DataRow item in dt.Rows)
                {
                     password = item[0].ToString();

                }
            }

            sdr.Close();
            con.Close();

            return password;
        }
        /// <summary>
        ///  Definition: SearchStudentByID function is used to Search student with studentID as parameter in the Student table.
        ///  Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns>
        /// Returns object of all the details of students whose ID is searched
        /// </returns>
        public Student SearchStudentByID(int Id)
        {
            Student s1 = new Student();
            con.Open();
            SqlParameter s;
            s = new SqlParameter("@StudentID", Id);

            cmd = new SqlCommand();
            cmd.CommandText = "usp_StudentById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.Add(s);
            sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr != null)
            {
                s1.StudentID = Int32.Parse(sdr[0].ToString());
                s1.FName = sdr[1].ToString();
                s1.LName = sdr[2].ToString();
                s1.Address = sdr[3].ToString();
                s1.DOB = Convert.ToDateTime(sdr[4]);
                s1.City = sdr[5].ToString();
                s1.MobileNo = (long)Convert.ToDouble(sdr[6]);
            }
            sdr.Close();
            con.Close();

            return s1;
        }
    }
}
