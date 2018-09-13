using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Engine.Request
{
    public class ManageExpenseRequest
    {
        public int ExpenseID { get; set; }
        public bool Approved { get; set; }
        public string RejectDescription { get; set; }
    }
}
