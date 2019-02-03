using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DontDrownAPI.Models;

namespace dontdrown.Controllers
{
    public class VraagController : ApiController
    {

        VraagItem[] vragen = new VraagItem[]
        {
            new VraagItem { Id = 1, Vraag = "vraag 1", MinLevel = 1, MaxLevel = 10, Type = 0},
            new VraagItem { Id = 2, Vraag = "vraag 2", MinLevel = 1, MaxLevel = 10, Type = 0},
            new VraagItem { Id = 3, Vraag = "vraag 3", MinLevel = 1, MaxLevel = 10, Type = 0},
        };

        public IEnumerable<VraagItem> GetAll()
        {
            //return vragen;
            return SqlExecuter.GetQuestionsAdmin();
        }

        public IHttpActionResult GetById(long id)
        {
            var VraagItem = SqlExecuter.GetQuestionAdmin(id);
            if (VraagItem == null)
            {
                return NotFound();
            }
            return Ok(VraagItem);
        }

        //[HttpGet("levelup/{id}", Name = "GetLevelUp")]
        //public ActionResult<bool> GetLevelUp(long id)
        //{
        //    return SqlExecuter.GetLevelUp(sqlCon, id);
        //}

        [HttpPost]
        public IHttpActionResult Create(VraagItem item)
        {
            bool succes = SqlExecuter.InsertQuestion(item);
            if (succes)
            {
                return Ok();
            }
            return NotFound();
        }

        //[HttpPut("{id}")]
        //public IActionResult Update(long id, VraagItem item)
        //{
        //    var vraagItem = _context.VraagItems.Find(id);
        //    if (vraagItem == null)
        //    {
        //        return NotFound();
        //    }

        //    vraagItem.Vraag = item.Vraag;

        //    _context.VraagItems.Update(vraagItem);
        //    _context.SaveChanges();
        //    return NoContent();
        //}

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                if (SqlExecuter.DeleteQuestion(id))
                {
                    return Ok();
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
