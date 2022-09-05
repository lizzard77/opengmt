using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenGMT.DB;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MigrateController : Controller
    {        
        private readonly FileDB db;
        private readonly OpenGMTContext context;

        public MigrateController(FileDB db, OpenGMTContext context)
        {
            this.db = db;
            this.context = context;
        }

        [HttpGet("/api/import")]
        public IActionResult Get()
        {
            foreach (var c in db.Creatures)
            {
                if (context.Creatures.Any(cc => cc.Id == c.Id))
                    context.Entry(c).State = EntityState.Modified;
                else
                    context.Entry(c).State = EntityState.Added;
            }
            context.SaveChanges();

            foreach (var c in db.Maps)
            {
                if (context.Maps.Any(cc => cc.Id == c.Id))
                    context.Entry(c).State = EntityState.Modified;
                else
                    context.Entry(c).State = EntityState.Added;
            }
            context.SaveChanges();
            
            foreach (var c in db.Scenes)
            {
                if (context.Scenes.Any(cc => cc.Id == c.Id))
                    context.Entry(c).State = EntityState.Modified;
                else
                    context.Entry(c).State = EntityState.Added;


                if (c.Map != null)
                    c.Map = context.Maps.FirstOrDefault(m => m.Id == c.Map.Id);
            }
            context.SaveChanges();

            return Ok();
        }
    }
}
