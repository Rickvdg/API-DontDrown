using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DontDrownAPI.Controllers
{
    [Route("api/[controller]")]
    public class SaveController : Controller
    {
        private SqlConnection sqlCon = new SqlConnection(StaticValues.sqlConString);

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return SqlExecuter.GetSaveData(sqlCon, id);
        }

        [HttpGet("/username/{username}")]
        public string GetByUsername(string username)
        {
            return SqlExecuter.GetSaveData(sqlCon, username);
        }


        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]string data)
        {
            bool result = SqlExecuter.UpdateSaveData(sqlCon, 1, data);
            return Ok();
        }
    }
}
