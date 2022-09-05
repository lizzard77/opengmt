using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SoundController : Controller
    {        
        private readonly IWebHostEnvironment env;
        private readonly OpenGMTContext context;

        public SoundController(IWebHostEnvironment env, OpenGMTContext context)
        {
            this.env = env;
            this.context = context;
        }

        [HttpGet("/api/sounds")]
        public IActionResult Get()
        {
            string dataDir = env.ContentRootPath + "/public/assets/sounds";
            if (Directory.Exists(dataDir))
            {
                var files = Directory.GetFiles(dataDir, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".mp3") || s.EndsWith(".wav")  || s.EndsWith(".m4a")  || s.EndsWith(".ogg"))
                    .Select(f => f.Replace(env.ContentRootPath, "")
                        .Replace("\\", "/")
                        .Replace("public/", "")
                        .Replace("wwwroot/", ""));
                return Ok(files);
            }
            return NotFound();
        }
    }
}
