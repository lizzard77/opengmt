using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenGMT.DB;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapController : Controller
    {        
        private readonly FileDB db;
        private readonly OpenGMTContext context;

        public MapController(FileDB db, OpenGMTContext context)
        {
            this.db = db;
            this.context = context;
        }

        [HttpGet("/api/maps")]
        public IActionResult Get()
        {
            return Json(context.Maps);
        }

        [HttpPut("/api/maps")]
        public IActionResult Put(Map info)
        {
            context.Entry(info).State = info.Id > 0 ? EntityState.Modified : EntityState.Added;
            return Ok();
        }
    }
}
