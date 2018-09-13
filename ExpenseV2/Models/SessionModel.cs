using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseV2.Models
{
    public class SessionModel   //Session Model class that is embedded in View Model super class
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
    }
}