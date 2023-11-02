using PhotosApi.Models;

namespace PhotosApi.Repositories;

public class PhotosRepository : IPhotosRepository
{
    private PhotosDBContext _dbContext;
    public PhotosRepository(PhotosDBContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.SaveChanges();
    }
    public void Store(Photo photo)
    {
        _dbContext.Products.Add(photo);
    }
    public Photo Get(Guid id)
    {
        return _dbContext.Products.Find(id) ?? throw new NullReferenceException();
    }
    public void Upsert(Photo photo)
    {
        Delete(photo.Id);
        _dbContext.Products.Add(photo);
    }
    public void Delete(Guid id)
    {
        var photoToRemove = _dbContext.Products.Find(id) ?? throw new NullReferenceException();
        _dbContext.Remove(photoToRemove);
    }
    public Photo[] GetAll()
    {
        var data = _dbContext.Products;
        return _dbContext.Products.AsEnumerable<Photo>().ToArray();
    }
}