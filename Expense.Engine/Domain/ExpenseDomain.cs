using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Engine.Domain
{
    public class ExpenseDomain  //General Information on Expense
    {
        public int ID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserID { get; set; }
        public int StatusID { get; set; }
        public int? ModifiedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string RejectDescription { get; set; }
        public string CreatedUserName { get; set; }
        public string CreatedUserSurname { get; set; }
        public string StatusName { get; set; }
    }
}
