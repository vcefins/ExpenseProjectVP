using Expense.Engine.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseV2.ViewModels
{
    public class ExpenseListViewModel : ViewModel  //Holds Expenses connected to the user 
    {
        public List<ExpenseDomain> ExpenseList { get; set; }
        

    }
}