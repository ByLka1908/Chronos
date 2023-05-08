using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ChronosBeta.BL
{
    public static class FunctionsImage
    {
        public static BitmapImage GetImage(string path = null)
        {
            if (path == null)
                return new BitmapImage(new Uri("pack://application:,,/Images/no_image.png"));

            return new BitmapImage(new Uri(path));
        }

        public static BitmapImage SetImage()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return GetImage(openFileDialog.FileName);
                }
                return null;
            }
        }

        public static byte[] PushImage(ImageSource ImageUser)
        {
            var image = (BitmapImage)ImageUser;

            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static BitmapImage ByteToBitmapImage(byte[] ImageUser)
        {
            var memoryStream = new MemoryStream(ImageUser);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            bitmapImage.Freeze();

            return bitmapImage;
        }
    }
}