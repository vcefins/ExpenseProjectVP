using Expense.Engine.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Engine.Response
{
    public class ExpenseDetailsResponse    //To contain a full set of details regarding a single Expense
    {
        public ExpenseDomain ExpenseInformation { get; set; } 
        public List<ExpenseItemDomain> ExpenseItemList { get; set; }
    }
}
