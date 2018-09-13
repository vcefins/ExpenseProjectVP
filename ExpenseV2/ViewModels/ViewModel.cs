using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseV2.ViewModels
{
    public class ViewModel  //Superclass to embed Session Model to all View Models
    {
        public Models.SessionModel SessionModel { get; set; }

        public ViewModel()
        {
            var session = HttpContext.Current.Session;
            this.SessionModel = new Models.SessionModel()
            {
                ID = Convert.ToInt32(session["ID"]),
                RoleID = Convert.ToInt32(session["RoleID"])
            };
        }
    }
}