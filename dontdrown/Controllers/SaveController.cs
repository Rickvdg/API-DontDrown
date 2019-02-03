using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dontdrown.Controllers
{
    public class SaveController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return SqlExecuter.GetSaveData(id);
        }

        [Route("api/save/username/{username}")]
        public string Get(string username)
        {
            return SqlExecuter.GetSaveData(username);
        }

        // POST api/<controller>
        public IHttpActionResult Post(int id, [FromBody]string value)
        {
            var executedQuery = SqlExecuter.UpdateSaveData(id, value);
            if (executedQuery)
            {
                return Ok();
            }
            return NotFound();
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