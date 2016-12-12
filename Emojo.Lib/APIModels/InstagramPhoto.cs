using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib.APIModels {
    public class InstagramPhoto {
        public string PhotoId { get; set; }
        public string LinkStandard { get; set; }
        public string LinkLow { get; set; }
        public string LinkThumbnail { get; set; }
        public User User { get; set; }
    }
}
