using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PhotosApi.Models.PhotoModel
{
    public class PhotoModelDbContext : DbContext
    {
        public PhotoModelDbContext(DbContextOptions<PhotoModelDbContext> options) : base(options) { }
        public DbSet<Photo> Photos => Set<Photo>();
    }
}
