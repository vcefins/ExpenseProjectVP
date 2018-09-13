using ExpenseV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Expense.Engine.Handlers;

namespace ExpenseV2.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {

            Session.Abandon();

            return View();
        }


        [HttpPost]
        public ActionResult Index(UserModel model)      //model contains the username and password entered by the current user.
        {
            if (ModelState.IsValid)
            {
                var handler = new LoginHandler();
                var currentUser = handler.GetUserInformation(model.Username, model.Password);

                if (currentUser != null)
                {
                    Session["ID"] = currentUser.ID.ToString();
                    Session["UserName"] = currentUser.Username.ToString();
                    Session["Name"] = currentUser.Name.ToString();
                    Session["Surname"] = currentUser.Surname.ToString();
                    Session["RoleID"] = currentUser.RoleID;
                    return RedirectToAction("Index", "Expense");
                }
                else model.Message = "Username Or Password Invalid";

            }
            return View(model);
        }

        public ActionResult LogOut()
        {

            Session.Abandon();


            return RedirectToAction("Index", "Login");
        }
    }
}