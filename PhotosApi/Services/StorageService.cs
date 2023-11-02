namespace PhotosApi.Services;

public class StorageService : IStorageService
{
    public string StorePhoto(IFormFile file)
    {
        if (isValidFileExtension(file.FileName))
        {

        }
    }
    public void DeletePhoto(Guid id)
    {

    }
    public byte[] DownloadPhoto(Guid id)
    {

    }

    private bool isValidFileExtension(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return extension == ".png" || extension == ".jpg" || extension == ".jpeg";
    }
}