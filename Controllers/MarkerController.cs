using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OpenGMT.SignalR;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarkerController : Controller
    {
        private readonly IHubContext<GameHub> hubContext;
        private readonly OpenGMTContext context;

        public MarkerController(IHubContext<GameHub> hubContext, OpenGMTContext context)
        {
            this.hubContext = hubContext;
            this.context = context;
        }

        [HttpGet("/api/markers/{id}")]
        public IActionResult Get(long id)
        {
            return Json(context.MapMarker.Where(m => m.SceneId == id));
        }

        [HttpPut("/api/marker")]
        public async Task<IActionResult> Put(MapMarker info)
        {
            var existingMarker = context.MapMarker
                .Include(s => s.Creature)
                .Include(s => s.Scene)
                .Include(s => s.Map)
                .FirstOrDefault(s => s.Id == info.Id);

            info.Creature = context.Creatures.FirstOrDefault(c=>c.Id == info.CreatureId);
            info.Map = context.Maps.FirstOrDefault(c=>c.Id == info.MapId);
            info.Scene = context.Scenes.FirstOrDefault(c=>c.Id == info.SceneId);

            if (existingMarker == null)
            {
                context.MapMarker.Add(info);
            } 
            else 
            {
                context.Entry(existingMarker).CurrentValues.SetValues(info);
            }
            
            context.SaveChanges();

            existingMarker = context.MapMarker
                .Include(s => s.Creature)
                .Include(s => s.Scene)
                .Include(s => s.Map)
                .FirstOrDefault(s => s.Id == info.Id);

            await hubContext.Clients.All.SendAsync("marker", JsonSerializer.Serialize(existingMarker, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            return Ok();
        }
    }
}


            
