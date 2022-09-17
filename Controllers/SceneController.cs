using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SceneController : Controller
    {        
        private readonly OpenGMTContext context;

        public SceneController(OpenGMTContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/scenes")]
        public IActionResult Get()
        {
            return Json(context.Scenes
                .Include(s => s.Creatures)
                .Include(s => s.Assets)
                .ToList());
        }

        [HttpPut("/api/scenes")]
        public IActionResult Put(Scene info)
        {
            var existingScene = context.Scenes
                .Include(s => s.Creatures)
                .Include(s => s.Assets)
                .Include(s => s.Map)
                .FirstOrDefault(s => s.Id==info.Id);

            if (existingScene == null)
            {
                context.Scenes.Add(info);
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
                    var sessions = context.Session.Where(s => s.SceneId == existingScene.Id).ToList();
                    foreach (var crea in toRemove)
                    {
                        existingScene.Creatures.Remove(crea);
                        var removeMarkers = context.MapMarker.Where(m => m.CreatureId == crea.Id && m.MapId == existingScene.Map.Id).ToList();
                        foreach (var marker in removeMarkers)
                            context.MapMarker.Remove(marker);
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
            }

            context.SaveChanges();
            return Ok();
        }
    }
}


            
