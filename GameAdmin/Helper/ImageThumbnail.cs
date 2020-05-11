using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace GameAdmin.Helper
{
    public static class ImageThumbnail
    {
        public static void DoThumbnail(string fileName, string newsThumbnailImage, Stream stream)
        {
            var path = Path.Combine(fileName, newsThumbnailImage);


            Image image = Image.FromStream(stream, true);

            int imgWidth = 110;
            int imgHeight = 95;

            Image thumb = image.GetThumbnailImage(imgWidth, imgHeight, () => false, IntPtr.Zero);
            thumb.Save(path);



        }
    }
}