using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var c = new Context())
            {
                c.Users.ToList();
                c.Photos.ToList();
            }
        }
    }
}
