using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib
{
    public class Photo
    {
        [Key]
        public string PhotoId { get; set; }
        public string LinkStandard { get; set; }
        public string LinkLow { get; set; }
        public string LinkThumbnail { get; set; }
        public double Anger { get; set; }
        public double Fear { get; set; }
        public double Happiness { get; set; }
        public double Sadness { get; set; }
        public double Surprise { get; set; }
        [Required]
        public virtual User User { get; set; }
    }
}
