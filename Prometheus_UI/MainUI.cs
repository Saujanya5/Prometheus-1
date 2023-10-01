using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Prometheus_UI
{
    /// <summary>
    /// Written by Navpreet, Prathamesh
    /// </summary>
    class MainUI
    {
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
            string title = "PROMETHEUS - HOMEWORK MANAGEMENT SYSTEM";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            title = "========================================================================================================================";
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
            string str = "WELCOME USER";
            Console.SetCursorPosition((Console.WindowWidth - str.Length) / 2, Console.CursorTop);
            Console.WriteLine(str);
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.White;

        }
        /// <summary>
        /// Menu to be printed
        /// </summary>
        static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("LOG IN AS");
            Console.WriteLine("==============================================");
            Console.WriteLine("\t1. Student ");
            Console.WriteLine("\t2. Teacher ");
            Console.WriteLine("\t3. Admin ");
            Console.WriteLine("\t4. Exit");
            Console.WriteLine("==============================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter your choice : ");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Main()
        {
            byte choice;
            Console.SetWindowSize(160, 40); // setting window size to fit all the tables
            do
            {
                Clear();
                Menu();
                try
                {
                    choice = Convert.ToByte(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Student_UI.StudentMain(); //switch to student module
                            break;
                        case 2:
                            Teacher_UI.TeacherMain(); //switch to teacher module
                            break;
                        case 3:
                            Admin_UI.AdminMain(); //switch to admin module
                            break;
                        case 4: //close the application
                            Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nSigning off...............");
                            
                            string str = "THANK YOU";

                            Thread.Sleep(1000);
                            Console.SetCursorPosition((Console.WindowWidth - str.Length) / 2, Console.CursorTop);
                            Console.WriteLine(str);
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter valid choice between 1 to 4");
                            Console.WriteLine("\nPress any key to continue\n");
                            Console.ReadKey();
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter valid choice between 1 to 4");
                    Console.WriteLine("\nPress any key to continue\n");
                    Console.ReadKey();
                    Console.ForegroundColor = ConsoleColor.White;
                    choice = 0;
                }
            } while (choice != 4);
        
        }
    }
}
