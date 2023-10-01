using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Prometheus_DAL;
using Prometheus_Entities;
using Prometheus_Exceptions;

namespace Prometheus_BL
{
    /// <summary>
    /// This class has all the functions which are used to interact with the UI layer of the Teacher module 
    /// Hrishikesh and Saujanya
    /// </summary>
    public class Teacher_BL
    {
        Teacher_DAO tDAO = null;

        public Teacher_BL()
        {
            tDAO = new Teacher_DAO();
        }
        /// <summary>
        /// Enter teacher login details
        /// </summary>
        /// <param name="teacherID">Teacher ID</param>
        /// <param name="pass">password</param>
        /// <returns>True if login credantials correct</returns>
        public bool LoginTeacher(int teacherID, string pass)
        {
            tDAO = new Teacher_DAO();
            string password = tDAO.GetPasswordTeacher(teacherID);
            if (password == pass)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Change password for Teacher 
        /// </summary>
        /// <param name="teacherID">Teacher ID</param>
        /// <param name="password">password</param>
        /// <returns>True if password changed</returns>
        public bool ChangePasswordTeacher(int teacherID, string password)
        {
            tDAO = new Teacher_DAO();
            bool res = tDAO.ChangePasswordTeacher(teacherID, password);
            return res;
        }
        /// <summary>
        /// Display all the courses which that particular teacher teaches
        /// </summary>
        /// <param name="TeacherID">Teacher ID</param>
        /// <returns>Returns list of courses</returns>
        public List<Course> ShowAllCourses(int TeacherID)
        {
            tDAO = new Teacher_DAO();
            return tDAO.ShowAllCourses(TeacherID);
        }

        /// <summary>
        /// Display the list of Students of that particular teacher
        /// </summary>
        /// <param name="TeacherID"></param>
        /// <returns>Returns list of students</returns>
        public List<Student> ShowAllStudents(int TeacherID)
        {
            tDAO = new Teacher_DAO();
            return tDAO.ShowAllStudents(TeacherID);
        }

        /// <summary>
        /// Update details of teacher
        /// </summary>
        /// <param name="teacherObj">Object of teacher entity</param>
        /// <returns>True if update successful</returns>
        public bool UpdateTeacherDetails(Teacher teacherObj)
        {
            tDAO = new Teacher_DAO();
            return tDAO.UpdateTeacherDetails(teacherObj);
        }

        /// <summary>
        /// Get all the hw assignments
        /// </summary>
        /// <param name="TeacherID">Teacher ID</param>
        /// <returns>list of hw</returns>
        public List<HomeWork> ShowHomeWorkAssignments(int TeacherID)
        {
            tDAO = new Teacher_DAO();
            return tDAO.ShowHomeWorkAssignments(TeacherID);
        }

        /// <summary>
        /// Add hw
        /// </summary>
        /// <param name="homeworkObj">object of hw entity</param>
        /// <param name="teacherID">Teacher ID</param>
        /// <param name="courseID">Course ID</param>
        /// <returns>True if hw added successfully</returns>
        public bool AddToHomeWork(HomeWork homeworkObj, int teacherID, int courseID)
        {
            tDAO = new Teacher_DAO();
            if (tDAO.AddToHomeWork(homeworkObj))
            {
                int homeworkID = tDAO.GetLatestHW();

                if (tDAO.AddToAssignment(homeworkID, teacherID, courseID))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Cheack for course
        /// </summary>
        /// <param name="myList3"></param>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public bool CheckCourse(List<Course> myList3, int courseID)
        {
            foreach (var item in myList3)
            {
                if (item.CourseID == courseID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Search Teacher details from Teacher Table By Teacher ID
        /// </summary>
        public Teacher SearchTeacher(int teachID)
        {
            tDAO = new Teacher_DAO();
            return tDAO.SearchTeacherByID(teachID);
        }

    }
}
