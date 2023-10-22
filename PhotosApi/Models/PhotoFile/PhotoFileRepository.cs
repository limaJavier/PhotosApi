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

                string bla = Path.GetExtension(fileName);


                // Verifying we're dealing with image filesF
                if (Path.GetExtension(fileName.ToLower()) != ".png" && Path.GetExtension(fileName.ToLower()) != ".jpeg" && Path.GetExtension(fileName.ToLower()) != ".jpg")
                    return false;

                url = GetUrl(fileName);
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
            var extension = Path.GetExtension(name);
            var path = "Storage";
            var url = $"{path}\\{Path.GetFileNameWithoutExtension(name)}";
            var i = 1;
            while (Directory.GetFiles(path).Any(file => file == $"{url}{extension}"))
                url = $"{url}{i++}";
            return $"{url}{extension}";
        }

        public bool DownloadPhoto(string url, out byte[] bytes)
        {
            bytes = File.ReadAllBytes(url);
            return true;
        }
    }
}
