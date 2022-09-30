using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OpenGMT.SignalR;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignController : Controller
    {        
        private readonly IHubContext<GameHub> hubContext;
        private readonly OpenGMTContext context;

        public CampaignController(IHubContext<GameHub> hubContext, OpenGMTContext context)
        {
            this.hubContext = hubContext;
            this.context = context;
        }

        [HttpGet("/api/campaigns")]
        public IActionResult Get()
        {
            var list = context.Campaigns
                .Include(s => s.Players)
                .ToList();
            return list != null && list.Count() > 0 ? Json(list) : NotFound();
        }

        [HttpGet("/api/campaigns/{id}")]
        public IActionResult Get(long id)
        {
            var campaign = context.Campaigns
                .Include(s => s.CurrentScene)
                .Include(s => s.CurrentScene.Map)
                .Include(s => s.CurrentScene.Markers)
                .Include(s => s.CurrentScene.Assets)
                .Include(s => s.CurrentScene.Creatures)
                .Include(s => s.Players)
                .FirstOrDefault(c => c.Id == id);
            return campaign != null ? Json(campaign) : NotFound();
        }

        [HttpPut("/api/campaigns")]
        public async Task<IActionResult> Put(Campaign info)
        {
            if (info == null || !ModelState.IsValid)
                return BadRequest();

            context.Update(info);
            context.SaveChanges();
            return Json(info);
        }

        [HttpPost("/api/campaigns")]
        public async Task<IActionResult> Post(Campaign info)
        {
            if (info == null || !ModelState.IsValid)
                return BadRequest();
            
            context.AttachRange(info.Players);
            context.Attach(info.CurrentScene);            
            context.Campaigns.Add(info);
            context.SaveChanges();
            return Created("/api/campaigns/" + info.Id, info);
        }
    }
}
