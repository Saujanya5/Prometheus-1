using System;
using System.Collections.Generic;
using System.Text;

namespace Prometheus_Entities
{
    /// <summary>
    /// Written by Navpreet
    /// </summary>
    public class HomeWork
    {
        //HomeWorkID numeric(5), Description varchar(20), Deadline date, ReqTime   int, LongDescription varchar(50)
        public int HomeWorkID{ get; set; }
        public string Description{ get; set; }
        public DateTime Deadline{ get; set; }
        public int ReqTime{ get; set; }
        public string LongDescription{ get; set; }
        public string CourseName { get; set; }
    }
}
