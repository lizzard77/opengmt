using System.IO;
using Microsoft.AspNetCore.Mvc;
using OpenGMT.DB;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapController : Controller
    {        
        private readonly FileDB db;

        public MapController(FileDB db)
        {
            this.db = db;
        }

        [HttpGet("/api/maps")]
        public IActionResult Get()
        {
            return Json(db.Maps);
        }

        [HttpPut("/api/maps")]
        public IActionResult Put(Map info)
        {
            db.Upsert(info);
            return Ok();
        }
    }
}
