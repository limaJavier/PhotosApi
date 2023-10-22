using System;

namespace PhotosApi.Models.PhotoModel
{
    public class PhotoModelRepository : IPhotoModelRepository
    {
        private PhotoModelDbContext _dbContext { get; set; }
        public PhotoModelRepository(PhotoModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreatePhoto(Photo photo)
        {
            _dbContext.Photos.Add(new Photo() { Title = photo.Title, Description = photo.Description, Album = photo.Album, Url = photo.Url, ContentType = photo.ContentType});
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeletePhoto(int id, out string url)
        {
            url = string.Empty;
            var photo = _dbContext.Photos.FirstOrDefault(photo => photo.Id == id);
            if (photo == null)
                return false;
            url = photo.Url;
            _dbContext.Remove(photo);
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<Photo> GetAllPhotos() => _dbContext.Photos;

        public Photo? GetPhoto(int id)
        {
            var photo = _dbContext.Photos.FirstOrDefault(photo => photo.Id == id);
            return photo;
        }

        public bool UpdatePhoto(int id, Photo photo)
        {
            var _photo = _dbContext.Photos.FirstOrDefault(photo => photo.Id == id);
            if (_photo == null)
                return false;
            _photo.Title = photo.Title;
            _photo.Description = photo.Description;
            _photo.Album = photo.Album;
            _dbContext.Update(_photo);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
