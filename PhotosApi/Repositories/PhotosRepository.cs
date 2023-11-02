using PhotosApi.Models;

namespace PhotosApi.Repositories;

public class PhotosRepository : IPhotosRepository
{
    private PhotosDBContext _dbContext;
    public PhotosRepository(PhotosDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Store(Photo photo)
    {
        _dbContext.Photos.Add(photo);
        _dbContext.SaveChanges();
    }
    public Photo Get(Guid id)
    {
        return _dbContext.Photos.Find(id) ?? throw new NullReferenceException();
    }
    public void Upsert(Photo photo)
    {
        _dbContext.Update(photo);
        _dbContext.SaveChanges();
    }
    public void Delete(Guid id)
    {
        var photo = _dbContext.Photos.Find(id);
        _dbContext.Photos.Remove(photo);
        _dbContext.SaveChanges();
    }
    public Photo[] GetAll()
    {
        return _dbContext.Photos.AsEnumerable<Photo>().ToArray();
    }
}