using Microsoft.EntityFrameworkCore;
using PhotosApi.Models;

namespace PhotosApi.Repositories
{
    public class PhotosDBContext : DbContext
    {
        public PhotosDBContext(DbContextOptions<PhotosDBContext> options) : base(options) { }
        public DbSet<Photo> Products => Set<Photo>();
    }
}