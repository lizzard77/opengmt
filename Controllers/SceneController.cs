using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OpenGMT.SignalR;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SceneController : Controller
    {
        private readonly IHubContext<GameHub> hubContext;
        private readonly OpenGMTContext context;

        public SceneController(IHubContext<GameHub> hubContext, OpenGMTContext context)
        {
            this.hubContext = hubContext;
            this.context = context;
        }

        [HttpGet("/api/scenes")]
        public IActionResult Get()
        {
            return Json(context.Scenes.ToList());
        }

        [HttpGet("/api/scenes/{id}")]
        public IActionResult Get(long id)
        {
            var scene =  context.Scenes
                .Include(s => s.Creatures)
                .Include(s => s.Markers)
                .Include(s => s.Assets)
                .Include(s => s.Map)
                .FirstOrDefault(s => s.Id == id);
            return scene != null ? Json(scene) : NotFound();
        }

        [HttpPut("/api/scenes")]
        public async Task<IActionResult> Put(Scene info)
        {
            if (info == null || !ModelState.IsValid)
                return BadRequest();

            context.AttachRange(info.Creatures);
            //context.AttachRange(info.Assets);
            //context.AttachRange(info.Markers);
            //context.Attach(info.Map);
            //context.Update(info);

            var existingScene = context.Scenes
                .Include(s => s.Creatures)
                .Include(s => s.Markers)
                .Include(s => s.Assets)
                .Include(s => s.Map)
                .FirstOrDefault(s => s.Id == info.Id);

            var add = info.Creatures.Where(c => !existingScene.Creatures.Any(cc => cc.Id == c.Id)).ToList();
            var remove = existingScene.Creatures.Where(c => !info.Creatures.Any(cc => cc.Id == c.Id)).ToList();
            var removeMarkers = existingScene.Markers.Where(c => !info.Creatures.Any(cc => cc.Id == c.CreatureId)).ToList();

            foreach (var c in add)
                existingScene.Creatures.Add(c);
            foreach (var c in remove)
                existingScene.Creatures.Remove(c);
            foreach (var m in removeMarkers)
                existingScene.Markers.Remove(m);

            context.Entry(existingScene).CurrentValues.SetValues(info);
            context.SaveChanges();
            return Ok();

            //var existingScene = info;

            
            var existingScene2 = context.Scenes
                .Include(s => s.Creatures)
                .Include(s => s.Markers)
                .Include(s => s.Assets)
                .Include(s => s.Map)
                .FirstOrDefault(s => s.Id == info.Id);

            existingScene.Creatures.Clear();
            foreach (var c in info.Creatures)
            {
                //context.Attach(c);
                existingScene.Creatures.Add(c);
            }

            existingScene.Assets.Clear();
            foreach (var a in info.Assets)
                existingScene.Assets.Add(a);

            existingScene.Markers.Clear();
            foreach (var m in info.Markers)
                existingScene.Markers.Add(m);

            context.SaveChanges();
            return Ok();
        }

        [HttpPost("/api/scenes")]
        public async Task<IActionResult> Post(Scene info)
        {
            if (info == null || !ModelState.IsValid)
                return BadRequest();

            context.AttachRange(info.Creatures);
            context.Scenes.Add(info);
            context.SaveChanges();
            return Created("/api/scenes/" + info.Id, info);
        }
    }
}


            

