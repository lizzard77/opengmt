using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

        public SessionController(FileDB db, IHubContext<GameHub> hubContext)
        {
            this.db = db;
            this.hubContext = hubContext;
        }

        [HttpGet("/api/session")]
        public IActionResult Get()
        {
            return Json(db.Session);
        }

        [HttpPut("/api/session")]
        public async Task<IActionResult> Put(Session info)
        {
            db.UpsertSingleton(info);
            await hubContext.Clients.All.SendAsync("sessionInfo", JsonSerializer.Serialize(info, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
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
