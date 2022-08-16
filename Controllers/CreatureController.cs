using System.IO;
using Microsoft.AspNetCore.Mvc;
using OpenGMT.DB;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreatureController : Controller
    {        
        private readonly FileDB db;

        public CreatureController(FileDB db)
        {
            this.db = db;
        }

        [HttpGet("/api/creatures")]
        public IActionResult Get()
        {
            return Json(db.Creatures);
        }

        [HttpPut("/api/creatures")]
        public IActionResult Put(Creature info)
        {
            db.Upsert(info);
            return Ok();
        }
    }
}
