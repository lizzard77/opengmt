using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OpenGMT.DB;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : Controller
    {        
        private readonly FileDB db;

        public SessionController(FileDB db)
        {
            this.db = db;
        }

        [HttpGet("/api/session")]
        public IActionResult Get()
        {
            return Json(db.Session);
        }

        [HttpPut("/api/session")]
        public IActionResult Put(Session info)
        {
            db.UpsertSingleton(info);
            return Ok();
        }

        [HttpDelete("/api/session/{id:long}")]
        public IActionResult Delete(long id)
        {
            db.DeleteSingleton<Session>();
            return Ok();
        }
    }
}
