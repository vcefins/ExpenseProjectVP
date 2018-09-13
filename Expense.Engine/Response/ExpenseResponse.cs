using Expense.Engine.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Engine.Response
{
    public class ExpenseResponse    //Listof General Information on Expenses (for Default Page)
    {
        public List<ExpenseDomain> ExpenseList { get; set; }
    }
}
