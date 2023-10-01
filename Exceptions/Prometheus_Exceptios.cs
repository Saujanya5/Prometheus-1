using System;
using System.Text.RegularExpressions;

namespace Prometheus_Exceptions
{
    /// <summary>
    /// Prometheus Exception class derived from basic exception class
    /// Written by Amaan
    /// </summary>
    public class PrometheusException : Exception
    {
        public PrometheusException(String message) : base(message)
        {

        }
    }

    /// <summary>
    /// Exception thrown for invalid ID entered
    /// Written by Amaan
    /// </summary>
    public class InvalidIdException : Exception
    {
        public InvalidIdException(String message) : base(message)
        {

        }
    }

    /// <summary>
    /// Exception thrown for invalid name entered
    /// Written by Amaan
    /// </summary>
    public class InvalidNameException : Exception
    {
        public InvalidNameException(String message) : base(message)
        {

        }
    }

    /// <summary>
    /// Exception thrown for invalid mobile number entered
    /// Written by Amaan
    /// </summary>
    public class InvalidMobileNoException : Exception
    {
        public InvalidMobileNoException(String message) : base(message)
        {

        }
    }

    /// <summary>
    /// Exception thrown for invalid password entered
    /// Written by Amaan
    /// </summary>
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(String message) : base(message)
        {

        }
    }

    /// <summary>
    /// Exception thrown for invalid date entered
    /// Written by Amaan
    /// </summary>
    public class InvalidDOBException : Exception
    {
        public InvalidDOBException(String message) : base(message)
        {

        }
    }

    /// <summary>
    /// Class for all the validating fields
    /// </summary>
    public class TestException
    {
        /// <summary>
        /// Method to validate ID
        /// </summary>
        /// <param name="id"></param>
        public void ValidateId(int id)
        {
            if (id.ToString().Length != 4 || id.ToString().Length != 5)
            {
                throw new InvalidIdException("Sorry! Student ID must be 4 or 5 digits only!");
            }
        }

        /// <summary>
        /// Method to validate Name
        /// </summary>
        /// <param name="name"></param>
        public void ValidateName(string name)
        {
            var regexItem = new Regex("^[a-zA-Z]*$");
            if (String.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                throw new InvalidNameException("Sorry! Name cannot be blank! Please Try Again!\n");
            }
            else if (!regexItem.IsMatch(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                throw new InvalidNameException("Sorry! Name can have only letters! Please Try Again!\n");
            }
        }

        /// <summary>
        /// Validate moblie number
        /// </summary>
        /// <param name="number"></param>
        public void ValidateMobileNo(long number)
        {
            if (number > 5999999999 && number < 9999999999)
            {

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                throw new InvalidMobileNoException("Sorry! Mobile No must be 10 digits and should only start with 6/7/8/9!\n");
            }
        }

        /// <summary>
        /// Validate password
        /// </summary>
        /// <param name="pass"></param>
        public void ValidatePassword(string pass)
        {
            if (pass.Length <=20 && pass.Length>=4)
            {

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                throw new InvalidPasswordException("Please enter a valid password\n");
            }
        }

        /// <summary>
        /// Validate teacher's DOB
        /// </summary>
        /// <param name="dob"></param>
        public void ValidateTeacherDOB(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;

            if (dob > DateTime.Today)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                throw new InvalidDOBException("Please enter valid date! Date entered is in Future!\n");
            }

            if (age < 25)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                throw new InvalidDOBException("Teacher's age must be 25 years or more!");
            }
        }

        /// <summary>
        /// Validate Student's DOB
        /// </summary>
        /// <param name="dob"></param>
        public void ValidateStudentDOB(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;

            if (dob > DateTime.Today)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                throw new InvalidDOBException("Please enter valid date! Date entered is in Future!\n");
            }

            if (age < 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                throw new InvalidDOBException("Student's age must be 4 years or more!");
            }
        }

        /// <summary>
        /// Validate courses start and end date
        /// </summary>
        /// <param name="date"></param>
        public void ValidateCourseDate(DateTime date)
        {
            if (date < DateTime.Today)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                throw new InvalidDOBException("Please enter a valid date. You cannot add course for past dates\n");
            }
        }

    }
}