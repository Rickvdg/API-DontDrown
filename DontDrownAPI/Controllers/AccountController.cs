using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private SqlConnection sqlCon = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DontDrown;Integrated Security=True;MultipleActiveResultSets=True");

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
    }
}