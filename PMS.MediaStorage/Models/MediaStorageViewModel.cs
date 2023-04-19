namespace PMS.MediaStorage.Models
{
    public class MediaStorageViewModel
    {
        private readonly string mediaFolder;

        public MediaStorageViewModel(string mediaFolder)
        {
            this.mediaFolder = mediaFolder;
        }

        public string MediaFolder
        {
            get { return mediaFolder; }
        }

        public string StoreMedia(IFormFile mediaFile, int employeeId)
        {
            string fileName = $"{employeeId}_{Guid.NewGuid().ToString()}{GetFileExtension(mediaFile.ContentType)}";
            string[] parts = fileName.Split('_');
            string firstPart = $"{parts[0]}_";

            var mediaFiles = Directory.GetFiles(MediaFolder);
            var mediaViewModels = mediaFiles.Select(file => Path.GetFileName(file)).ToList();
            foreach (var media in mediaViewModels)
            {
                if (media.StartsWith(firstPart))
                {
                    // Update existing image
                    string filePath = Path.Combine(MediaFolder, media);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        mediaFile.CopyTo(stream);
                    }
                    return GetMediaUrl(media);
                }
            }

            // Create new image
            string newFilePath = Path.Combine(MediaFolder, fileName);
            using (var stream = new FileStream(newFilePath, FileMode.Create))
            {
                mediaFile.CopyTo(stream);
            }
            return GetMediaUrl(fileName);
        }

        public byte[] GetMedia(string mediaUrl)
        {
            string fileName = Path.GetFileName(mediaUrl);
            string filePath = Path.Combine(mediaFolder, fileName);
            return File.ReadAllBytes(filePath);
        }

        private string GetMediaUrl(string fileName)
        {
            return $"http://localhost:5275/api2/mediums/media?mediaUrl={fileName}";
        }

        private string GetFileExtension(string mediaType)
        {
            switch (mediaType)
            {
                case "image/jpeg":
                    return ".jpg";
                case "image/png":
                    return ".png";
                case "video/mp4":
                    return ".mp4";
                default:
                    throw new ArgumentException($"Unsupported media type: {mediaType}");
            }
        }
    }
}