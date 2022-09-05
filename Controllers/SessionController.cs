using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OpenGMT.DB;
using OpenGMT.SignalR;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : Controller
    {        
        private readonly FileDB db;
        private readonly IHubContext<GameHub> hubContext;
        private readonly OpenGMTContext context;

        public SessionController(FileDB db, IHubContext<GameHub> hubContext, OpenGMTContext context)
        {
            this.db = db;
            this.hubContext = hubContext;
            this.context = context;
        }

        [HttpGet("/api/session")]
        public IActionResult Get()
        {
            var session = context.Session
                .Include(s => s.Scene)
                .Include(s => s.Scene.Creatures)
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
            context.Entry(info).State = info.Id > 0 ? EntityState.Modified : EntityState.Added;
            await hubContext.Clients.All.SendAsync("sessionInfo", JsonSerializer.Serialize(info, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            return Ok();
        }

        [HttpDelete("/api/session/{id:long}")]
        public IActionResult Delete(long id)
        {
            return Ok();
        }
    }
}
