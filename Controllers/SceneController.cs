using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenGMT.DB;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SceneController : Controller
    {        
        private readonly FileDB db;
        private readonly OpenGMTContext context;

        public SceneController(FileDB db, OpenGMTContext context)
        {
            this.db = db;
            this.context = context;
        }

        [HttpGet("/api/scenes")]
        public IActionResult Get()
        {
            return Json(context.Scenes);
        }

        [HttpPut("/api/scenes")]
        public IActionResult Put(Scene info)
        {
            context.Entry(info).State = info.Id > 0 ? EntityState.Modified : EntityState.Added;
            return Ok();
        }
    }
}


            
