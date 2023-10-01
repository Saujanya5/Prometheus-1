using System;
using System.Collections.Generic;
using System.Text;

namespace Prometheus_Entities
{
    /// <summary>
    /// Written by Navpreet
    /// </summary>
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public string City { get; set; }
        public long MobileNo { get; set; }
        public byte IsAdmin { get; set; }
    }
}
