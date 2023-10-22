namespace PhotosApi.Models.PhotoModel
{
    public interface IPhotoModelRepository
    {
        IEnumerable<Photo> GetAllPhotos();
        Photo? GetPhoto(int id);
        bool CreatePhoto(Photo photo);
        bool UpdatePhoto(int id, Photo photo);
        bool DeletePhoto(int id, out string url);
    }
}
