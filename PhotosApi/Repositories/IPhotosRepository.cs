using PhotosApi.Models;

namespace PhotosApi.Repositories;

public interface IPhotosRepository
{
    void Store(Photo photo);
    Photo Get(Guid id);
    void Upsert(Photo photo);
    void Delete(Guid id);
}