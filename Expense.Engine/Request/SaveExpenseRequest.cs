using Expense.Engine.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Engine.Request
{
    public class SaveExpenseRequest
    {
        public List<ExpenseItemDomain> ExpenseItemList { get; set; }
        public int ExpenseID { get; set; }
    }
}
