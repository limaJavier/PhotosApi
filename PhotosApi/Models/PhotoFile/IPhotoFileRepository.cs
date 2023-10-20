namespace PhotosApi.Models.PhotoFile
{
    public interface IPhotoFileRepository
    {
        bool UploadPhoto(PhotoFile photoFile, out string url);
        bool DeletePhoto(string url);
    }
}
