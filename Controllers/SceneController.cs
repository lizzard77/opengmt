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

            var existingScene = context.Scenes
                .Include(s => s.Creatures)
                .FirstOrDefault(s => s.Id==info.Id);

            if (existingScene == null)
            {
                context.Scenes.Add(info);
            } 
            else 
            {
                context.Entry(existingScene).CurrentValues.SetValues(info);
                foreach (var crea in info.Creatures)
                {
                    var existingCrea = existingScene.Creatures.FirstOrDefault(p => p.Id == crea.Id);
                    if (existingCrea == null)
                    {
                        existingScene.Creatures.Add(crea);
                    }
                    else
                    {
                        context.Entry(existingCrea).CurrentValues.SetValues(crea);
                    }
                }

                var toRemove = existingScene.Creatures.Where(c => !info.Creatures.Any(cc => cc.Id == c.Id)).ToList();
                foreach (var crea in toRemove)
                {
                    existingScene.Creatures.Remove(crea);
                }
            }

            context.SaveChanges();
            return Ok();
        }
    }
}


            
