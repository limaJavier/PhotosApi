namespace PhotosApi.Services;

public interface IStorageService
{
    string StorePhoto(IFormFile file);
    void DeletePhoto(Guid id);
    byte[] DownloadPhoto(Guid id);
}