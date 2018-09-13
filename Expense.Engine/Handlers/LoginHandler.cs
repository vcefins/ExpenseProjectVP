using Expense.Data;
using Expense.Engine.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Engine.Handlers
{
    public class LoginHandler
    {
        public UserDomain GetUserInformation(string username, string password)   //Somehow, send the info here from View/ngJS
        {
            UserDomain response = new UserDomain();
            using (var entities = new ExpenseProjectDBEntities())
            {
                response = (from e in entities.Users.Where(e => e.Username == username && e.Password == password)
                            select new UserDomain
                            {
                                ID = e.ID,
                                Username = e.Username,
                                Password = e.Password,
                                Name = e.Name,
                                Surname = e.Surname,
                                Email = e.Email,
                                RoleID = e.RoleId
                            }
                        ).FirstOrDefault();
            }

            return response;
        }

        public UserDomain GetUserInformationByID(int UserID)   //Somehow, send the info here from View/ngJS
        {
            UserDomain response = new UserDomain();
            using (var entities = new Expense.Data.ExpenseProjectDBEntities())
            {
                response = (from e in entities.Users.Where(e => e.ID == UserID)
                            select new UserDomain
                            {
                                ID = e.ID,
                                Username = e.Username,
                                Password = e.Password,
                                Name = e.Name,
                                Surname = e.Surname,
                                Email = e.Email,
                                RoleID = e.RoleId
                            }
                        ).FirstOrDefault();
            }

            return response;
        }

    }
}
