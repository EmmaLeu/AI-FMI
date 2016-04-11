using BE;
using DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new FMIContext();
            InsertUser(context, "ana2@test.ro", "123456", "FirstName2", "LastName2");
            Console.WriteLine(14);
        }


        private static bool InsertUser(FMIContext context, string email, string password, string fn, string ln)
        {
            try
            {
                var user = new User
                {
                    LoginEmail = email,
                    PasswordHash = password,
                    FirstName = fn,
                    LastName = ln,
                    Gender = true,
                    CreationDate = DateTime.Now,
                    IsDeleted = false
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

    }
}