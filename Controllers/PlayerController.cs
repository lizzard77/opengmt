using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OpenGMT.SignalR;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : Controller
    {        
        private readonly IHubContext<GameHub> hubContext;
        private readonly OpenGMTContext context;

        public PlayerController(IHubContext<GameHub> hubContext, OpenGMTContext context)
        {
            this.hubContext = hubContext;
            this.context = context;
        }

        [HttpGet("/api/players")]
        public IActionResult Get()
        {
            var list = context.Players.ToList();
            return list != null && list.Count() > 0 ? Json(list) : NotFound();
        }

        [HttpGet("/api/players/{id}")]
        public IActionResult Get(long id)
        {
            var campaign = context.Players.FirstOrDefault(p => p.Id == id);
            return campaign != null ? Json(campaign) : NotFound();
        }

        [HttpPut("/api/players")]
        public async Task<IActionResult> Put(Player info)
        {
            if (info == null || !ModelState.IsValid)
                return BadRequest();

            context.Update(info);
            context.SaveChanges();
            return Json(info);
        }

        [HttpPost("/api/players")]
        public async Task<IActionResult> Post(Player info)
        {
            if (info == null || !ModelState.IsValid)
                return BadRequest();

            context.Players.Add(info);
            context.SaveChanges();
            return Created("/api/players/" + info.Id, info);
        }
    }
}
