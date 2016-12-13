using System.ComponentModel.DataAnnotations;

namespace Emojo.Lib
{
    public class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
