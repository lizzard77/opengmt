using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OpenGMT.DB;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SceneController : Controller
    {        
        private readonly FileDB db;

        public SceneController(FileDB db)
        {
            this.db = db;
        }

        [HttpGet("/api/scenes")]
        public IActionResult Get()
        {
            return Json(db.Scenes);
        }

        [HttpPut("/api/scenes")]
        public IActionResult Put(Scene info)
        {
            db.Upsert(info);
            return Ok();
        }
    }
}


            
