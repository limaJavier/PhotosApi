using PhotosApi.Models;

namespace PhotosApi.Repositories;

public interface IPhotosRepository
{
    void Store(Guid id, Photo photo);
    void Get(Guid id);
    void Upsert(Guid id, Photo photo);
    void Delete(Guid id);
}