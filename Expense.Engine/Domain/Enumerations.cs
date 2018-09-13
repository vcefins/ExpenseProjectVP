using Expense.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Engine.Domain
{
    public class Enumerations
    {
        public enum RoleEnum
        {
            Employee = 1,
            Manager = 2,
            Accountant = 3
        }

        public enum StatusEnum  //Can't I pull this from dbo just once and use it from here? 
        {
            Created = 1,
            ManagerApproval = 2,
            AccountantApproval = 3,
            Rejected = 4,
            Approved = 5
        }
    }
}
