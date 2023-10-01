using System;
using System.Collections.Generic;
using System.Text;

namespace Project_sprint1
{
    class HomeWork
    {
        //HomeWorkID numeric(5), Description varchar(20), Deadline date, ReqTime   int, LongDescription varchar(50)
        public int HomeWorkID{ get; set; }
        public string Description{ get; set; }
        public DateTime Deadline{ get; set; }
        public int ReqTime{ get; set; }
        public string LongDescription{ get; set; }

    }
}
