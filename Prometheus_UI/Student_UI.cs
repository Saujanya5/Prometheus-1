using System;
using System.Collections.Generic;
using Prometheus_Exceptions;
using Prometheus_BL;
using Prometheus_Entities;
using ConsoleTables;
using System.Threading;


namespace Prometheus_UI
{
    /// <summary>
    /// Written By Hrithik, Navpreet
    /// </summary>
    public class Student_UI
    {
        int studentId; // global level variable to store login id
        /// <summary>
        /// To clear the console and reprint the title
        /// </summary>
        static void Clear()
        {
            Console.Clear();
            Title();
        }
        /// <summary>
        /// Print the title
        /// </summary>
        static void Title()
        {
            string title = "PROMETHEUS";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            title = "========================================================================================================================";
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
            string str = "WELCOME STUDENT";
            Console.SetCursorPosition((Console.WindowWidth - str.Length) / 2, Console.CursorTop);
            Console.WriteLine(str);
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.White;

        }
        /// <summary>
        /// Mask the password
        /// </summary>
        /// <returns>String of password</returns>
        static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;

        }
        /// <summary>
        /// Log into the system
        /// </summary>
        void PreLogin()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tEnter Your Details");
            Console.WriteLine("\t===============================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\tEnter ID as 0 to Exit to Home");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n\tEnter Your StudentID : ");
            try
            {
                studentId = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\tPlease Enter Valid ID");
                Console.ForegroundColor = ConsoleColor.White;
                StudentMain();
            }
            if(studentId == 0)
            {
                MainUI.Main();
            }
            Console.Write("\tEnter Password : ");
            string pass = GetPassword();

            Student_BL sblobj = new Student_BL();
            if (sblobj.LoginStudent(studentId, pass))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\tLogin Successful");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                Clear();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\tInvalid Credentials");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                Clear();
                this.PreLogin();
            }
        }
        /// <summary>
        /// Menu to be printed after log in
        /// </summary>
        static void AfterLogin()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Welcome To Student Portal");
            Console.WriteLine("==============================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t1.Update details. ");
            Console.WriteLine("\t2.Show available courses. ");
            Console.WriteLine("\t3.Show Enrolled courses. ");
            Console.WriteLine("\t4.Enroll for courses. ");
            Console.WriteLine("\t5.View Homework. ");
            Console.WriteLine("\t6.Change Password. ");
            Console.WriteLine("\t7.Log Out. ");
            Console.WriteLine("==============================================");
            Console.ForegroundColor = ConsoleColor.White;

        }
        /// <summary>
        /// Print the course list in an tabular format
        /// </summary>
        /// <param name="courseList">list of courses</param>
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
        /// Contains main flow of student module
        /// </summary>
        public static void StudentMain()
        {
            
            Clear();
            int choice;
            
            Student_UI uiObj = new Student_UI();  //Initializing StudentUI object
            Student_BL blObj = null;  //Declaring StudentBL object
           
            TestException testException = new TestException();   //exception class object initialization
            
            uiObj.PreLogin(); //calling log in
            blObj = new Student_BL();
            string plan = String.Empty;
            List<HomeWork> homeworks = blObj.ShowAvailableHW(uiObj.studentId);// get list of hw for notifing student
            
            
            do
            {
                Clear();
                AfterLogin();
                if (homeworks!=null) //checking if there are pending hw or not
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Homework with closest deadline ");
                    homeworks = blObj.PlanHW(homeworks, out plan); // get the highes priority hw
                    var table = new ConsoleTable("Priority HW", "Deadline", "CourseName");
                    table.AddRow(homeworks[0].Description, homeworks[0].Deadline, homeworks[0].CourseName);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    table.Write(Format.Alternative);
                    Console.ForegroundColor = ConsoleColor.White;
                    //printing the heshest priority hw
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nNo homework Pending.....Great!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("Enter Your Choice :");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:  //Case to update student data
                            try
                            {
                                Student student = blObj.SearchstudbyID(uiObj.studentId);
                                if (student != null)
                                {
                                    var tableStudent = new ConsoleTable("StudentID", "First Name", "Last Name", "Address", "DOB", "City", "MobileNo");
                                    tableStudent.AddRow(student.StudentID, student.FName, student.LName, student.Address, student.DOB.ToString("dd/MM/yyyy"), student.City, student.MobileNo);
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    tableStudent.Write(Format.Alternative);
                                }
                                student = new Student();
                                Console.Write("Enter Your First Name: ");
                                student.FName = Console.ReadLine();
                                //validating student Name
                                testException.ValidateName(student.FName);

                                Console.Write("Enter Your Last Name: ");
                                student.LName = Console.ReadLine();
                                //Validating Name as per Exceptions
                                testException.ValidateName(student.LName);
                                Console.Write("Enter Your Address: ");
                                student.Address = Console.ReadLine();
                                Console.Write("Enter Your Date of Birth(DD/MM/YYYY): ");
                                student.DOB = Convert.ToDateTime(Console.ReadLine());
                                testException.ValidateStudentDOB(student.DOB);
                                Console.Write("Enter Your City: ");
                                student.City = Console.ReadLine();
                                Console.Write("Enter Your Mobile No.: ");
                                student.MobileNo = Convert.ToInt64(Console.ReadLine());

                                testException.ValidateMobileNo(student.MobileNo); //validating mobile No


                                student.StudentID = uiObj.studentId;
                                blObj = new Student_BL();
                                if (blObj.UpdateDetails(student))
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
                                    Console.WriteLine("Please Try Again!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                            }
                            catch (InvalidNameException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (InvalidMobileNoException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch(InvalidDOBException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n\n\tPlease ensure you entered correct details and try again");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine("\nPress any key to continue");
                            Console.ReadKey();
                            break;
                        case 2: // case to show available courses
                            blObj = new Student_BL();

                            List<Course> coursesAvailable = blObj.ShowAvailableCourses(uiObj.studentId);
                            if (coursesAvailable != null)
                                PrintCourseList(coursesAvailable);
                            else
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.WriteLine("No Courses Available Currently!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            Console.WriteLine("\nPress any key to continue");
                            Console.ReadKey();
                            break;
                        case 3: // case to show applied courses
                            blObj = new Student_BL();

                            List<Course> coursesApplied = blObj.ShowAppliedCourses(uiObj.studentId);
                            if(coursesApplied != null)
                                PrintCourseList(coursesApplied);
                            else
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.WriteLine("\nNot enrolled in any course");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine("\nPress any key to continue");
                            Console.ReadKey();
                            break;
                        case 4: //case to apply for the course
                            try
                            {
                                blObj = new Student_BL();

                                List<Course> courses = blObj.ShowAvailableCourses(uiObj.studentId);
                                if (courses != null)
                                {
                                    PrintCourseList(courses);
                                    Console.Write("Enter the course you want to enroll for ");
                                    int courseId = Convert.ToInt32(Console.ReadLine());

                                    blObj = new Student_BL();
                                    if (blObj.EnrollStudent(courseId, uiObj.studentId))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Enrolled Successfully!");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Please Try again!");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("\nNo course available to enroll for");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    
                                }
                            }
                            catch
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Enter valid course ID");
                                Console.ForegroundColor = ConsoleColor.White;
                                
                            }
                            Console.WriteLine("\n\tPress any key to continue");
                            Console.ReadKey();
                            break;
                        case 5: // show all the available hw
                            try
                            {
                                blObj = new Student_BL();
                                homeworks = blObj.ShowAvailableHW(uiObj.studentId);

                                if (homeworks != null)
                                {
                                    homeworks = blObj.PlanHW(homeworks, out plan);
                                    var table = new ConsoleTable("HomeworkID", "Description", "Deadline", "Required time", "Long description", "Course Name");
                                    foreach (var item in homeworks)
                                    {
                                        table.AddRow(item.HomeWorkID, item.Description, item.Deadline, item.ReqTime, item.LongDescription, item.CourseName);
                                    }
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    table.Write(Format.Alternative);
                                    //PrintHomeWrok();
                                    Console.WriteLine("\n"+plan);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("\n\nNo homework Pending.....Great!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            catch
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n\nSomething went wrong please try gain lated\nSorry for inconvience");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine("\n\tPress any key to continue");
                            Console.ReadKey();
                            break;
                        case 6:// case to change password 
                            try
                            {
                                Console.Write("\nPlease enter new password (between 4 and 20 characters) ");
                                string password = GetPassword();
                                testException.ValidatePassword(password);
                                if (blObj.ChangePassword(uiObj.studentId, password))
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nUpdations successful!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nPlease Try again");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                               
                            }
                            catch (InvalidPasswordException e)
                            {
                                Console.WriteLine(e.Message);
                                
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nPlease ensure you entered correct details and try again");
                                Console.ForegroundColor = ConsoleColor.White;

                            }
                            Console.WriteLine("\nPress any key to continue\n");
                            Console.ReadKey();
                            break;
                        case 7: // case to sign out
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n Signing off...............");
                            Thread.Sleep(1000);
                            string str = "THANK YOU Student";
                            Console.SetCursorPosition((Console.WindowWidth - str.Length) / 2, Console.CursorTop);
                            Console.WriteLine(str);
                            StudentMain();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n\tPlease Enter Valid Choice");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\tPlease Enter Valid Choice");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPress any key to continue\n");
                    Console.ReadKey();
                    choice = 0;
                }
            } while (choice!= 7);


        }
    }
}
