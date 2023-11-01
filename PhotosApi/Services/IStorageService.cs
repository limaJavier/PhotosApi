namespace PhotosApi.Services;

public interface IStorageService
{
    void StorePhoto(IFormFile file);
    void DeletePhoto(Guid id);
    byte[] DownloadPhoto(Guid id);
}