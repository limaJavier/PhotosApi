namespace PhotosApi.Models.PhotoFile
{
    public class PhotoFile
    {
        public IFormFile FormFile { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
    }
}
