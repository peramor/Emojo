using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib
{
    public class User
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        [Required]
        public string Username { get; set; }
        public string FullName { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
