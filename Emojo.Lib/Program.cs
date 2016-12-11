using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Emojo.Lib
{
    class Program
    {
        static async void Main(string[] args)
        {
            InstagramGetter getter = new InstagramGetter();
            Process.Start(getter.GetAuthLink());
            HttpListener listener = new HttpListener();
            string prefix = "http://emojo.azurewebsites.net/";
            listener.Prefixes.Add(prefix);
            listener.Start();
            var context = await listener.GetContextAsync();
            Console.WriteLine(context.Request.Url);
        }
    }
}
