using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OpenGMT.SignalR;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : Controller
    {        
        private readonly IHubContext<GameHub> hubContext;
        private readonly OpenGMTContext context;

        public SessionController(IHubContext<GameHub> hubContext, OpenGMTContext context)
        {
            this.hubContext = hubContext;
            this.context = context;
        }

        [HttpGet("/api/session")]
        public IActionResult Get()
        {
            var session = context.Session
                .Include(s => s.Scene)
                .Include(s => s.Scene.Markers)
                .Include(s => s.Scene.Creatures)
                .Include(s => s.Scene.Assets)
                .Include(s => s.Scene.Map)
                .FirstOrDefault();

            if (session == null)
            {
                session = new Session()
                {
                    Created = DateTime.UtcNow,
                    Scene = context.Scenes
                        .Include(s => s.Creatures)
                        .Include(s => s.Map)
                        .FirstOrDefault()
                };
                context.Session.Add(session);
                context.SaveChanges();
            }
            return Json(session);
        }

        [HttpPut("/api/session")]
        public async Task<IActionResult> Put(Session info)
        {
            var existingSession = context.Session.FirstOrDefault(s => s.Id == info.Id);
            if (existingSession == null)
            {
                context.Session.Add(info);
            } 
            else 
            {
                context.Entry(existingSession).CurrentValues.SetValues(info);
            }
            context.SaveChanges();

            var session = context.Session
                .Include(s => s.Scene)
                .Include(s => s.Scene.Markers)
                .Include(s => s.Scene.Creatures)
                .Include(s => s.Scene.Assets)
                .Include(s => s.Scene.Map)
                .FirstOrDefault();

            await hubContext.Clients.All.SendAsync("session", JsonSerializer.Serialize(session, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            return Ok();
        }

        [HttpDelete("/api/session/{id:long}")]
        public IActionResult Delete(long id)
        {
            return Ok();
        }
    }
}
