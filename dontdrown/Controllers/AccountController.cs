using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using dontdrown.Controllers;
using DontDrownAPI.Models;

namespace DontDrownAPI.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return SqlExecuter.GetAccounts(null);
        }

        [Route("api/account/{classname}")]
        public IEnumerable<Account> GetClassmates(string classname)
        {
            return SqlExecuter.GetAccounts(classname);
        }

        public IHttpActionResult Post([FromBody] Account account)
        {
            var value = SqlExecuter.InsertAccount(account.Username, account.Password, account.RolId, account.Classname);
            if (value)
            {
                return Ok();
            }
            return NotFound();
        } 
        
        [Route("api/account/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateLevelUp(long id)
        {
            try
            {
                bool result = SqlExecuter.UpdateLevelUp(id);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return NotFound();
            }
        }
    }
}