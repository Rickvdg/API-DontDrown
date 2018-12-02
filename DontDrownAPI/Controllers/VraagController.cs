using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DontDrownAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data.Sql;
using DontDrownAPI.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DontDrownAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class VraagController : ControllerBase
    {
        private readonly VraagContext _context;
        private SqlConnection sqlCon = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DontDrown;Integrated Security=True;MultipleActiveResultSets=True");

        public VraagController(VraagContext context)
        {
            _context = context;

            if (_context.VraagItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.VraagItems.Add(new VraagItem { Id = 1, Vraag = "1 + 2 = ?", Type = 0, Hint = "Hoofdstuk 1, paragraaf 2" });

                _context.SaveChanges();
            }
        }

        [HttpGet(Name = "GetVragen")]
        public ActionResult<List<VraagItem>> GetAll()
        {
            return SqlExecuter.GetQuestionsAdmin(sqlCon);
        }

        [HttpGet("{id}", Name = "GetVraag")]
        public ActionResult<VraagItem> GetById(long id)
        {
            return SqlExecuter.GetQuestionAdmin(sqlCon, id);
        }

        [HttpPost]
        public IActionResult Create(VraagItem item)
        {
            bool succes = SqlExecuter.InsertQuestion(sqlCon, item);
            return succes ? NoContent() : (IActionResult)NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, VraagItem item)
        {
            var vraagItem = _context.VraagItems.Find(id);
            if (vraagItem == null)
            {
                return NotFound();
            }

            vraagItem.Vraag = item.Vraag;

            _context.VraagItems.Update(vraagItem);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                if (SqlExecuter.DeleteQuestion(sqlCon, id))
                {
                    return NoContent();
                }
                return NoContent();
            }
            catch
            {
                return NoContent();
            }
        }
    }
}
