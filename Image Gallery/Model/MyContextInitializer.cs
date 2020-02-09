using System.Collections.Generic;
using System.Data.Entity;

namespace Image_Gallery.Model
{
    class MyContextInitializer: DropCreateDatabaseIfModelChanges<GalleryContext>
    {
        protected override void Seed(GalleryContext db)
        {
            List<Picture> pictures = new List<Picture>();
            for (int i = 1; i < 16; i++)
            {
                Picture picture = new Picture();
                picture.Name = "../Image/" + i + ".jpg";
                pictures.Add(picture);
            }
            db.Pictures.AddRange(pictures);
            db.SaveChanges();
            base.Seed(db);
        }
    }
}
