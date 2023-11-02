namespace PhotosApi.Services;

public interface IStorageService
{
    string StorePhoto(Guid id, IFormFile file);
    void DeletePhoto(Guid id);
    Tuple<byte[], string> DownloadPhoto(Guid id);
}