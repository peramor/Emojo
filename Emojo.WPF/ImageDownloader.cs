using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Emojo.WPF
{
    
        class ImageDownloader
        {
            public async Task<BitmapImage> DownloadImageTaskAsync(string fileUrl)
            {
                using (HttpClient client = new HttpClient())
                {
                    byte[] data = await client.GetByteArrayAsync(fileUrl);
                    return ConvertBytesToImage(data);
                }
            }
            public BitmapImage ConvertBytesToImage(byte[] data)
            {
                MemoryStream stream = new MemoryStream(data);
                stream.Seek(0, SeekOrigin.Begin);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }
    
}
