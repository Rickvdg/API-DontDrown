using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dontdrown.Controllers
{
    public class AuthController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        // GET api/<controller>/5
        public int Get(int id)
        {
            string username = "Rick";
            string password = "123";
            bool correctLogin = SqlExecuter.Login(username, password);
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

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}