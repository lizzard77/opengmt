using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : Controller
    {        
        private readonly IWebHostEnvironment env;

        public SessionController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpGet("/api/session")]
        public IActionResult Get()
        {
            string dataDir = env.ContentRootPath + "data";
            string dataFile = Path.Combine(dataDir, "session.json");
            if (Directory.Exists(dataDir) && System.IO.File.Exists(dataFile))
            {
                string data = System.IO.File.ReadAllText(dataFile);
                Console.WriteLine("session: " + dataFile);
                return Ok(data);
            } else Console.WriteLine("session: not found" +dataFile);
            return NotFound();
        }

        [HttpPut("/api/session")]
        public IActionResult Put(SessionInfo info)
        {
            string dataDir = env.ContentRootPath + "data";
            string dataFile = Path.Combine(dataDir, "session.json");
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);
                
            info.Updated = DateTime.UtcNow;
            System.IO.File.WriteAllText(dataFile, JsonSerializer.Serialize(info));
            
            return Ok(info);
        }

        [HttpDelete("/api/session")]
        public IActionResult Delete()
        {
            string dataDir = env.ContentRootPath + "data";
            string dataFile = Path.Combine(dataDir, "session.json");
            if (Directory.Exists(dataDir) && System.IO.File.Exists(dataFile))
            {
                System.IO.File.Delete(dataFile);
                return Ok();
            }
            return NotFound();
        }

        public class SessionInfo
        {
            public int SceneId { get; set; }
            public DateTime Updated { get; set; }
        }
    }
}
