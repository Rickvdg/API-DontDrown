using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DontDrownAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DontDrownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private SqlConnection sqlCon = new SqlConnection(StaticValues.sqlConString);

        [HttpGet]
        public ActionResult<List<Account>> GetAll()
        {
            return SqlExecuter.GetAccounts(sqlCon, null);
        }

        [HttpGet("{classname}")]
        public ActionResult<List<Account>> GetClassmates(string classname)
        {
            return SqlExecuter.GetAccounts(sqlCon, classname);
        }

        [HttpPut("levelup/{id}")]
        public IActionResult UpdateLevelUpPlayer(long id)
        {
            try
            {
                bool result = SqlExecuter.UpdateLevelUpPlayer(sqlCon, id);
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
        
        [HttpPut("levelup/{id}/{canLevelUp}")]
        public IActionResult UpdateLevelUp(long id, string canLevelUp)
        {
            try
            {
                bool result = SqlExecuter.UpdateLevelUp(sqlCon, id, canLevelUp.ToLower() == "true" ? true : false);
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

        [HttpPut("save/{id}")]
        public IActionResult UpdateSave(long id, [FromBody]string data)
        {
            //try
            //{
            Debug.WriteLine(data);
            bool result = SqlExecuter.UpdateSaveData(sqlCon, id, data);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
            //}
            //catch
            //{
            //    return NotFound();
            //}
        }
    }
}