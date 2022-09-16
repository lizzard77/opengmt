using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapController : Controller
    {        
        private readonly OpenGMTContext context;

        public MapController(OpenGMTContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/maps")]
        public IActionResult Get()
        {
            return Json(context.Maps);
        }

        [HttpPut("/api/maps")]
        public IActionResult Put(Map info)
        {
            context.Entry(info).State = info.Id > 0 ? EntityState.Modified : EntityState.Added;
            return Ok();
        }
    }
}
