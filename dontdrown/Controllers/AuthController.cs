using DontDrownAPI.Models;
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
        
        // GET api/<controller>/{username}/{password}
        [Route("api/auth/login/{username}/{password}")]
        public IHttpActionResult Get(string username, string password)
        {
            Account user = SqlExecuter.Login(username, password);

            if (user != null)
            {
                if (!StaticValues.userCookies.ContainsKey(username))
                {
                    int authCookie = new Random().Next(10000, 99999);
                    StaticValues.userCookies.Add(username, authCookie);
                }
                return Ok(user);
            }
            return NotFound();
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