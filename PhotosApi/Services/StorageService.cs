using System;

namespace PhotosApi.Services;

public class StorageService : IStorageService
{
    private static Dictionary<Guid, Tuple<string, string>> _fileRecord = new Dictionary<Guid, Tuple<string, string>>();
    public string StorePhoto(Guid id, IFormFile file)
    {
        if (isValidContentType(file.ContentType))
        {
            var url = CreateUrl(id, file.FileName, out string storageName);
            using var stream = new FileStream(storageName, FileMode.Create);
            file.CopyTo(stream);
            _fileRecord.Add(id, new Tuple<string, string>(storageName, file.ContentType));
            return url;
        }

        return "";
    }
    public void DeletePhoto(Guid id)
    {
        var path = _fileRecord[id].Item1;
        _fileRecord.Remove(id);
        File.Delete(path);
    }
    public Tuple<byte[], string> DownloadPhoto(Guid id)
    {
        var path = _fileRecord[id].Item1;
        var contentType = _fileRecord[id].Item2;
        var file = File.ReadAllBytes(path);
        return new Tuple<byte[], string>(file, contentType);
    }

    private static bool isValidContentType(string contentType) => contentType == "image/png" || contentType == "image/jpg" || contentType == "image/jpeg";
    private static string CreateUrl(Guid id, string fileName, out string storageName)
    {
        var extension = Path.GetExtension(fileName);
        var name = Path.GetFileNameWithoutExtension(fileName);
        storageName = $"storage/{name}_{id}{extension}";
        return $"https://localhost:5002/storage/{id}";
    }
}