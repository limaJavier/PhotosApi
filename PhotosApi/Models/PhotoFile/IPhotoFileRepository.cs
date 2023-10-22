namespace PhotosApi.Models.PhotoFile
{
    public interface IPhotoFileRepository
    {
        bool UploadPhoto(PhotoFile photoFile, out string url);
        bool DownloadPhoto(string url, out byte[] bytes);
        bool DeletePhoto(string url);
    }
}
