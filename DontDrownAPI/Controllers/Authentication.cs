using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DontDrownAPI.Controllers
{
    public class Authentication
    {
        Dictionary<string, int> userCookies = new Dictionary<string, int>();
        private SqlConnection sqlCon = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DontDrown;Integrated Security=True;MultipleActiveResultSets=True");

        public int Login(string username, string password)
        {
            bool correctLogin = SqlExecuter.Login(sqlCon, username, password);

            if (correctLogin)
            {
                if (!userCookies.ContainsKey(username))
                {
                    int authCookie = new Random().Next(10000, 99999);
                    userCookies.Add(username, authCookie);

                    return authCookie;
                }
                else
                {
                    return userCookies[username];
                }
            }
            return -1;
        }

        public bool Logout(string username)
        {
            userCookies.Remove(username);

            try
            {
                var u = userCookies[username];
            }
            catch
            {
                return true;
            }
            return false;
        }
    }
}
