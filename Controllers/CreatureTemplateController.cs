using System.IO;
using Microsoft.AspNetCore.Mvc;
using OpenGMT.DB;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreatureTemplateController : Controller
    {        
        private readonly FileDB db;

        public CreatureTemplateController(FileDB db)
        {
            this.db = db;
        }

        [HttpGet("/api/creatureTemplates")]
        public IActionResult Get()
        {
            return Json(db.Creatures);
        }

        [HttpPut("/api/creatureTemplates")]
        public IActionResult Put(Creature info)
        {
            db.Upsert(info);
            return Ok();
        }
    }
}
