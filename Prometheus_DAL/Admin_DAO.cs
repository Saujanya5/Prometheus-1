using Prometheus_Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Prometheus_DAL
{
    /// <summary>
    /// Definition: Admin DAO class deals with interaction with the database for Admin Module.
    /// Name: Mridul and Prathamesh
    /// </summary>
    public class Admin_DAO
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader sdr;
        DataTable dt;

        public Admin_DAO()
        {
            con = new SqlConnection();
            con.ConnectionString = "server=.\\SqlExpress;Integrated Security=true;Database=PROJECT_SPRINT1";
        }
        /// <summary>
        ///  Definition: SelectAvailableAdmins function is used to View all teachers that are admins.
        ///  Name: Mridul and Prathamesh    
        /// </summary>
        /// <param name="IsAdmin"></param>
        /// <returns>
        /// Returns a list of the details of teacher where IsAdmin is 1
        /// </returns>
        public List<Teacher> SelectAvailableAdmins()
        {
            List<Teacher> myAdminList = null;
            con.Open();

            cmd = new SqlCommand();
            cmd.CommandText = "Select * from Teacher where IsAdmin=1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            sdr = cmd.ExecuteReader();

            //Loading Data from Reader in to Table
            dt = new DataTable();
            dt.Load(sdr);

            //Converting DataTable to List<Vendor>
            if (dt.Rows.Count > 0)
            {
                myAdminList = new List<Teacher>();

                foreach (DataRow item in dt.Rows)
                {
                    Teacher tObj = new Teacher();

                    tObj.TeacherID = Convert.ToInt32(item[0]);
                    tObj.FName = item[1].ToString();
                    tObj.LName = item[2].ToString();
                    tObj.Address = item[3].ToString();
                    tObj.City = item[5].ToString();
                    tObj.MobileNo = Convert.ToInt32(item[7]);
                    //Adding Vendor object to List
                    myAdminList.Add(tObj);

                }
            }

            sdr.Close();
            con.Close();

            return myAdminList;
        }
        /// <summary>
        ///  Definition: AddTeacher function is used to add First Name, Last Name, Address, DOB, and password in the Teacher table.
        ///  Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="Fname"></param>
        /// <param name="Lname"></param>
        /// <param name="Address"></param>
        /// <param name="DOB"></param>
        /// <param name="password"></param>
        /// <returns>
        /// Returns true if the Teacher data is successfully added to the Teacher table, else returns false.
        /// </returns>
        public bool AddTeacher(Teacher teacherObj,string password)
        {
            con.Open();
            SqlParameter param1 = new SqlParameter("@Fname", teacherObj.FName);
            SqlParameter param2 = new SqlParameter("@Lname", teacherObj.LName);
            SqlParameter param3 = new SqlParameter("@DOB", teacherObj.DOB);
            SqlParameter param4 = new SqlParameter("@password", password);

            cmd = new SqlCommand();
            cmd.CommandText = "USP_AddTeacher";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);
            cmd.Parameters.Add(param4);
            cmd.Connection = con;

            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            if (affectedRows > 0)
                return true;
            else
                return false;

        }
        /// <summary>
        ///  Definition: AddAdmin function is used to add Admin access to teacher in the Teacher table.
        ///  Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="IsAdmin"></param>>
        /// <returns>
        /// Returns true if the Teacher data is successfully added to the Teacher table, else returns false.
        /// </returns>
        public bool AddAdmin(int teacherId)
        {
            con.Open();
            SqlParameter param1 = new SqlParameter("@teacherId", teacherId);
            cmd = new SqlCommand();
            cmd.CommandText = "Update Teacher set isAdmin = 1 where teacherID=@teacherId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(param1);
            cmd.Connection = con;
            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            if (affectedRows > 0)
                return true;
            else
                return false;

        }
        /// <summary>
        ///  Definition: AddStudent function is used to add First Name, Last Name, Address, DOB, and password in the Student table.
        ///  Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="Fname"></param>
        /// <param name="Lname"></param>
        /// <param name="Address"></param>
        /// <param name="DOB"></param>
        /// <param name="password"></param>
        /// <returns>
        /// Returns true if the Student data is successfully added to the Student table, else returns false.
        /// </returns>
        public bool AddStudent(Student studentObj,string password)
        {
            con.Open();
            SqlParameter param1 = new SqlParameter("@Fname", studentObj.FName);
            SqlParameter param2 = new SqlParameter("@Lname", studentObj.LName);
            SqlParameter param3 = new SqlParameter("@DOB", studentObj.DOB);
            SqlParameter param4 = new SqlParameter("@password", password);

            cmd = new SqlCommand();
            cmd.CommandText = "USP_AddStudent";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);
            cmd.Parameters.Add(param4);
            cmd.Connection = con;

            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            if (affectedRows > 0)
                return true;
            else
                return false;

        }
        /// <summary>
        ///  Definition: AddCourse function is used to add Course Name, Start Date and End Date in the Course table.
        ///  Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="CourseName"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>
        /// Returns true if the Course data is successfully added to the Course table, else returns false.
        /// </returns>
        public bool AddCourse(Course courseObj)
        {
            con.Open();
            SqlParameter param1 = new SqlParameter("@CourseName", courseObj.CourseName);
            SqlParameter param2 = new SqlParameter("@StartDate", courseObj.StartDate);
            SqlParameter param3 = new SqlParameter("@EndDate", courseObj.EndDate);


            cmd = new SqlCommand();
            cmd.CommandText = "usp_AddCourse";
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
                if (sdr[3] == System.DBNull.Value)
                    s1.Address = string.Empty;
                else
                    s1.Address = sdr[3].ToString();
                s1.DOB = Convert.ToDateTime(sdr[4]);
                if (sdr[5] == System.DBNull.Value)
                    s1.City = string.Empty;
                else
                    s1.City = sdr[5].ToString();
                if (sdr[6] == System.DBNull.Value)
                    s1.MobileNo = 0;
                else
                    s1.MobileNo = Convert.ToInt64(sdr[6]);

            }
            sdr.Close();
            con.Close();

            return s1;
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
                if (sdr[3] == System.DBNull.Value)
                    t1.Address = string.Empty;
                else
                    t1.Address = sdr[3].ToString();
                t1.DOB = Convert.ToDateTime(sdr[4]);
                if (sdr[5] == System.DBNull.Value)
                    t1.City = string.Empty;
                else
                    t1.City = sdr[5].ToString();
                if (sdr[6] == System.DBNull.Value)
                    t1.MobileNo = 0;
                else
                    t1.MobileNo = Convert.ToInt64(sdr[6]);
            }
            sdr.Close();
            con.Close();

            return t1;
        }
        /// <summary>
        ///  Definition: SearchCourseByID function is used to Search Course with courseID as parameter in the Course table.
        ///  Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="CourseID"></param>
        /// <returns>
        /// Returns object of all the details of COurse whose ID is searched
        /// </returns>
        public Course SearchCourseByID(int courseId)
        {
            Course c1 = new Course();
            con.Open();
            SqlParameter c;
            c = new SqlParameter("@CourseID", courseId);

            cmd = new SqlCommand();
            cmd.CommandText = "usp_CourseById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(c);
            sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr != null)
            {
                c1.CourseID = Int32.Parse(sdr[0].ToString());
                c1.CourseName = sdr[1].ToString();
                c1.StartDate = Convert.ToDateTime(sdr[2]);
                c1.EndDate = Convert.ToDateTime(sdr[3]);
            }
            sdr.Close();
            con.Close();
            return c1;
        }
        /// <summary>
        ///  Definition: DeleteStudentByID function is used to Delete Student with StudentID as parameter in the Student table.
        ///  Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns>
        /// Returns true if the Student data is successfully Deleted from the Student table, else returns false.
        /// </returns>
        public bool DeleteStudentByID(int studID)
        {
            //Student s1 = new Student();
            con.Open();
            SqlParameter s;
            s = new SqlParameter("@StudentID", studID);
            cmd = new SqlCommand();
            cmd.CommandText = "USP_DelStudent";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(s);
            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            if (affectedRows > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        ///  Definition: DeleteTeacherByID function is used to Delete Teacher with TeacherID as parameter in the Teacher table.
        ///  Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="TeacherID"></param>
        /// <returns>
        /// Returns true if the Teacher data is successfully Deleted from the Teacher table, else returns false.
        /// </returns>
        public bool DeleteTeacherByID(int teachID)
        {
            Teacher t1 = new Teacher();
            con.Open();
            SqlParameter t;
            t = new SqlParameter("@TeacherID", teachID);

            cmd = new SqlCommand();
            cmd.CommandText = "USP_DelTeacher";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(t);
            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            if (affectedRows > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        ///  Definition: DeleteCourseByID function is used to Delete Course with CourseID as parameter in the Course table.
        ///  Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="CourseID"></param>
        /// <returns>
        /// Returns true if the Course data is successfully Deleted from the Course table, else returns false.
        /// </returns>
        public bool DeleteCourseByID(int courseID)
        {
            Course c1 = new Course();
            con.Open();
            SqlParameter c;
            c = new SqlParameter("@CourseID", courseID);

            cmd = new SqlCommand();
            cmd.CommandText = "USP_DelCourse";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(c);
            cmd.Connection = con;
            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            if (affectedRows > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Definition: SelectAllStudents is used to display all Students
        /// Name: Mridul and Prathamesh
        /// </summary>
        /// <returns>
        /// Returns the list of Students
        /// </returns>
        public List<Student> SelectAllStudents()
        {
            List<Student> myStudentList = null;
            con.Open();

            cmd = new SqlCommand();
            cmd.CommandText = "Select * from Student";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            sdr = cmd.ExecuteReader();

            //Loading Data from Reader in to Table
            dt = new DataTable();
            dt.Load(sdr);

            //Converting DataTable to List<Student>
            if (dt.Rows.Count > 0)
            {
                myStudentList = new List<Student>();

                foreach (DataRow item in dt.Rows)
                {
                    Student sObj = new Student();

                    sObj.StudentID = Convert.ToInt32(item[0]);
                    sObj.FName = item[1].ToString();
                    sObj.LName = item[2].ToString();
                    if(item[3] == System.DBNull.Value)  
                        sObj.Address = string.Empty; 
                    else
                        sObj.Address = item[3].ToString();
                    sObj.DOB = Convert.ToDateTime(item[4]);
                    if(item[5] == System.DBNull.Value)
                        sObj.City = string.Empty;
                    else                       
                        sObj.City = item[5].ToString();
                    if(item[7] == System.DBNull.Value)
                        sObj.MobileNo = 0;
                    else                        
                        sObj.MobileNo = Convert.ToInt64(item[7]);
                    //Adding Vendor object to List
                    myStudentList.Add(sObj);

                }
            }

            sdr.Close();
            con.Close();    
            return myStudentList;
        }
        /// <summary>
        /// Definition: SelectAllTeachers is used to display all Teachers
        /// Name: Mridul and Prathamesh
        /// </summary>
        /// <returns>
        /// Returns the list of Teachers
        /// </returns>
        public List<Teacher> SelectAllTeachers()
        {
            List<Teacher> myTeacherList = null;
            con.Open();

            cmd = new SqlCommand();
            cmd.CommandText = "Select * from Teacher";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            sdr = cmd.ExecuteReader();

            //Loading Data from Reader in to Table
            dt = new DataTable();
            dt.Load(sdr);

            //Converting DataTable to List<Vendor>
            if (dt.Rows.Count > 0)
            {
                myTeacherList = new List<Teacher>();

                foreach (DataRow item in dt.Rows)
                {
                    Teacher tObj = new Teacher();

                    tObj.TeacherID = Convert.ToInt32(item[0]);
                    tObj.FName = item[1].ToString();
                    tObj.LName = item[2].ToString();
                    if (item[3] == System.DBNull.Value)
                        tObj.Address = string.Empty;
                    else
                        tObj.Address = item[3].ToString();
                    tObj.DOB = Convert.ToDateTime(item[4]);
                    if (item[5] == System.DBNull.Value)
                        tObj.City = string.Empty;
                    else
                        tObj.City = item[5].ToString();
                    if (item[7] == System.DBNull.Value)
                        tObj.MobileNo = 0;
                    else
                        tObj.MobileNo = Convert.ToInt64(item[7]);
                    tObj.IsAdmin =Convert.ToByte(item[8]);
                    //Adding Vendor object to List
                    myTeacherList.Add(tObj);

                }
            }

            sdr.Close();
            con.Close();

            return myTeacherList;
        }
        /// <summary>
        /// Definition: SelectAllCourses is used to display all Courses
        /// Name: Mridul and Prathamesh
        /// </summary>
        /// <returns>
        /// Returns the list of Courses
        /// </returns>
        public List<Course> SelectAllCourses()
        {
            List<Course> myCoursesList = null;
            con.Open();

            cmd = new SqlCommand();
            cmd.CommandText = "Select * from Courses";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            sdr = cmd.ExecuteReader();

            //Loading Data from Reader in to Table
            dt = new DataTable();
            dt.Load(sdr);

            //Converting DataTable to List<Vendor>
            if (dt.Rows.Count > 0)
            {
                myCoursesList = new List<Course>();

                foreach (DataRow item in dt.Rows)
                {
                    Course cObj = new Course();

                    cObj.CourseID = Convert.ToInt32(item[0]);
                    cObj.CourseName = item[1].ToString();
                    cObj.StartDate = Convert.ToDateTime(item[2]);
                    cObj.EndDate = Convert.ToDateTime(item[3]);
                    //Adding Vendor object to List
                    myCoursesList.Add(cObj);

                }
            }

            sdr.Close();
            con.Close();

            return myCoursesList;
        }
        /// <summary>
        /// Definition: getPassword is used to get the password of a particular teacher where TeacherID is supplied
        /// Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns>
        /// Returns the password
        /// </returns>
        public string GetPassword(int teacherID)
        {
            con.Open();
            string password = null;
            SqlParameter param = new SqlParameter("@teacherID", teacherID);
            cmd = new SqlCommand();
            cmd.CommandText = "select password from Teacher where teacherID = @teacherID AND IsAdmin = 1";
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
        /// Definition: Assign teacher to a course
        /// Name: Mridul and Prathamesh
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns>
        /// Returns the password
        /// </returns>
        public bool AssignCourse(int teacherID,int courseID)
        {
            con.Open();
            SqlParameter s1;
            s1 = new SqlParameter("@teacherID", teacherID);
            SqlParameter s2 = new SqlParameter("@courseID", courseID);
            cmd = new SqlCommand();
            cmd.CommandText = "insert into Teaches values(@teacherID,@courseID)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.Parameters.Add(s1);
            cmd.Parameters.Add(s2);
            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            if (affectedRows > 0)
                return true;
            else
                return false;
        }


    }

}
