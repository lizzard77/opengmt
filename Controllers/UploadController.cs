using Microsoft.AspNetCore.Mvc;

namespace OpenGMT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : Controller
    {
        private readonly OpenGMTContext context;
        private string _rootPath;

        public UploadController(IHostEnvironment env, OpenGMTContext context)
        {
            _rootPath = Path.Combine(env.ContentRootPath, "public");
            this.context = context;
        }

        [HttpPost]
        [Route("/api/upload")]
        public async Task<IActionResult> UploadAsset([FromForm]List<IFormFile> files, [FromForm]string folder = "", [FromForm]long sceneId = 0)
        {
            var dataPath = Path.Combine(_rootPath, "assets", folder);
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);

            var scene = context.Scenes.FirstOrDefault(s => s.Id == sceneId);

            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var ext = Path.GetExtension(formFile.FileName).ToLower();
                    var id = DateTime.Now.Ticks;
                    var fileNameOnly = $"{id}{ext}";
                    var fileName = Path.Combine(dataPath, fileNameOnly);
                    using (var stream = System.IO.File.Create(fileName))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    var assetType = folder.EndsWith("audio") ? AssetType.Video : AssetType.GenericFile;
                    if (ext.EndsWith("jpg") || ext.EndsWith("png") || ext.EndsWith("gif"))
                        assetType = AssetType.Image;

                    var assetInfo = new Asset()
                    {
                        FilePath = fileName,
                        Uri = fileName.Replace(_rootPath, "").Replace('\\', '/'),
                        Name = formFile.FileName,
                        Type = assetType
                    };
                    context.Assets.Add(assetInfo);

                    if (scene != null)
                    {
                        if (scene.Assets == null)
                            scene.Assets =new List<Asset>();
                        scene.Assets.Add(assetInfo);
                    }

                    context.SaveChanges();
                }
            }
            return Ok(); 
        }
    }
}