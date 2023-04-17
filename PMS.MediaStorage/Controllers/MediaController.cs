using Microsoft.AspNetCore.Mvc;
using PMS.MediaStorage.Models;

namespace PMS.MediaStorage.Controllers
{
    [ApiController]
    [Route("api2/mediums")]
    public class MediaController : ControllerBase
    {
        private readonly MediaStorageViewModel mediaStore;

        public MediaController(MediaStorageViewModel mediaStore)
        {
            this.mediaStore = mediaStore;
        }

        [HttpPost]
        public IActionResult Upload(IFormFile mediaFile)
        {
            string mediaUrl = mediaStore.StoreMedia(mediaFile);
            return this.Ok(mediaUrl);
        }

        [HttpGet("media")]
        public IActionResult Media([FromQuery] string mediaUrl)
        {
            byte[] mediaData = mediaStore.GetMedia(mediaUrl);
            return this.File(mediaData, "image/jpeg");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var mediaFiles = Directory.GetFiles(mediaStore.MediaFolder);
            var mediaViewModels = mediaFiles.Select(file => new MediaViewModel
            {
                FileName = Path.GetFileName(file),
                Url = GetMediaUrl(Path.GetFileName(file))
            }).ToList();

            return this.Ok(mediaViewModels);
        }

        private string GetMediaUrl(string fileName)
        {
            return Url.Content($"~/media/{fileName}");
        }
    }
}