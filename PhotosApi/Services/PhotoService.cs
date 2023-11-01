using PhotosApi.Models;
using PhotosApi.Repositories;

namespace PhotosApi.Services;

public class PhotosService : IPhotosService
{
    IPhotosRepository _repository;
    public PhotosService(IPhotosRepository repository)
    {
        _repository = repository;
    }

    public void DeletePhoto(Guid id)
    {
        _repository.Delete(id);
    }

    public Photo GetPhoto(Guid id)
    {
        return _repository.Get(id);
    }

    public void StorePhoto(Photo photo)
    {
        _repository.Store(photo);
    }

    public void UpsertPhoto(Photo photo)
    {
        _repository.Upsert(photo);
    }
}