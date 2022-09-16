using Microsoft.AspNetCore.Mvc;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : Controller
    {
        private string _rootPath;

        public UploadController(IHostEnvironment env)
        {
            _rootPath = Path.Combine(env.ContentRootPath, "public");
        }

        [HttpPost]
        [Route("/api/upload")]
        public async Task<IActionResult> UploadAsset([FromForm]List<IFormFile> files, [FromForm]string folder = "")
        {
            var dataPath = Path.Combine(_rootPath, "assets", folder);
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);

            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var ext = Path.GetExtension(formFile.FileName);
                    var id = DateTime.Now.Ticks;
                    var fileNameOnly = $"{id}{ext}";
                    var fileName = Path.Combine(dataPath, fileNameOnly);
                    using (var stream = System.IO.File.Create(fileName))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(); 
        }
    }
}