using Expense.Engine.Domain;
using Expense.Engine.Handlers;
using Expense.Engine.Request;
using Expense.Engine.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseV2.Api
{
    public class ExpenseController : ApiController
    {
        // GET api/ExpenseController/GetExpensesByUserId
        [HttpPost]
        public ExpenseResponse GetExpensesByUserID(GetExpensesRequest request)
        {
            return ExpenseHandler.GetAllExpenses(request);
        }

        [HttpPost]
        public SaveExpenseResponse SaveExpense(SaveExpenseRequest request)
        {
            return ExpenseHandler.SubmitExpenseToDatabase(request);
        }

        [HttpPost]
        public IHttpActionResult GetExpensesByExpenseID(ExpenseDetailsRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = ExpenseHandler.GetExpenseDetails(request);

            return Ok(response);
        }

        [HttpPost]
        public SaveExpenseResponse EvaluateExpense(ManageExpenseRequest request)
        {
            return ExpenseHandler.ManageExpenses(request);
        }

    }
}