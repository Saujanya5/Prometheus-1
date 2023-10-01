using Prometheus_BL;
using System;
using System.Collections.Generic;
using Prometheus_Exceptions;
using System.Threading;
using Prometheus_Entities;
using ConsoleTables;
using System.Data.SqlClient;

namespace Prometheus_UI
{
    /// <summary>
    /// Name : Mridul and Prathamesh
    /// </summary>
    public class Admin_UI
    {
        int adminID;
        /// <summary>
        /// Definition : Method to print Title in console and clear previous data
        /// Name : Mridul and Prathamesh
        /// </summary>
        static void Close()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string title = "PROMETHEUS - HOMEWORK MANAGMENT SYSTEM";
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
            title = "========================================================================================================================";
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title); 
            string str = "WELCOME ADMIN";
            Console.SetCursorPosition((Console.WindowWidth - str.Length) / 2, Console.CursorTop);
            Console.WriteLine(str);
            title = "========================================================================================================================";
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
        }
        /// <summary>
        /// Definition: Method to get password and Mask the password with "*"
        /// Name : Mridul and Prathamesh
        /// </summary>
        /// <returns></returns>
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
        /// Definition: Method to show Menu before Logging in to system
        /// Name : Mridul and Prathamesh
        /// </summary>
        void PreLogin()
        {
            Close();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\tEnter Your Details");
            Console.WriteLine("\t===============================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\tEnter ID as 0 to Exit to Home");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n\tEnter Your AdminID : ");
            try
            {
                adminID = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\t Enter Valid ID");
                Console.WriteLine("\n\t\tEnter any key to continue");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;
                AdminMain();
            }
            if (adminID == 0)
            {
                MainUI.Main();
            }
            Console.Write("\tEnter Password : ");
            string pass = GetPassword();

            Admin_BL adminObj = new Admin_BL();

            if (adminObj.LoginAdmin(adminID, pass))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\tLogin Successful!");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                Close();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\tEither Password is wrong or you do not have admin access.\n\tTry Teacher Login?");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(3000);
                Close();
                this.PreLogin();
            }
        }
        public static void AdminMain()
        {
            Close();
            Admin_UI admin = new Admin_UI();
            admin.PreLogin();
            int choice;
            //exception class object initialization
            TestException testException = new TestException();
            /// <summary>
            /// Definition : Method to show Menu after successfully logging in and options selection
            /// Name : Mridul and Prathamesh
            /// </summary>
            void MainMenu()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("MAIN MENU");
                Console.WriteLine("===============================================");
                Console.WriteLine("Choose an appropriate option:-");
                Console.WriteLine();
                Console.WriteLine("\t1.Manage Students. ");
                Console.WriteLine("\t2.Manage Teachers. ");
                Console.WriteLine("\t3.Manage Courses. ");
                Console.WriteLine("\t4.Switch to Teacher Mode. ");
                Console.WriteLine("\t5.Exit. ");
                Console.WriteLine("==============================================");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nEnter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        studentopt();
                        break;

                    case 2:
                        teacheropt();
                        break;

                    case 3:
                        coursesopt();
                        break;

                    case 4:
                        switchopt();
                        break;

                    case 5:
                        exitopt();
                        break;

                    default:
                        Console.WriteLine("\n Please enter valid value between 1 to 5  ");
                        break;
                }
            }
            Close();
            MainMenu();
            /// <summary>
            /// Definition : Method to show Selection of Student option and various Operations Such as add, delete, search, view all and main menu
            /// Name : Mridul and Prathamesh
            /// </summary>
            void studentopt()
            {
                int choice;
                do
                {
                    Close();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    string title = "========================================================================================================================";
                    Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
                    Console.WriteLine(title); string str = "STUDENT MANAGMENT";
                    Console.SetCursorPosition((Console.WindowWidth - str.Length) / 2, Console.CursorTop);
                    Console.WriteLine(str);
                    title = "========================================================================================================================";
                    Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
                    Console.WriteLine(title); Console.WriteLine("Choose an appropriate value:-");
                    Console.WriteLine();
                    Console.WriteLine("\t1.Add Students. ");
                    Console.WriteLine("\t2.Delete Students. ");
                    Console.WriteLine("\t3.Search Students. ");
                    Console.WriteLine("\t4.View All Students. ");
                    Console.WriteLine("\t5.Main Menu. ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nEnter your choice = ");
                    try
                    {
                        choice = Convert.ToInt32(Console.ReadLine());
                        var adminBL = new Admin_BL();
                        switch (choice)
                        {
                            case 1: //update student
                                try
                                {
                                    Console.Write("\nEnter the First Name: ");
                                    String fn = Console.ReadLine(); //First Name Input
                                    testException.ValidateName(fn);
                                    Console.Write("Enter the Last Name: ");
                                    String ln = Console.ReadLine(); //Last Name Input
                                    testException.ValidateName(ln); // validating last name
                                    Console.Write("Enter the Date of Birth(DD/MM/YYYY): ");
                                    DateTime dob = Convert.ToDateTime(Console.ReadLine()); //DOB Input
                                    testException.ValidateStudentDOB(dob);
                                    Console.Write("Enter the Password: ");
                                    String pass = GetPassword(); //password
                                    testException.ValidatePassword(pass);

                                    if (adminBL.AddStudents(fn, ln, dob, pass))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("\n\tAdded Successfully");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\t\tPlease try again");
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
                                catch (InvalidDOBException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                catch (InvalidPasswordException e)
                                {
                                    Console.WriteLine(e.Message);
                                    
                                }

                                catch (Exception e)
                                {
                                    Console.WriteLine("\n\n\tPlease ensure you entered correct details and try again");
                                   
                                }
                                Console.WriteLine("\nPress any key to continue\n");
                                Console.ReadKey();
                                break;

                            case 2: //delete student
                                try
                                {
                                    Console.WriteLine("\n\tDelete Student");
                                    Console.Write("\nEnter the Student ID=");
                                    int studdelID = Convert.ToInt32(Console.ReadLine());

                                    if (adminBL.DelStudByID(studdelID))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("\n\tDeleted Successfully");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\n\tThis Student ID does not exist. Please Enter Valid ID.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\tEnter valid ID");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("\nPress any key to continue\n");
                                Console.ReadKey();
                                break;

                            case 3: //search student
                                try
                                {
                                    Console.WriteLine("\nSearch Student");
                                    Console.Write("Enter the Student ID : ");
                                    int studID = Convert.ToInt32(Console.ReadLine());
                                    Student student = adminBL.SearchstudbyID(studID);
                                    if (student != null)
                                    {
                                        var tableStudent = new ConsoleTable("StudentID", "First Name", "Last Name", "Address", "DOB", "City", "MobileNo");
                                        tableStudent.AddRow(student.StudentID, student.FName, student.LName, student.Address, student.DOB.ToString("dd/MM/yyyy"), student.City, student.MobileNo);
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        tableStudent.Write(Format.Alternative);
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nThis Student ID does not exist. Please Enter Valid ID.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\tEnter valid ID");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("\nPress any key to continue\n");
                                Console.ReadKey();
                                break;

                            case 4://All students Details
                                try
                                {
                                    Console.WriteLine("\nAll students Details");
                                   
                                    
                                    List<Student> studentList = adminBL.ViewAllStuds();
                                    

                                    if (studentList != null)
                                    {
                                        Console.WriteLine($"\nThere are currently {studentList.Count} students.");
                                        var table = new ConsoleTable("StudentID", "First Name", "Last Name", "Address", "DOB", "MobileNo");
                                        foreach (var item in studentList)
                                        {
                                            table.AddRow(item.StudentID, item.FName, item.LName, item.Address, item.DOB.ToString("dd/MM/yyyy"), item.MobileNo);
                                        }
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        table.Write(Format.Alternative);
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\tNo student data found");
                                        Console.ForegroundColor = ConsoleColor.White;

                                    }
                                }
                                catch(Exception e)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\tNo student data found");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("\nPress any key to continue\n");
                                Console.ReadKey();
                                break;

                            case 5:
                                Console.WriteLine("Redirecting to Main Menu................\n");
                                Thread.Sleep(1000);
                                Close();
                                MainMenu();
                                break;

                            default:
                                Console.WriteLine("\n Please enter valid value between 1 to 5  ");
                                break;
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t Please enter valid value between 1 to 5  ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        choice = 0;
                    }
                } while (choice != 5) ;
                
            }
            /// <summary>
            /// Definition : Method to show Selection of Teacher option and various Operations Such as add, delete, search, view all and main menu
            /// Name : Mridul and Prathamesh
            /// </summary>
            void teacheropt()
            {
                int choice;
                do
                {
                    Close();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string title = "========================================================================================================================";
                    Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
                    Console.WriteLine(title); string str = "TEACHER MANAGEMENT";
                    Console.SetCursorPosition((Console.WindowWidth - str.Length) / 2, Console.CursorTop);
                    Console.WriteLine(str);
                    title = "========================================================================================================================";
                    Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
                    Console.WriteLine(title); Console.WriteLine("Choose an appropriate value:-");
                    Console.WriteLine();
                    Console.WriteLine("\t1.Add Teacher.");
                    Console.WriteLine("\t2.Delete Teacher. ");
                    Console.WriteLine("\t3.Search Teacher. ");
                    Console.WriteLine("\t4.View All Teachers. ");
                    Console.WriteLine("\t5.Make Teacher an Admin. ");
                    Console.WriteLine("\t6.Assign course to teachers. ");
                    Console.WriteLine("\t7.Main Menu ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nEnter your choice: ");
                    try
                    {
                        choice = Convert.ToInt32(Console.ReadLine());
                        var adminBL = new Admin_BL();
                        switch (choice)
                        {
                            case 1:
                                try
                                {
                                    Console.WriteLine("\nAdd Teacher Details");
                                    Console.Write("\nEnter the First Name: ");
                                    String fn = Console.ReadLine(); //First Name Input

                                    testException.ValidateName(fn);

                                    Console.Write("Enter the Last Name: ");
                                    String ln = Console.ReadLine(); //Last Name Input

                                    testException.ValidateName(fn);

                                    Console.Write("Enter the Date of Birth(DD/MM/YYYY): ");
                                    DateTime dob = Convert.ToDateTime(Console.ReadLine()); //DOB Input
                                    testException.ValidateTeacherDOB(dob);

                                    Console.Write("Enter the Password: ");
                                    String pass = GetPassword(); //Last Name Input
                                    testException.ValidatePassword(pass);
                                    if (adminBL.AddTeachers(fn, ln, dob, pass))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("\n\tAdded Successfully");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\tPlease ensure you entered correct details and try again!");
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
                                catch (InvalidPasswordException e)
                                {
                                    Console.WriteLine(e.Message);
                                    

                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("\nPlease ensure you entered correct details and try again");
                                    
                                }
                                Console.WriteLine("\nPress any key to continue");
                                Console.ReadKey();
                                break;

                            case 2: //Delete Teacher
                                try
                                {
                                    Console.WriteLine("Delete Teacher");
                                    Console.Write("Enter the Teacher ID=");
                                    int teachdelID = Convert.ToInt32(Console.ReadLine());
                                    if (teachdelID == admin.adminID)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("You cannot delete yourself");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine("Please enter any key to continue");
                                        Console.ReadKey();
                                    }
                                    if (adminBL.DelTeacher(teachdelID))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("\n\tDelete Successfully");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\n\tPlease valid teacher ID and try again");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\tPlease valid teacher ID and try again!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("\nPress any key to continue\n");
                                Console.ReadKey();
                                break;

                            case 3: //search teacher
                                try
                                {
                                    Console.WriteLine("\nSearch Teacher");
                                    Console.Write("Enter the Teacher ID: ");
                                    int teachID = Convert.ToInt32(Console.ReadLine());
                                    Teacher teacher = adminBL.SearchTeacher(teachID);
                                    if (teacher != null)
                                    {
                                        var tableTeacher = new ConsoleTable("TeacherID", "First Name", "Last Name", "Address", "DOB", "City", "MobileNo");
                                        tableTeacher.AddRow(teacher.TeacherID, teacher.FName, teacher.LName, teacher.Address, teacher.DOB.ToString("dd/MM/yyyy"), teacher.City, teacher.MobileNo);
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        tableTeacher.Write(Format.Alternative);
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\tPlease valid teacher ID and try again");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\t Enter valid TeacherID");
                                    Console.WriteLine(e.Message);
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("\nPress any key to continue");
                                Console.ReadKey();
                                break;

                            case 4://All teacher details
                                Console.WriteLine("\nAll Teacher Details\n");
                                List<Teacher> teacherList = adminBL.ViewAllTchrs();
                                if (teacherList != null)
                                {
                                    var table = new ConsoleTable("TeacherID", "First Name", "Last Name", "Address", "DOB", "MobileNo", "Admin Status");
                                    foreach (var item in teacherList)
                                    {
                                        string s;
                                        if (item.IsAdmin == 1) s = "Admin";
                                        else s = "Not Admin";
                                        table.AddRow(item.TeacherID, item.FName, item.LName, item.Address, item.DOB.ToString("dd/MM/yyyy"), item.MobileNo,s);
                                    }
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    table.Write(Format.Alternative);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\tNo teacher added");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("\n Press any key to continue");
                                Console.ReadKey();
                                break;
                            case 5: // add as admin
                                try
                                {
                                    Console.WriteLine("\nAll Teacher Details\n");
                                    List<Teacher> teacherList1 = adminBL.ViewAllTchrs();
                                    if (teacherList1 != null)
                                    {
                                        var table = new ConsoleTable("TeacherID", "First Name", "Last Name","Admin status");
                                        foreach (var item in teacherList1)
                                        {
                                            string s;
                                            if (item.IsAdmin == 1) s = "Admin";
                                            else s = "Not Admin";
                                            table.AddRow(item.TeacherID, item.FName, item.LName,s);
                                        }
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        table.Write(Format.Alternative);
                                        Console.Write("\nEnter Teacher Id u want to promote to admin: ");
                                        int teacherID = Convert.ToInt32(Console.ReadLine());
                                        if (adminBL.AddAdmin(teacherID))
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\tAdded Successfully");
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\nPlease enter valid teacher ID and try again!");
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\t\tNo teacher added");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                catch
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nPlease enter valid teacher ID and try again!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("\n\tPress any key to continue");
                                Console.ReadKey();
                                break;
                            case 6:
                                try
                                {
                                    Console.WriteLine("\nAll Teacher Details\n");
                                    List<Teacher> teacherList1 = adminBL.ViewAllTchrs();
                                    if (teacherList1 != null)
                                    {
                                        var table = new ConsoleTable("TeacherID", "First Name", "Last Name", "Address", "DOB", "MobileNo", "Admin status");
                                        foreach (var item in teacherList1)
                                        {
                                            string s;
                                            if (item.IsAdmin == 1) s = "Admin";
                                            else s = "Not Admin";
                                            table.AddRow(item.TeacherID, item.FName, item.LName, item.Address, item.DOB, item.MobileNo, s);
                                        }
                                        table.Write(Format.Alternative);
                                        List<Course> courseList = adminBL.ViewAllCourses();
                                        if (courseList != null)
                                        {
                                            var tableCourse = new ConsoleTable("CourseID", "CourseName", "Start Date", "End Date");
                                            foreach (var item in courseList)
                                            {
                                                tableCourse.AddRow(item.CourseID, item.CourseName, item.StartDate, item.EndDate);
                                            }
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("\nAll Courses Details\n");
                                            tableCourse.Write(Format.Alternative);
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write("Enter Teacher Id: ");
                                            int teacherID = Convert.ToInt32( Console.ReadLine());
                                            Console.Write("Enter Course Id: ");
                                            int courseID = Convert.ToInt32(Console.ReadLine());
                                            if (adminBL.AssignCourse(teacherID, courseID))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Course assigned to teacher");
                                                Console.ForegroundColor = ConsoleColor.White;
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Please try again.\nSorry for your inconvenience");
                                                Console.ForegroundColor = ConsoleColor.White;
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\nNo course added");
                                            Console.ForegroundColor = ConsoleColor.White;

                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\t\tNo teacher added");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                catch(SqlException)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nIDs were invalid or teacher already teaches that course");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                catch
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nPlease enter valid IDs and try again!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("\n\tPress any key to continue");
                                Console.ReadKey();

                                break;
                            case 7:
                                Console.WriteLine("Redirecting to Main Menu................\n");
                                Thread.Sleep(1000);
                                Close();
                                MainMenu();
                                break;

                            default:
                                Console.WriteLine("\n Please enter valid value between 1 to 5  ");
                                Console.WriteLine("Please enter any to continue");
                                Console.ReadKey();
                                break;
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t\t Please enter valid value between 1 to 5  ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        choice = 0;
                    }
                } while(choice!=7);

            }
            /// <summary>
            /// Definition : Method to show Selection of Course option and various Operations Such as add, delete, search, view all and Exit
            /// </summary>
            void coursesopt()
            {
                int choice;
                do
                {

                    Close();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string title = "========================================================================================================================";
                    Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
                    Console.WriteLine(title); string str = "COURSE MANAGMENT";
                    Console.SetCursorPosition((Console.WindowWidth - str.Length) / 2, Console.CursorTop);
                    Console.WriteLine(str);
                    title = "========================================================================================================================";
                    Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
                    Console.WriteLine(title); Console.WriteLine("Choose an appropriate value:-");
                    Console.WriteLine();
                    Console.WriteLine("\t1.Add Courses. ");
                    Console.WriteLine("\t2.Delete Courses. ");
                    Console.WriteLine("\t3.Search Courses. ");
                    Console.WriteLine("\t4.View All Courses. ");
                    Console.WriteLine("\t5.Main Menu.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nEnter your choice: ");
                    try
                    {
                        choice = Convert.ToInt32(Console.ReadLine());
                        var adminBL = new Admin_BL();
                        switch (choice)
                        {
                            case 1: // add course
                                try
                                {
                                    Console.WriteLine("\nAdd Course Details");
                                    Console.Write("\nEnter the Course Name: ");
                                    String cn = Console.ReadLine(); //Course Name Input
                                    Console.Write("Enter the Course Start Date (DD/MM/YYYY): ");
                                    DateTime strtdate = Convert.ToDateTime(Console.ReadLine()); //Course Start date
                                    testException.ValidateCourseDate(strtdate);
                                    Console.Write("Enter the Course End Date: (DD/MM/YYYY): ");
                                    DateTime enddate = Convert.ToDateTime(Console.ReadLine());//Course End Date input
                                    testException.ValidateCourseDate(strtdate);
                                    if (adminBL.AddCourses(cn, strtdate, enddate))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("\nAdded Successfully");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\tPlease Enter Valid Course Details");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                catch (InvalidDOBException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Please Enter Valid Course Details");
                                }
                                Console.WriteLine("\n\tPress any key to continue");
                                Console.ReadKey();
                                break;

                            case 2: // course
                                try
                                {
                                    Console.WriteLine("\nDelete Course");
                                    Console.Write("Enter the Course ID: ");
                                    int coursedelID = Convert.ToInt32(Console.ReadLine());

                                    if (adminBL.DelCourses(coursedelID))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("\nDeleted Successfully");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nPlease enter valid Course ID");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n Please enter valid Course ID");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("\nPress any key to continue");
                                Console.ReadKey();
                                break;

                            case 3: // seacher course
                                try
                                {
                                    Console.WriteLine("\nSearch Course");
                                    Console.Write("Enter the Course ID: ");
                                    int courseID = Convert.ToInt32(Console.ReadLine());
                                    Course course = adminBL.SearchCourses(courseID);
                                    if (course != null)
                                    {
                                        var courseTable = new ConsoleTable("CourseID", "CourseName", "Start Date", "End Date");
                                        courseTable.AddRow(course.CourseID, course.CourseName, course.StartDate, course.EndDate);
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        courseTable.Write(Format.Alternative);
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nCourse not found");
                                        Console.ForegroundColor = ConsoleColor.White;

                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n Please Enter valid Course ID");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine("\n Press any key to continue");
                                Console.ReadKey();
                                break;

                            case 4: // view all courses
                                Console.WriteLine("\nAll Course Details");
                                List<Course> courseList = adminBL.ViewAllCourses();
                                if (courseList != null)
                                {
                                    var table = new ConsoleTable("CourseID", "CourseName", "Start Date", "End Date");
                                    foreach (var item in courseList)
                                    {
                                        table.AddRow(item.CourseID, item.CourseName, item.StartDate, item.EndDate);
                                    }
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    table.Write(Format.Alternative);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nNo course added");
                                    Console.ForegroundColor = ConsoleColor.White;

                                }

                                Console.WriteLine("\nPress any key to continue\n");
                                Console.ReadKey();
                                break;

                            case 5:
                                Console.WriteLine("Redirecting to Main Menu................\n");
                                Thread.Sleep(1000);
                                Close();
                                MainMenu();
                                break;

                            default:
                                Console.WriteLine("\n Please enter valid value between 1 to 5  ");
                                break;
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n Please enter valid value between 1 to 5  ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        choice = 0;
                    }
                } while (choice != 5);
            }
            /// <summary>
            /// Definition : Method to switch to teacher mode and calling teacher login page
            /// Name : Mridul and Prathamesh
            /// </summary>
            void switchopt()
            {
                //TEACHER - ui
                Teacher_UI.TeacherMain();
                Close();
                MainMenu();
                
            }
            /// <summary>
            /// Definition : Method to sign off and exit
            /// Name : Mridul and Prathamesh
            /// </summary>
            void exitopt()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n Signing off...............");         
                string str = "THANK YOU ADMIN";
                Thread.Sleep(1000);
                Console.SetCursorPosition((Console.WindowWidth - str.Length) / 2, Console.CursorTop);
                Console.WriteLine(str);
            }
        }
    }
}
