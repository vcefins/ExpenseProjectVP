using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Engine.Domain
{
    public class ExpenseItemDomain    //To hold a single Expense Item information
    {
        public int ExpenseItemID { get; set; }
        public string ExpenseItemDescription { get; set; }
        public decimal ExpenseItemAmount { get; set; }
        public DateTime ExpenseItemDate { get; set; }

    }
}
