using System;
using Prometheus_Exceptions;
using System.Collections.Generic;
using System.Text;
using Prometheus_BL;
using Prometheus_Entities;
using ConsoleTables;
using System.Threading;

namespace Prometheus_UI
{
    public class Teacher_UI
    {

        int teacherId;

        /// <summary>
        /// Definition : Method to clear console and then print Title
        /// Name : Amaan Nizam
        /// </summary>
        static void Clear()
        {
            Console.Clear();
            Title();
        }

        /// <summary>
        /// Definition : Method to print Title in console
        /// Name : Amaan Nizam
        /// </summary>

        static void Title()
        {
            string title = "PROMETHEUS - HOMEWORK MANAGEMENT SYSTEM";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            title = "========================================================================================================================";
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
            string str = "WELCOME TEACHER";
            Console.SetCursorPosition((Console.WindowWidth - str.Length) / 2, Console.CursorTop);
            Console.WriteLine(str);
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Definition: Method to Mask the password with "*"
        /// Name : Amaan Nizam
        /// </summary>
        /// <returns></returns>
        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }

        /// <summary>
        /// Definition: Method to show Menu before Logging in to system
        /// Name : Amaan Nizam
        /// </summary>
        void PreLogin()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine("\tEnter Your Details");
            Console.WriteLine("\t===============================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\tEnter ID as 0 to Exit to Home\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\tEnter Your TeacherID : ");
            try
            {
                teacherId = Convert.ToInt32(Console.ReadLine());
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tPlease Enter Valid ID.");
                Console.ForegroundColor = ConsoleColor.White;
                TeacherMain();
            }
            if (teacherId == 0)
            {
                MainUI.Main();
            }
            Console.Write("\tEnter Password : ");
            var password = ReadPassword();

            Teacher_BL teacherBLobj = new Teacher_BL();

            if (teacherBLobj.LoginTeacher(teacherId, password))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\tLogin Successful!");
                Console.ForegroundColor = ConsoleColor.White;
                //add delay of 1s
                Thread.Sleep(1000);
                Clear();
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\tInvalid Credentials! Please Try Again! ");
                Console.ForegroundColor = ConsoleColor.White;
                //add delay of 1s
                Thread.Sleep(1000);
                Clear();
                //go back to Pre Logging in menu
                this.PreLogin();
            }
        }

        /// <summary>
        /// Definition : Method to show Menu after successfully logging in
        /// Name : Amaan Nizam
        /// </summary>
        static void AfterLogin()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Welcome To Teacher Portal");
            Console.WriteLine("==============================================");
            Console.WriteLine("\t1. Update Your Details. ");
            Console.WriteLine("\t2. Change Your Password. ");
            Console.WriteLine("\t3. Show All Courses. ");
            Console.WriteLine("\t4. Show All Students for Course. ");
            Console.WriteLine("\t5. Show All Homework Assignments. ");
            Console.WriteLine("\t6. Add Homework for Course.  ");
            Console.WriteLine("\t7. Logout. ");
            Console.WriteLine("==============================================");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Definition : Method to print course list in table format for better readability
        /// Name : Amaan Nizam
        /// </summary>
        /// <param name="courseList"></param>
        static void PrintCourseList(List<Course> courseList)
        {
            var table = new ConsoleTable("CourseID", "CourseName", "Start Date", "End Date");
            foreach (var item in courseList)
            {
                table.AddRow(item.CourseID, item.CourseName, item.StartDate, item.EndDate);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            table.Write(Format.Alternative);
        }

        /// <summary>
        /// Definition : Method to print student list in table format for better readability
        /// Name : Amaan Nizam
        /// </summary>
        /// <param name="studentList"></param>
        static void PrintStudentList(List<Student> studentList)
        {
            var table = new ConsoleTable("StudentID", "First Name", "Last Name", "Date of Birth", "City");
            foreach (var item in studentList)
            {
                table.AddRow(item.StudentID, item.FName, item.LName, item.DOB, item.City);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            table.Write(Format.Alternative);
        }

        /// <summary>
        /// Definition : Method to print homework list in table format for better readability
        /// Name : Amaan Nizam
        /// </summary>
        /// <param name="homeworkList"></param>
        static void PrintHomeWorkList(List<HomeWork> homeworkList)
        {
            var table = new ConsoleTable("HomeworkID", "Description", "Deadline", "Required Time", "Long Description");
            foreach (var item in homeworkList)
            {
                table.AddRow(item.HomeWorkID, item.Description, item.Deadline, item.ReqTime, item.LongDescription);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            table.Write(Format.Alternative);
        }

        /// <summary>
        /// Main method
        /// </summary>
        public static void TeacherMain()
        {
            Clear();
            int choice = 0;
            //exception class object initialization
            TestException testException = new TestException();
            //teacher UI object initilization
            Teacher_UI teacherUIobj = new Teacher_UI();
            //teacher BL object initialization
            Teacher_BL teacherBLobj = new Teacher_BL();
            //showing Pre login menu
            teacherUIobj.PreLogin();

            do
            {
                Clear();
                //show after login menu
                AfterLogin();

                Console.WriteLine();
                Console.Write("Enter Your Choice : ");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            try
                            {
                                Teacher teacher = teacherBLobj.SearchTeacher(teacherUIobj.teacherId);
                                if (teacher != null)
                                {
                                    var tableTeacher = new ConsoleTable("TeacherID", "First Name", "Last Name", "Address", "DOB", "City", "MobileNo");
                                    tableTeacher.AddRow(teacher.TeacherID, teacher.FName, teacher.LName, teacher.Address, teacher.DOB.ToString("dd/MM/yyyy"), teacher.City, teacher.MobileNo);
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    tableTeacher.Write(Format.Alternative);
                                }

                                Console.Write("Enter Your First Name: ");
                                teacher.FName = Console.ReadLine();
                                //Validating Name as per Exceptions
                                testException.ValidateName(teacher.FName);

                                Console.Write("Enter Your Last Name: ");
                                teacher.LName = Console.ReadLine();
                                //Validating Name as per Exceptions
                                testException.ValidateName(teacher.LName);

                                Console.Write("Enter Your Address: ");
                                teacher.Address = Console.ReadLine();

                                Console.Write("Enter Your Date of Birth(DD/MM/YYYY): ");
                                teacher.DOB = Convert.ToDateTime(Console.ReadLine());
                                testException.ValidateTeacherDOB(teacher.DOB);

                                Console.Write("Enter Your City: ");
                                teacher.City = Console.ReadLine();

                                Console.Write("Enter Your Mobile No.: ");
                                teacher.MobileNo = Convert.ToInt64(Console.ReadLine());
                                //Validating Mobile Number
                                testException.ValidateMobileNo(teacher.MobileNo);

                                teacher.TeacherID = teacherUIobj.teacherId;
                                teacherBLobj = new Teacher_BL();
                                if (teacherBLobj.UpdateTeacherDetails(teacher))
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Update Successful!");
                                    Console.ForegroundColor = ConsoleColor.White;

                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Please Try again!");
                                    Console.ForegroundColor = ConsoleColor.White;

                                }
                                
                            }
                            catch (InvalidNameException e)
                            {
                                Console.WriteLine(e.Message);
                                
                            }
                            catch (InvalidDOBException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (InvalidMobileNoException e)
                            {
                                Console.WriteLine(e.Message);
                                
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nPlease ensure you entered correct details and try again");
                                //Console.WriteLine(e.Message);
                                Console.ForegroundColor = ConsoleColor.White;


                            }
                            Console.WriteLine("\nPress any key to continue\n");
                            Console.ReadKey();
                            break;
                        case 2:
                            //change password
                            try
                            {
                                Console.Write("Please enter new password(length between 4 and 20): ");
                                var password = ReadPassword();
                                testException.ValidatePassword(password);
                                if (teacherBLobj.ChangePasswordTeacher(teacherUIobj.teacherId, password))
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Password Updated Successfully!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Please Try again!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                
                            }
                            catch (InvalidPasswordException e)
                            {
                                Console.WriteLine(e.Message);
                                
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\nPlease ensure you entered correct details and try again");
                               
                            }
                            Console.WriteLine("\nPress any key to continue\n");
                            Console.ReadKey();
                            break;

                        case 3:
                            //show all courses
                            teacherBLobj = new Teacher_BL();

                            try
                            {
                                List<Course> myList = teacherBLobj.ShowAllCourses(teacherUIobj.teacherId);
                                if (myList!=null)
                                {
                                    PrintCourseList(myList);
                                }
                                else
                                {
                                    
                                    Console.WriteLine("\nYou are not teaching any course!");
                                    Console.ForegroundColor = ConsoleColor.White;                                    
                                }
                                
                            }                           
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Something went wrong please try again\nSorry for inconvience");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine("\nPress any key to continue\n");
                            Console.ReadKey();
                            break;

                        case 4:
                            //show all students
                            teacherBLobj = new Teacher_BL();

                            try
                            {
                                List<Student> myList1 = teacherBLobj.ShowAllStudents(teacherUIobj.teacherId);
                                if (myList1!=null)
                                {
                                    PrintStudentList(myList1);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nNo Students have enrolled for any of your courses!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                
                            }                            
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Something went wrong please try again\nSorry for inconvience");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine("\nPress any key to continue\n");
                            Console.ReadKey();
                            break;

                        case 5:
                            //show homework list
                            teacherBLobj = new Teacher_BL();

                            try
                            {
                                List<HomeWork> myList2 = teacherBLobj.ShowHomeWorkAssignments(teacherUIobj.teacherId);
                                if (myList2!=null)
                                {
                                    PrintHomeWorkList(myList2);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nNo Homework Assignments Available!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                
                            }                            
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Something went wrong please try again\nSorry for inconvience");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine("\nPress any key to continue\n");
                            Console.ReadKey();
                            //PrintHomeworkList(myList2);
                            break;

                        case 6: //add hw
                            HomeWork homework = new HomeWork();

                            List<Course> myList3 = teacherBLobj.ShowAllCourses(teacherUIobj.teacherId);
                            if (myList3 != null)
                            {
                                PrintCourseList(myList3);

                                Console.Write("Enter the Course ID: ");
                                int courseID = Convert.ToInt32(Console.ReadLine());
                                if (teacherBLobj.CheckCourse(myList3, courseID))
                                {
                                    Console.Write("Enter the Description: ");
                                    homework.Description = Console.ReadLine();
                                    Console.Write("Enter the Deadline(in hours): ");
                                    homework.Deadline = Convert.ToDateTime(Console.ReadLine());
                                    Console.Write("Enter the Required Time: ");
                                    homework.ReqTime = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter the Long Description: ");
                                    homework.LongDescription = Console.ReadLine();

                                    teacherBLobj = new Teacher_BL();
                                    if (teacherBLobj.AddToHomeWork(homework, teacherUIobj.teacherId, courseID))
                                    {
                                        Console.WriteLine();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Homework Added!");
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Please Try again");
                                        Console.WriteLine();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("You do not teacch this course");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("\nPress any key to continue\n");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else
                                Console.WriteLine("Sorry, you dont teach any course");
                            Console.WriteLine("\nPress any key to continue\n");
                            Console.ReadKey();
                            break;
                        case 7:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n Signing off...............");
                            Thread.Sleep(1000);
                            string str = "THANK YOU TEACHER";
                            Console.SetCursorPosition((Console.WindowWidth - str.Length) / 2, Console.CursorTop);
                            Console.WriteLine(str);
                            TeacherMain();
                            break;
                        default:
                            Console.WriteLine("Invalid Input please Enter between 1-7");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;               
                    
                    Console.WriteLine("Invalid Input please Enter between 1-7");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPress any key to continue\n");
                    Console.ReadKey();
                }
            } while (choice != 7);
        }
    }
}
