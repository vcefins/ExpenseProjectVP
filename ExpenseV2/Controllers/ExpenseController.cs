using ExpenseV2.Models;
using ExpenseV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Expense.Engine.Handlers;
using Expense.Engine.Response;

namespace ExpenseV2.Controllers
{
    public class ExpenseController : Controller
    {
        // GET: Expense
        public ActionResult Index() //Home Page
        {
            if (Session["ID"] != null)
            {
                var viewModel = new ExpenseListViewModel();

                //var request = new Expense.Engine.Request.GetExpensesRequest();        //Angular will do the post

                //request.UserID = Convert.ToInt32(Session["ID"]);
                //request.RoleID = Convert.ToInt32(Session["RoleID"]);

                //viewModel.ExpenseList = Expense.Engine.Handlers.ExpenseHandler.GetAllExpenses(request).ExpenseList;

                return View(viewModel);
            }

            return RedirectToAction("Index", "Login");  //If Session is empty, throw back to login
        }


        public ActionResult Create(int ID)  //if ID == 0 --> new form | if ID != 0 --> details for an existing form
        {
            if (Session["ID"] != null)
            {
                if (ID == 0)    //New Expense
                {
                    var model = new ExpenseIdModel();

                    model.ExpenseID = 0;

                    return View(model);
                }
                else           //Existing Expense
                {
                    var model = new ExpenseIdModel();

                    model.ExpenseID = ID;

                    return View(model);
                }
            }

            return RedirectToAction("Index", "Login");  //If Session is empty, throw back to login
        }
    }
}