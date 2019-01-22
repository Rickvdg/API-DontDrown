using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DontDrownAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class Authentication
    {
        private SqlConnection sqlCon = new SqlConnection(StaticValues.sqlConString);

        [HttpGet("login")]
        public int Login(string username, string password)
        {
            bool correctLogin = SqlExecuter.Login(sqlCon, username, password);
            Debug.WriteLine(StaticValues.userCookies.Count);

            if (correctLogin)
            {
                if (!StaticValues.userCookies.ContainsKey(username))
                {
                    int authCookie = new Random().Next(10000, 99999);
                    StaticValues.userCookies.Add(username, authCookie);

                    return authCookie;
                }
                else
                {
                    return StaticValues.userCookies[username];
                }
            }
            return -1;
        }

        [HttpGet("logout")]
        public bool Logout(string username)
        {
            StaticValues.userCookies.Remove(username);

            try
            {
                var u = StaticValues.userCookies[username];
            }
            catch
            {
                return true;
            }
            return false;
        }
    }
}
