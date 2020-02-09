using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Gallery.Model
{
    public class User
    {
        public User()
        {
            this.Marks = new HashSet<Mark>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
