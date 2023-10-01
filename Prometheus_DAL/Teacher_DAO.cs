using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Prometheus_Entities;


namespace Prometheus_DAL
{
    /// <summary>
    /// Definition: Teacher DAO class deals with interaction with the database for Teacher Module.
    /// Name: Hrishikesh Gawas And Saujanya Waikar
    /// </summary>
    public class Teacher_DAO
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader sdr;
        DataTable dt;
        public Teacher_DAO()
        {
            con = new SqlConnection();
            con.ConnectionString = "Server=.\\SqlExpress;Integrated Security=true;Database=PROJECT_SPRINT1";
        }



      
        /// <summary>
        /// Definition: AddToHomeWork function is used to add homework data in the Homework table.
        /// Name: Hrishikesh Gawas And Saujanya Waikar
        /// </summary>
        /// <param name="homeWorkObj"></param>
        /// <returns>
        /// Returns true if homework data is successfully added to the Homework table, else returns false.
        /// </returns>
        public bool AddToHomeWork(HomeWork homeWorkObj)
        {
            con.Open();

            SqlParameter param2 = new SqlParameter("@Description", homeWorkObj.Description);
            SqlParameter param3 = new SqlParameter("@Deadline", homeWorkObj.Deadline);
            SqlParameter param4 = new SqlParameter("@ReqTime", homeWorkObj.ReqTime);
            SqlParameter param5 = new SqlParameter("@LongDescription", homeWorkObj.LongDescription);



            cmd = new SqlCommand();
            cmd.CommandText = "usp_AddHW";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);
            cmd.Parameters.Add(param4);
            cmd.Parameters.Add(param5);
            cmd.Connection = con;


            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            if (affectedRows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        ///  Definition: AddToAssignment function is used to add Homework Id, Course Id, and Teacher Id in the Assignment table.
        ///  Name: Hrishikesh Gawas And Saujanya Waikar
        /// </summary>
        /// <param name="homeWorkID"></param>
        /// <param name="teacherID"></param>
        /// <param name="courseID"></param>
        /// <returns>
        /// Returns true if the assignment data is successfully added to the Assignment table, else returns false.
        /// </returns>

        public bool AddToAssignment(int homeWorkID, int teacherID, int courseID)
        {
            con.Open();
            SqlParameter param1 = new SqlParameter("@HomeWorkID", homeWorkID);
            SqlParameter param2 = new SqlParameter("@TeacherID", teacherID);
            SqlParameter param3 = new SqlParameter("@CourseID", courseID);



            cmd = new SqlCommand();
            cmd.CommandText = "usp_AddAssignment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);

            cmd.Connection = con;

            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();

            if (affectedRows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Definition: AddToAssignment function is used to add or update the teacher data in the Teacher table.
        /// Name: Hrishikesh Gawas And Saujanya Waikar
        /// </summary>
        /// <param name="teacherObj"></param>
        /// <returns>
        /// Returns true if the teacher data is successfully added or updated to the teacher table, else returns false.
        /// </returns>
        public bool UpdateTeacherDetails(Teacher teacherObj)
        {

            con.Open();
            SqlParameter param1 = new SqlParameter("@TeacherID", teacherObj.TeacherID);
            SqlParameter param2 = new SqlParameter("@Fname", teacherObj.FName);
            SqlParameter param3 = new SqlParameter("@Lname", teacherObj.LName);
            SqlParameter param4 = new SqlParameter("@Address", teacherObj.Address);
            SqlParameter param5 = new SqlParameter("@DOB", teacherObj.DOB);
            SqlParameter param6 = new SqlParameter("@City", teacherObj.City);
            SqlParameter param7 = new SqlParameter("@MobileNo", teacherObj.MobileNo);


            cmd = new SqlCommand();
            cmd.CommandText = "USP_UpdateTeacher";
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
        /// Defination: ChangePasswordTeacher is used to change the password data in the Teacher Table.
        /// Name: Hrishikesh Gawas And Saujanya Waikar
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="password"></param>
        /// <returns>
        /// Returns true if the password data is successfully added or updated to the teacher table, else returns false.
        /// </returns>
        public bool ChangePasswordTeacher(int teacherID, string password)
        {
            con.Open();
            SqlParameter param1 = new SqlParameter("@teacherID", teacherID);
            SqlParameter param = new SqlParameter("@password", password);
            cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Teacher SET password = @password WHERE TeacherID = @teacherID";
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
        /// Definition: ShowAllCourses is used to display all courses under a specific teacher.
        /// Name: Hrishikesh Gawas And Saujanya Waikar
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns>
        /// Returns the list of courses under a specific teacher.
        /// </returns>
        public List<Course> ShowAllCourses(int teacherID)
        {
            List<Course> myCourseList = null;
            con.Open();
            SqlParameter param1 = new SqlParameter("@TeacherID", teacherID);

            cmd = new SqlCommand();
            cmd.CommandText = "Select * from Courses where CourseId in(select CourseId from Teaches where TeacherID=@TeacherID)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(param1);
            cmd.Connection = con;

            sdr = cmd.ExecuteReader();

            //Loading Data from Reader in to Table
            dt = new DataTable();
            dt.Load(sdr);

            //Converting DataTable to List<Courses>
            if (dt.Rows.Count > 0)
            {
                myCourseList = new List<Course>();

                foreach (DataRow item in dt.Rows)
                {
                    Course tObj = new Course();

                    tObj.CourseID = Convert.ToInt32(item[0]);
                    tObj.CourseName = item[1].ToString();
                    tObj.StartDate = Convert.ToDateTime(item[2]);
                    tObj.EndDate = Convert.ToDateTime(item[3]);

                    myCourseList.Add(tObj);

                }
            }

            sdr.Close();
            con.Close();

            return myCourseList;
        }

        /// <summary>
        /// Definition: ShowAllStudents is used to display details of all the students enrolled in a course under a specific teacher.
        /// Name: Hrishikesh Gawas And Saujanya Waikar
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns>
        ///  Returns the list of students enrolled in a course under a specific teacher.
        /// </returns>
        public List<Student> ShowAllStudents(int teacherID)
        {
            List<Student> myStudentList = null;
            con.Open();
            SqlParameter paraStud = new SqlParameter("@TeacherID", teacherID);
            cmd = new SqlCommand();
            cmd.CommandText = "Select * from Student where StudentId in(select studentId from enrollment where courseId in(select courseId from teaches where TeacherId=@TeacherID))";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(paraStud);
            cmd.Connection = con;

            sdr = cmd.ExecuteReader();

            //Loading Data from Reader in to Table
            dt = new DataTable();
            dt.Load(sdr);

            //Converting DataTable to List<Courses>
            if (dt.Rows.Count > 0)
            {
                myStudentList = new List<Student>();

                foreach (DataRow item in dt.Rows)
                {
                    Student sObj = new Student();

                    sObj.StudentID = Convert.ToInt32(item[0]);
                    sObj.FName = item[1].ToString();
                    sObj.LName = item[2].ToString();
                    sObj.Address = item[3].ToString();
                    sObj.DOB = Convert.ToDateTime(item[4]);
                    sObj.City = item[5].ToString();
                    sObj.MobileNo = Convert.ToInt64(item[7]);




                    myStudentList.Add(sObj);

                }
            }

            sdr.Close();
            con.Close();

            return myStudentList;
        }



        /// <summary>
        /// Definition: ShowHomeWorkAssignments is used to display all homework data for a course under specific teacher.
        /// Name: Hrishikesh Gawas And Saujanya Waikar
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns>
        ///  Returns the list of homeworks for a course under a specific teacher.
        ///  </returns>
        public List<HomeWork> ShowHomeWorkAssignments(int teacherID)
        {
            List<HomeWork> myHomeWorkAssignmentList = null;
            con.Open();
            SqlParameter param1 = new SqlParameter("@TeacherID", teacherID);

            cmd = new SqlCommand();
            cmd.CommandText = "Select * from HomeWork where HomeWorkId IN (select HomeworkId from assignment where courseId in(select courseId from teaches where TeacherId=@TeacherID))";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(param1);
            cmd.Connection = con;

            sdr = cmd.ExecuteReader();

            //Loading Data from Reader in to Table
            dt = new DataTable();
            dt.Load(sdr);

            //Converting DataTable to List<HomeWork>
            if (dt.Rows.Count > 0)
            {
                myHomeWorkAssignmentList = new List<HomeWork>();

                foreach (DataRow item in dt.Rows)
                {
                    HomeWork haObj = new HomeWork();

                    haObj.HomeWorkID = Convert.ToInt32(item[0]);
                    haObj.Description = item[1].ToString();
                    haObj.Deadline = Convert.ToDateTime(item[2]);
                    haObj.ReqTime = Convert.ToInt32(item[3]);
                    haObj.LongDescription = item[4].ToString();

                    myHomeWorkAssignmentList.Add(haObj);

                }
            }

            sdr.Close();
            con.Close();

            return myHomeWorkAssignmentList;
        }

        /// <summary>
        /// Definition: GetLatestHomework is used to get the Homework ID for latest homework data added in the Homework table.
        /// Name: Hrishikesh Gawas And Saujanya Waikar
        /// </summary>
        /// <returns>
        /// Returns maximum Homework ID in the Homework Table.
        /// </returns>
        public int GetLatestHW()
        {
            con.Open();

            cmd = new SqlCommand();
            cmd.CommandText = "select max(HomeWorkId) from Homework";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            int max = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return max;
        }

        /// <summary>
        /// Definition: GetPassword is used to get the current password data of teacher in the Teacher table based on the Teacher ID.
        /// Name: Hrishikesh Gawas And Saujanya Waikar
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns>
        /// Returns password data from the Teacher Table.
        /// </returns>
        public string GetPasswordTeacher(int teacherID)
        {
            con.Open();
            string password = null;
            SqlParameter param = new SqlParameter("@teacherID", teacherID);
            cmd = new SqlCommand();
            cmd.CommandText = "select password from teacher where TeacherID = @teacherID";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.Parameters.Add(param);
            sdr = cmd.ExecuteReader();

            //Loading Data from Reader in to Table
            dt = new DataTable();
            dt.Load(sdr);

            //Converting DataTable to List<Vendor>
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
        ///  Definition: SearchTeacherByID function is used to Search Teacher with TeacherID as parameter in the Teacher table.
        ///  Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="TeacherID"></param>
        /// <returns>
        /// Returns object of all the details of Teacher whose ID is searched
        /// </returns>

        public Teacher SearchTeacherByID(int teacherID)
        {
            Teacher t1 = new Teacher();
            con.Open();
            SqlParameter param1 = new SqlParameter("@TeacherID", teacherID);

            cmd = new SqlCommand();
            cmd.CommandText = "usp_TeacherById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(param1);
            sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr != null)
            {
                t1.TeacherID = Int32.Parse(sdr[0].ToString());
                t1.FName = sdr[1].ToString();
                t1.LName = sdr[2].ToString();
                t1.Address = sdr[3].ToString();
                t1.DOB = Convert.ToDateTime(sdr[4]);
                t1.City = sdr[5].ToString();
                t1.MobileNo = Convert.ToInt64(sdr[6]);
            }
            sdr.Close();
            con.Close();

            return t1;
        }

    }
}
