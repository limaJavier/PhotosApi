using PhotosApi.Models;

namespace PhotosApi.Repositories;

public class MockRepository : IPhotosRepository
{
    private static Dictionary<Guid, Photo> _photos = new Dictionary<Guid, Photo>();
    public void Delete(Guid id)
    {
        _photos.Remove(id);
    }

    public Photo Get(Guid id)
    {
        return _photos[id];
    }

    public void Store(Photo photo)
    {
        _photos[photo.Id] = photo;
    }

    public void Upsert(Photo photo)
    {
        _photos[photo.Id] = photo;
    }
}