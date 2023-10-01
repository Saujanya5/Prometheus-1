using System;
using System.Collections.Generic;
using System.Text;
using Prometheus_DAL;
using Prometheus_Entities;
namespace Prometheus_BL
{
    /// <summary>
    /// This class has all the functions which are used to interact with the UI layer of the Admin module 
    /// Mridul and Prathamesh
    /// </summary>
    public class Admin_BL
    {
        Admin_DAO adminObj = new Admin_DAO();
        /// <summary>
        ///Adds student to the Student Table with the required or not null values.
        /// </summary>
        public bool AddStudents(String fn, String ln, DateTime dob,string pass)
        {
            Student studObj = new Student();
            studObj.FName = fn;
            studObj.LName = ln;
            studObj.DOB = dob;
            return adminObj.AddStudent(studObj, pass);
        }
        /// <summary>
        /// Deletes students from student table by using Student ID
        /// </summary>
        public bool DelStudByID(int studdelID)
        {
            return adminObj.DeleteStudentByID(studdelID);
        }
        /// <summary>
        /// Search Student details from Student Table By Student ID 
        /// </summary>
        public Student SearchstudbyID(int studID)
        {
            return adminObj.SearchStudentByID(studID);
        }
        /// <summary>
        /// Displays All the students details in Student Table
        /// </summary>
        public List<Student> ViewAllStuds()
        {
            List<Student> studentList = adminObj.SelectAllStudents();
            return studentList;
        }
        /// <summary>
        /// Adds Teacher to the Teacher Table with the required or not null values.
        /// </summary>
        public bool AddTeachers(String fn, String ln, DateTime dob,string pass)
        {
            Teacher teacherobj = new Teacher();
            teacherobj.FName = fn;
            teacherobj.LName = ln;
            teacherobj.DOB = dob;
            return adminObj.AddTeacher(teacherobj, pass);
        }
        /// <summary>
        /// Deletes Teacher from Teacher table by using Teacher ID
        /// </summary>
        public bool DelTeacher(int teachdelID)
        {
           return adminObj.DeleteTeacherByID(teachdelID);
        }
        /// <summary>
        /// Search Teacher details from Teacher Table By Teacher ID
        /// </summary>
        public Teacher SearchTeacher(int teachID)
        {
            return adminObj.SearchTeacherByID(teachID);
        }
        /// <summary>
        /// Displays All the Teachers details in Techer Table
        /// </summary>
        public List<Teacher> ViewAllTchrs()
        {
            List<Teacher> teacherList =  adminObj.SelectAllTeachers();
            return teacherList;
        }
        /// <summary>
        ///  Adds Course to the Courses Table with the required or not null values.
        /// </summary>
        public bool AddCourses(String cn, DateTime strtdate, DateTime enddate)
        {
            Course cobj = new Course();
            cobj.CourseName = cn;
            cobj.StartDate = strtdate;
            cobj.EndDate = enddate;
            return adminObj.AddCourse(cobj);
        }
        /// <summary>
        /// Deletes Course from Courses table by using Course ID
        /// </summary>
        public bool DelCourses(int coursedelID)
        {
           return adminObj.DeleteCourseByID(coursedelID);
        }
        /// <summary>
        /// Search Course details from Courses Table By Course ID
        /// </summary>
        public Course SearchCourses(int courseID)
        {
           return adminObj.SearchCourseByID(courseID);
        }
        /// <summary>
        /// Displays All the Course details in Courses Table
        /// </summary>
        public List<Course> ViewAllCourses()
        {
           List<Course> courseList =  adminObj.SelectAllCourses();
            return courseList;
        }
        /// <summary>
        /// Login function to check Teacher ID and password for Admin access 
        /// </summary>
        public bool LoginAdmin(int teacherID, string pass)
        {
            adminObj = new Admin_DAO();
            string password = adminObj.GetPassword(teacherID);
            if (password == pass)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Dislpays Teacher Table with Admin Status and adds Admin to any of the non-Admin Teacher using there Teacher ID
        /// </summary>
        public bool AddAdmin(int teacherID)
        {
            return adminObj.AddAdmin(teacherID);
        }
        /// <summary>
        ///  assign courses to teachers
        /// </summary>
        public bool AssignCourse(int teacherID,int courseID)
        {
            return adminObj.AssignCourse(teacherID, courseID);
        }
    }
}
