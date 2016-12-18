using System.ComponentModel.DataAnnotations;

namespace Emojo.Lib
{
    public class Photo
    {
        public string PhotoId { get; set; }
        public string LinkStandard { get; set; }
        public string LinkLow { get; set; }
        public string LinkThumbnail { get; set; }
        public double Anger { get; set; }
        public double Fear { get; set; }
        public double Happiness { get; set; }
        public double Sadness { get; set; }
        public double Surprise { get; set; }
        public long UserId { get; set; }
    }
}
