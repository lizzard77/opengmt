using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SceneController : Controller
    {        
        private readonly IWebHostEnvironment env;

        public SceneController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpGet("/api/scenes")]
        public IActionResult Get()
        {
            string dataDir = env.ContentRootPath + "data";
            string dataFile = Path.Combine(dataDir, "maps.json");
            if (Directory.Exists(dataDir) && System.IO.File.Exists(dataFile))
            {
                string data = System.IO.File.ReadAllText(dataFile);
                Console.WriteLine("maps: " + dataFile);
                return Ok(data);
            } else Console.WriteLine("maps: not found" +dataFile);
            return NotFound();
        }
    }
}
