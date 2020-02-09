using System.Data.Entity;

namespace Image_Gallery.Model
{
    public class GalleryContext : DbContext
    {
        static GalleryContext()
        {
            Database.SetInitializer<GalleryContext>(new MyContextInitializer());
        }
        public GalleryContext(): base("GalleryDB") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Mark> Marks { get; set; }
    }
}
