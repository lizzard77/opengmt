using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreatureController : Controller
    {        
        private readonly IWebHostEnvironment env;

        public CreatureController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpGet("/api/creatures")]
        public IActionResult Get()
        {
            string dataDir = env.ContentRootPath + "data";
            Console.WriteLine("dataDir: " + dataDir);
            string dataFile = Path.Combine(dataDir, "creatures.json");
            if (Directory.Exists(dataDir) && System.IO.File.Exists(dataFile))
            {
                string data = System.IO.File.ReadAllText(dataFile);
                return Ok(data);
            }
            return NotFound();
        }
    }
}
