using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenGMT.DB;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreatureController : Controller
    {        
        private readonly FileDB db;
        private readonly OpenGMTContext context;

        public CreatureController(FileDB db, OpenGMTContext context)
        {
            this.db = db;
            this.context = context;
        }

        [HttpGet("/api/creatures")]
        public IActionResult Get()
        {
            return Json(context.Creatures);
        }

        [HttpPut("/api/creatures")]
        public IActionResult Put(Creature info)
        {
            context.Entry(info).State = info.Id > 0 ? EntityState.Modified : EntityState.Added;
            return Ok();
        }
    }
}
