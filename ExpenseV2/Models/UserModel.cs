using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseV2.Models
{
    public class UserModel  //Sends the entered Login info to Controller
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
    }
}