using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Image_Gallery.Model
{
    public class Mark
    {
        public Mark () { }

        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public int PictureId { get; set; }

        public string Value { get; set; }

        public virtual User User { get; set; }
        public virtual Picture Picture { get; set; }

    }
}
