using Expense.Engine.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseV2.Models
{
    public class ExpenseModel   //Carries a fully detailed Expense
    {
        public ExpenseDetailsResponse ExpenseDetails { get; set; }
    }
}