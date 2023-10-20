﻿using System;

namespace PhotosApi.Models.PhotoModel
{
    public class PhotosModelRepository : IPhotoModelRepository
    {
        private PhotosModelDbContext _dbContext { get; set; }
        public PhotosModelRepository(PhotosModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreatePhoto(Photo photo, string url)
        {
            _dbContext.Photos.Add(new Photo() { Title = photo.Title, Description = photo.Description, Album = photo.Album, Url = url });
            int i = 10;
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
