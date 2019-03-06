using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Forms;

namespace ProjetDevMobile.Utils
{
    class ImageUtils
    {

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn.Length > 0)
            {
                Image image = new Image();
                image.Source = ImageSource.FromStream(() => new MemoryStream(byteArrayIn));
                return image;
            }else{
                return null;
            }
        }
    }
    
}
