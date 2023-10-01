using System;
using System.Collections.Generic;
using System.Text;
using Prometheus_DAL;
using Prometheus_Entities;

namespace Prometheus_BL
{
    /// <summary>
    /// Wrtten by Hrithik and Navpreet
    /// </summary>
    public class Student_BL
    {
        Student_DAO studentOBJ = null;
        /// <summary>
        /// Check if password is corrct or not
        /// </summary>
        /// <param name="studentID">integer value for student id</param>
        /// <param name="pass">string password value </param>
        /// <returns></returns>
        public bool LoginStudent(int studentID,string pass)
        {
            studentOBJ = new Student_DAO();
            string password = studentOBJ.GetPasswordStudent(studentID);
            if (password == pass)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Update student details
        /// </summary>
        /// <param name="obj">object of student entity</param>
        /// <returns>returns true if updated successfully</returns>
        public bool UpdateDetails(Student obj)
        {
            studentOBJ = new Student_DAO();
            bool res = studentOBJ.UpdateDeatails(obj);
            return res;
        }
        /// <summary>
        /// Emroll student into a course
        /// </summary>
        /// <param name="courseID">Course id</param>
        /// <param name="studentID">Student id</param>
        /// <returns>returns true if enrolled successfully</returns>
        public bool EnrollStudent(int courseID,int studentID)
        {
            studentOBJ = new Student_DAO();
            bool res = studentOBJ.ApplyCourse(courseID,studentID);
            return res;
        }
        /// <summary>
        /// Show all the available courses for student
        /// </summary>
        /// <param name="studentID">Student id</param>
        /// <returns>list of courses</returns>
        public List<Course> ShowAvailableCourses(int studentID)
        {
            studentOBJ = new Student_DAO();
            List <Course> courses = new List<Course>();
            courses = studentOBJ.ShowAllCoursesAvailable(studentID);
            return courses;
        }
        /// <summary>
        /// Show the applied courses for a student
        /// </summary>
        /// <param name="studentID">student ID</param>
        /// <returns>list of applied courses</returns>
        public List<Course> ShowAppliedCourses(int studentID)
        {
            studentOBJ = new Student_DAO();
            List<Course> courses = new List<Course>();
            courses = studentOBJ.ShowAllCoursesApplied(studentID);
            return courses;
        }
        /// <summary>
        /// Show all the hw availabe
        /// </summary>
        /// <param name="studentID">Student Id</param>
        /// <returns>List of availabe hw</returns>
        public List<HomeWork> ShowAvailableHW(int studentID)
        {
            studentOBJ = new Student_DAO();
            List<HomeWork> homeworks = new List<HomeWork>();
            homeworks = studentOBJ.ShowAllHomework(studentID);
            return homeworks;
        }
        /// <summary>
        /// Function to change the password
        /// </summary>
        /// <param name="studentID">Student id</param>
        /// <param name="password">new password</param>
        /// <returns>true if password updated successfully</returns>
        public bool ChangePassword(int studentID,string password)
        {
            bool res = studentOBJ.ChangePassword(studentID,password);
            return res;
        }
        /// <summary>
        /// To sort and get highest priority hw
        /// </summary>
        /// <param name="hw">list of hw</param>
        /// <returns>Prioritised list of hw</returns>
        public List<HomeWork> PlanHW(List<HomeWork> hw , out string plan)
        {
            List<HomeWork> homeworks = new List<HomeWork>();
            //List<HomeWork> SortedList = homeworks.OrderBy(o => o.Deadline).ToList();
            hw.Sort((x, y) => x.Deadline.CompareTo(y.Deadline));
            var time = hw[0].Deadline - DateTime.Today;
            float days = time.Days;
            if (days > 0.0f)
            {
                float hoursPerDay = (float)hw[0].ReqTime / days;
                if (hoursPerDay > 0.5f)
                    plan = $"Devote {hoursPerDay:00.00} hour/s per day to finish on time";
                else
                    plan = "You can relax as the closest deadline is very far";
            }
            
            else
                plan = "You were unable to submit on time, do it ASAP";
            return hw;
            
        }
        /// <summary>
        /// Search Student details from Student Table By Student ID 
        /// </summary>
        public Student SearchstudbyID(int studID)
        {
            return studentOBJ.SearchStudentByID(studID);
        }


    }
}
