namespace PMS.MediaStorage.Models
{
    public class MediaUploadViewModel
    {
        public int EmployeeId { get; set; }
        public IFormFile MediaFile { get; set; }
    }
}