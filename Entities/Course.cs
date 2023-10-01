using System;
using System.Collections.Generic;
using System.Text;

namespace Prometheus_Entities
{
    /// <summary>
    /// Written by Navpreet
    /// </summary>
    public class Course
    {
        //CourseID numeric(5), CourseName varchar(20), StartDate date, EndDate date
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
