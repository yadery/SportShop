using SportShop.@base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportShop
{
    public class TestValid
    {
        public static bool ValidateUser(string Login, string Password)
        {
            if (Login == null || Password == null)
                return false;
            else if (Password == "asd" && Login == "asd") return true;
            else return false;
        }

        public static bool ZayavkaTest(int idUser, int productArticle)
        {
            try
            {
                Request request = new Request();
                request.userID = idUser;
                request.productArticle = productArticle;

                AppData.db.Request.Add(request);
                AppData.db.SaveChanges();
                AppData.db.Request.Remove(request);
                AppData.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool getUser(string value)
        {
            try
            {
                var tovar = AppData.db.Users.FirstOrDefault(x => x.Login == value);
                if (tovar == null)
                    return false;
                else
                    return true;

            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public static bool addUser(string login, string password, int roleID)
        {
            try
            {
                Users usertest = new Users();
                usertest.Login = login;
                usertest.Password = password;
                usertest.RoleID = roleID;

                AppData.db.Users.Add(usertest);
                AppData.db.SaveChanges();
                AppData.db.Users.Remove(usertest);
                AppData.db.SaveChanges();
                return false;
            }
            catch
            {
                return true;
            }
        }

    }
}
