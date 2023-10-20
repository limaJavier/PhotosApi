using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PhotosApi.Models.PhotoModel
{
    public class PhotosModelDbContext : DbContext
    {
        public PhotosModelDbContext(DbContextOptions<PhotosModelDbContext> options) : base(options) { }
        public DbSet<Photo> Photos => Set<Photo>();
    }
}
