namespace PhotosApi.Models.PhotoModel
{
    public class MockPhotoModelRepo : IPhotoModelRepository
    {
        private HashSet<int> _takenIds = new HashSet<int>();
        private List<Photo> _photos { get; set; } = new List<Photo>();
        public bool CreatePhoto(Photo photo)
        {
            _photos.Add(new Photo() { Id = GetId(), Title = photo.Title, Description = photo.Description, Url = photo.Url, ContentType = photo.ContentType });
            return true;
        }

        public bool DeletePhoto(int id, out string url)
        {
            url = string.Empty;
            Photo? target = null;
            foreach (var photo in _photos)
                if (photo.Id == id)
                    target = photo;
            if (target == null)
                return false;
            url = target.Url;
            return _photos.Remove(target);
        }

        public IEnumerable<Photo> GetAllPhotos() => _photos;

        public Photo? GetPhoto(int id)
        {
            foreach (var photo in _photos)
                if (photo.Id == id)
                    return photo;
            return null;
        }

        public bool UpdatePhoto(int id, Photo photo)
        {
            foreach (var _photo in _photos)
            {
                if (_photo.Id == id)
                {
                    Update(_photo, photo);
                    return true;
                }
            }
            return false;
        }
        private static void Update(Photo _photo, Photo photo)
        {
            _photo.Title = photo.Title;
            _photo.Description = photo.Description;
            _photo.Album = photo.Album;
        }
        private int GetId()
        {
            for (int i = 0; ; i++)
            {
                if (i == int.MaxValue)
                {
                    throw new Exception("We ran out of indeces");
                }
                else if (!_takenIds.Contains(i))
                {
                    _takenIds.Add(i);
                    return i;
                }
            }
        }
    }
}
