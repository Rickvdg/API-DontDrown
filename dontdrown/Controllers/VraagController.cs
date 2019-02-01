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

        //[HttpPost]
        //public IActionResult Create(VraagItem item)
        //{
        //    bool succes = SqlExecuter.InsertQuestion(sqlCon, item);
        //    return succes ? NoContent() : (IActionResult)NotFound();
        //}

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

        //[HttpDelete("{id}")]
        //public IActionResult Delete(long id)
        //{
        //    try
        //    {
        //        if (SqlExecuter.DeleteQuestion(sqlCon, id))
        //        {
        //            return NoContent();
        //        }
        //        return NoContent();
        //    }
        //    catch
        //    {
        //        return NoContent();
        //    }
        //}
    }
}
