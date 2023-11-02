using PhotosApi.Models;

namespace PhotosApi.Services;

public interface IPhotosService
{
    void StorePhoto(Photo photo);
    Photo GetPhoto(Guid id);
    void UpsertPhoto(Photo photo);
    void DeletePhoto(Guid id);
    Photo[] GetPhotos();
}