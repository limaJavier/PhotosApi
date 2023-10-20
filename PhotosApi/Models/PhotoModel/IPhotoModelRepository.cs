namespace PhotosApi.Models.PhotoModel
{
    public interface IPhotoModelRepository
    {
        IEnumerable<Photo> GetAllPhotos();
        Photo? GetPhoto(int id);
        bool CreatePhoto(Photo photo, string url);
        bool UpdatePhoto(int id, Photo photo);
        bool DeletePhoto(int id, out string url);
    }
}
