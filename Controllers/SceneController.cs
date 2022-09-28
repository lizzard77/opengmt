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
            var existingScene = context.Scenes
                .Include(s => s.Creatures)
                .Include(s => s.Markers)
                .Include(s => s.Assets)
                .Include(s => s.Map)
                .FirstOrDefault(s => s.Id==info.Id);

            if (existingScene == null)
            {
                var scene = new Scene();
                scene.Name = info.Name;
                context.Scenes.Add(scene);
            } 
            else 
            {
                context.Entry(existingScene).CurrentValues.SetValues(info);

                if (info.Creatures != null)
                {
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

                if (info.Assets != null)
                {                
                    foreach (var asset in info.Assets)
                    {
                        var existingAsset = existingScene.Assets.FirstOrDefault(p => p.Id == asset.Id);
                        if (existingAsset == null)
                        {
                            existingScene.Assets.Add(asset);
                        }
                        else
                        {
                            context.Entry(existingAsset).CurrentValues.SetValues(asset);
                        }
                    }

                    var assetsToRemove = existingScene.Assets.Where(c => !info.Assets.Any(cc => cc.Id == c.Id)).ToList();
                    foreach (var asset in assetsToRemove)
                    {
                        existingScene.Assets.Remove(asset);
                    }
                }

                if (info.Map != null)
                    existingScene.Map = context.Maps.FirstOrDefault(m => m.Id == info.Map.Id);
            }

            context.SaveChanges();

            /*
            var session = context.Session
                .Include(s => s.Scene)
                .Include(s => s.Scene.Markers)
                .Include(s => s.Scene.Creatures)
                .Include(s => s.Scene.Assets)
                .Include(s => s.Scene.Map)
                .FirstOrDefault();
            if (session.SceneId == existingScene.Id)
            {
                await hubContext.Clients.All.SendAsync("sessionInfo", JsonSerializer.Serialize(session, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            }
            */

            return Ok();
        }

        [HttpPost("/api/scenes")]
        public async Task<IActionResult> Post(Scene info)
        {
            if (info == null || !ModelState.IsValid)
                return BadRequest();

            context.Scenes.Add(info);
            context.SaveChanges();
            return Created("/api/scenes/" + info.Id, info);
        }
    }
}


            
