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
        public IActionResult Upload([FromForm] MediaUploadViewModel model)
        {
            var mediaFile = model.MediaFile;
            var employeeId = model.EmployeeId;
            string mediaUrl = mediaStore.StoreMedia(mediaFile, employeeId);
            return this.Ok(mediaUrl);
        }

        [HttpGet("media")]
        public IActionResult Media([FromQuery] string mediaUrl)
        {
            try
            {
                byte[] mediaData = mediaStore.GetMedia(mediaUrl);
                return this.File(mediaData, "image/jpeg");
            }
            catch(Exception)
            {
                Console.WriteLine($"cannot find {mediaUrl}");
                return this.Ok($"cannot find {mediaUrl}");
            }
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