using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        [Route("api/save/{id}")]
        [HttpGet]
        public string Get(int id)
        {
            var data = SqlExecuter.GetSaveData(id);
            Debug.WriteLine(data);
            return data;
        }

        [Route("api/save/username/{username}")]
        public string Get(string username)
        {
            var data = SqlExecuter.GetSaveData(username);
            Debug.WriteLine(data);
            return data;
        }

        [Route("api/save/upgrade/{id}")]
        public bool GetUpgradable(int id)
        {
            return SqlExecuter.GetLevelUp(id);
        }

        [Route("api/save/upgrade/{id}")]
        [HttpPut]
        public bool PostUpgradePlayer(int id)
        {
            return SqlExecuter.UpdateLevelUp(id);
        }

        // PUT api/<controller>/{id}
        [Route("api/save/{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            Debug.WriteLine(value);
            var executedQuery = SqlExecuter.UpdateSaveData(id, value);
            if (executedQuery)
            {
                return Ok();
            }
            throw new Exception("Could not save the data");
        }

    }
}