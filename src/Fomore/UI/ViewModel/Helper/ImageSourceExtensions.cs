using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Fomore.UI.ViewModel.Helper
{
    public static class ImageSourceExtensions
    {
        private static Dictionary<BitmapSource, Cursor> Cached { get; } = new Dictionary<BitmapSource, Cursor>();

        private static bool IsEqual(this BitmapSource bitmapSource, BitmapSource bitmapSource2)
        {
            if (bitmapSource == null || bitmapSource2 == null)
            {
                return false;
            }
            return bitmapSource.ToBytes().SequenceEqual(bitmapSource2.ToBytes());
        }

        private static byte[] ToBytes(this BitmapSource image)
        {
            byte[] data = { };
            if (image == null) return data;
            var encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        private static Cursor TryGetCachedValue(BitmapSource bitmapSource)
        {
            if (Cached.Any(kvp => kvp.Key.IsEqual(bitmapSource)))
                return Cached.First(kvp => kvp.Key.IsEqual(bitmapSource)).Value;
            return null;
        }

        public static Cursor CreateCursor(this BitmapSource bitmapSource, int hotspotX, int hotspotY, bool loadCached = true)
        {
            {
                var cursor = TryGetCachedValue(bitmapSource);
                if (cursor != null)
                    return cursor;
            }
            using (var ms1 = new MemoryStream())
            {
                var pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                pngEncoder.Save(ms1);

                var pngBytes = ms1.ToArray();
                int size = pngBytes.GetLength(0);

                using (var ms = new MemoryStream())
                {
                    //Reserved must be zero; 2 bytes
                    ms.Write(BitConverter.GetBytes((short)0), 0, 2);

                    //image type 1 = ico 2 = cur; 2 bytes
                    ms.Write(BitConverter.GetBytes((short)2), 0, 2);

                    //number of images; 2 bytes
                    ms.Write(BitConverter.GetBytes((short)1), 0, 2);

                    //image width in pixels
                    ms.WriteByte(32);

                    //image height in pixels
                    ms.WriteByte(32);

                    //Number of Colors in the color palette. Should be 0 if the image doesn't use a color palette
                    ms.WriteByte(0);

                    //reserved must be 0
                    ms.WriteByte(0);

                    //2 bytes. In CUR format: Specifies the horizontal coordinates of the hotspot in number of pixels from the left.
                    ms.Write(BitConverter.GetBytes((short)hotspotX), 0, 2);
                    //2 bytes. In CUR format: Specifies the vertical coordinates of the hotspot in number of pixels from the top.
                    ms.Write(BitConverter.GetBytes((short)hotspotY), 0, 2);

                    //Specifies the size of the image's data in bytes
                    ms.Write(BitConverter.GetBytes(size), 0, 4);

                    //Specifies the offset of BMP or PNG data from the beginning of the ICO/CUR file
                    ms.Write(BitConverter.GetBytes(22), 0, 4);

                    ms.Write(pngBytes, 0, size); //write the png data.
                    ms.Seek(0, SeekOrigin.Begin);
                    var cursor = new Cursor(ms);
                    Cached.Add(bitmapSource, cursor);
                    return cursor;
                }
            }
        }
    }
}