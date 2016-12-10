using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Datebase
{
    public class Photo
    {
        public int Id { get; set; }
        public double Anger { get; set; }
        public double Fear { get; set; }
        public double Happiness { get; set; }
        public double Sadness { get; set; }
        public double Surprise { get; set; }
        public User User { get; set; }
    }
}
