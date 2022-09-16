using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreatureController : Controller
    {        
        private readonly OpenGMTContext context;

        public CreatureController(OpenGMTContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/creatures")]
        public IActionResult Get()
        {
            return Json(context.Creatures);
        }

        [HttpPut("/api/creatures")]
        public IActionResult Put(Creature info)
        {
            context.Entry(info).State = info.Id > 0 ? EntityState.Modified : EntityState.Added;
            context.SaveChanges();
            return Ok();
        }
    }
}
