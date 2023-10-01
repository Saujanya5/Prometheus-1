using System;
using System.Collections.Generic;
using System.Text;

namespace Project_sprint1
{
    class Course
    {
        //CourseID numeric(5), CourseName varchar(20), StartDate date, EndDate date
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
