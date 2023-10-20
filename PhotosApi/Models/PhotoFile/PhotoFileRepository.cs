using Microsoft.AspNetCore.Mvc;

namespace PhotosApi.Models.PhotoFile
{
    public class PhotoFileRepository : IPhotoFileRepository
    {
        public bool UploadPhoto(PhotoFile photoFile, out string url)
        {
            url = string.Empty;
            if (photoFile.FormFile.Length > 0)
            {
                var headers = photoFile.FormFile.ContentDisposition.Split(";");
                var fileName = headers[headers.Length - 1].Split('"')[1];
                url = GetUrl(fileName.Remove(fileName.Length - 4));
                try
                {
                    using (var stream = new FileStream(url, FileMode.Create))
                    {
                        photoFile.FormFile.CopyTo(stream);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        public bool DeletePhoto(string url)
        {
            try
            {
                File.Delete(url);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string GetUrl(string name)
        {
            var path = "Storage";
            var url = $"{path}\\{name}";
            var i = 1;
            while (Directory.GetFiles(path).Any(file => file == $"{url}.png"))
                url = $"{path}\\{name}{i++}";
            return $"{url}.png";
        }
    }
}
