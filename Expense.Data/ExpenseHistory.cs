//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Expense.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExpenseHistory
    {
        public int ID { get; set; }
        public int ExpenseId { get; set; }
        public int StatusId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual Expense Expense { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ExpenseStatus ExpenseStatus { get; set; }
    }
}
