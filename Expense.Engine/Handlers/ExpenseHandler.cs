using Expense.Data;
using Expense.Engine.Domain;
using Expense.Engine.Request;
using Expense.Engine.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Engine.Handlers
{
    public class ExpenseHandler
    {
        public static ExpenseResponse GetAllExpenses(GetExpensesRequest request)    //Get all Expense Information regarding that User
        {
            ExpenseResponse response = new ExpenseResponse();
            using (var entities = new ExpenseProjectDBEntities())
            {
                response.ExpenseList = (from e in entities.Expenses
                                        join u in entities.Users on e.Created_User equals u.ID
                                        join s in entities.ExpenseStatuses on e.StatusId equals s.ID
                                        where ((e.Created_User == request.UserID && request.RoleID == 1)
                                        || (e.StatusId == 2 && request.RoleID == 2)
                                        || (e.StatusId == 3 && request.RoleID == 3))
                                        select new ExpenseDomain
                                        {
                                            ID = e.ID,
                                            TotalAmount = e.TotalAmount,
                                            CreatedDate = e.CreatedDate,
                                            CreatedUserID = e.Created_User,
                                            CreatedUserName = u.Name,
                                            CreatedUserSurname = u.Surname,
                                            StatusID = e.StatusId,
                                            StatusName = s.StatusDescription,
                                            ModifiedByID = e.ModifiedBy,
                                            ModifiedDate = e.ModifiedDate,
                                            RejectDescription = e.RejectDescription
                                        }
                        ).ToList();

                List<ExpenseDomain> expenses = new List<ExpenseDomain>();

                return response;
            }
        }

        public static List<ExpenseItemDomain> GetExpenseItems(ExpenseDetailsRequest request)   //Get all Expense Items of one Expense
        {

            int ExpenseID = request.ExpenseID;

            List<ExpenseItemDomain> response = new List<ExpenseItemDomain>();
            using (var entities = new ExpenseProjectDBEntities())
            {
                response = (from e in entities.ExpenseItems
                            where (e.ExpenseId == ExpenseID)
                            select new ExpenseItemDomain
                            {
                                ExpenseItemID = e.ID,
                                ExpenseItemDescription = e.ExpenseDescription,
                                ExpenseItemAmount = e.ExpenseAmount,
                                ExpenseItemDate = e.ExpenseDate
                            }
                        ).ToList();

                return response;
            }
        }

        public static ExpenseDetailsResponse GetExpenseDetails(ExpenseDetailsRequest request)   //Get Details for a specific Expense
        {
            int ExpenseID = request.ExpenseID;

            var response = new ExpenseDetailsResponse();
            var expense = new ExpenseDomain();

            using (var entities = new ExpenseProjectDBEntities())
            {
                expense = (from e in entities.Expenses
                           where (e.ID == ExpenseID)
                           select new ExpenseDomain
                           {
                               ID = e.ID,
                               TotalAmount = e.TotalAmount,
                               CreatedDate = e.CreatedDate,
                               CreatedUserID = e.Created_User,
                               StatusID = e.StatusId,
                               ModifiedByID = e.ModifiedBy,
                               ModifiedDate = e.ModifiedDate,
                               RejectDescription = e.RejectDescription
                           }
                        ).FirstOrDefault();

                response.ExpenseInformation = expense;
                response.ExpenseItemList = GetExpenseItems(request);

                return response;
            }

        }

        public static SaveExpenseResponse SubmitExpenseToDatabase(SaveExpenseRequest request)   //Submit New Expense
        {
            var response = new SaveExpenseResponse();

            var expenseItemList = request.ExpenseItemList;

            using (var entities = new ExpenseProjectDBEntities())
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                //{

                var currentExpenseID = request.ExpenseID;

                Expense.Data.Expense expense = new Data.Expense();
                if (currentExpenseID == 0)  //If Expense doesn't exist, creates a new one and pushes the ID to currentExpenseID
                {

                    expense = new Expense.Data.Expense
                    {
                        TotalAmount = expenseItemList.Sum(x => x.ExpenseItemAmount),        //Holy
                        CreatedDate = DateTime.Now,
                        Created_User = 1,              //Session? Angular?
                        StatusId = 2
                    };

                    entities.Expenses.Add(expense);     //Creates a new expense in db
                }


                Expense.Data.ExpenseItem expenseItem;

                if (entities.SaveChanges() > 0 || expenseItemList.Count > 0)     //???
                {
                    

                    foreach (var item in expenseItemList)
                    {
                        expenseItem = new ExpenseItem();
                        expenseItem.ExpenseAmount = item.ExpenseItemAmount;
                        expenseItem.ExpenseDate = item.ExpenseItemDate;
                        expenseItem.ExpenseDescription = item.ExpenseItemDescription;
                        expenseItem.ExpenseId = (currentExpenseID == 0) ? expense.ID : currentExpenseID;     //This is where you tie the item to the expense

                        if (item.ExpenseItemID == 0)
                        {
                            entities.ExpenseItems.Add(expenseItem);
                        }
                        else
                        {
                            var expenseItemDb = entities.ExpenseItems.Where(e => e.ID == item.ExpenseItemID).First();
                            expenseItemDb.ExpenseDescription = expenseItem.ExpenseDescription;
                            expenseItemDb.ExpenseDate = expenseItem.ExpenseDate;
                            expenseItemDb.ExpenseAmount = expenseItem.ExpenseAmount;
                        }
                    }
                    currentExpenseID = (currentExpenseID == 0) ? expense.ID : currentExpenseID;
                    var expenseItemListDB = entities.ExpenseItems.Where(i => i.ExpenseId == currentExpenseID).ToList();
                    var notMatchedExpenseItems = expenseItemListDB.Where(e => !expenseItemList.Select(i => i.ExpenseItemID).Contains(e.ID)).ToList();

                    foreach (var item in notMatchedExpenseItems)
                    {
                        entities.ExpenseItems.Remove(item);
                    }
                    entities.SaveChanges();

                }

                else
                {
                    response.IsSuccess = false;
                    response.Message = "There was a problem.";
                }

                SetStatusToWaitingForManager(currentExpenseID);
                entities.SaveChanges();

                //scope.Commit();
            }

            response.IsSuccess = true;
            response.Message = "Your expense has been successfully created.";

            return response;
        }

        public static void SetStatusToWaitingForManager(int ID)
        {
            var expense = new ExpenseDomain();

            using (var entities = new ExpenseProjectDBEntities())
            {
                expense = (from e in entities.Expenses
                           where (e.ID == ID)
                           select new ExpenseDomain
                           {
                               StatusID = 4,
                           }
                        ).FirstOrDefault();

                entities.SaveChanges();
            }
        }

        public static SaveExpenseResponse ManageExpenses(ManageExpenseRequest request)  //Evaluate an Expense
        {

            using (var entities = new ExpenseProjectDBEntities())
            {
                try     //Correct?
                {
                    var expense = entities.Expenses.SingleOrDefault(e => e.ID == request.ExpenseID);
                    if (request.Approved)
                    {
                        if (expense.StatusId == 2)
                        {
                            expense.StatusId = 3;   //Status of the expense updates from waiting mng approval to waiting acc approval
                        }
                        else if (expense.StatusId == 3)
                        {
                            expense.StatusId = 5;   //What to do with Approved Expenses?
                        }
                    }
                    else
                    {
                        expense.StatusId = 4;
                        expense.RejectDescription = request.RejectDescription;
                    }

                    entities.SaveChanges();

                    var response = new SaveExpenseResponse();

                    response.IsSuccess = true;
                    response.Message = "The expense's status has been successfully updated.";

                    return response;
                }
                catch (NullReferenceException)
                {
                    var response = new SaveExpenseResponse();

                    response.IsSuccess = false;
                    response.Message = "The expense you're trying to evaluate doesn't exist.";

                    return response;
                }
            }
        }
    }
}
