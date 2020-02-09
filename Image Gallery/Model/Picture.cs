using System;
using System.Collections.Generic;
using System.Linq;

namespace Image_Gallery.Model
{
    public class Picture
    {
        public Picture()
        {
            this.Marks = new HashSet<Mark>();
            Name = String.Empty;
            Author = "Admin";
            Date = DateTime.Now;
            AverageMark = 0;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public double AverageMark { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }

        public override string ToString()
        {
            string shortName = Name.Substring(Name.LastIndexOf("/") + 1);
            AverageMark = AverageMarkCount(this, Marks);
            string info =
                $"Name: {shortName}\n" +
                $"Author: {Author}\n" +
                $"Date: {Date.ToString("d")}\n" +
                $"Average mark: {Math.Round(AverageMark, 2)}";
            return info;
        }

        private double AverageMarkCount(Picture picture, ICollection<Mark> marks)
        {
            if (marks.Count != 0)
            {
                var picMarksStr = marks.Where(m => m.PictureId == picture.Id).Select(m => m.Value);
                List<int> picMarksInt = new List<int>();
                foreach (var pm in picMarksStr)
                    picMarksInt.Add(int.Parse(pm));
                if (picMarksInt.Count() != 0)
                    return ((double)picMarksInt.Sum() / (double)picMarksInt.Count());
            }
            return 0;
        }
    }
}
