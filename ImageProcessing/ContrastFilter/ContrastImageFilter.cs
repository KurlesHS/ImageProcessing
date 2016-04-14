using System;
using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessing.Model;

namespace ImageProcessing.ContrastFilter
{
    internal class ContrastImageFilter : IImageFilter
    {

        /*
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        private static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
        */

        private ContrastImageFilterSetting _setting = new ContrastImageFilterSetting { Threshold = 0 };
        public string FilterDescription()
        {
            return "Contrast filter";
        }

        public string FilterId()
        {
            return "contrast";
        }

        public bool ApplySettings(IImageFilterSetting setting)
        {
            var result = setting is ContrastImageFilterSetting;
            if (result)
            {
                _setting = (ContrastImageFilterSetting)setting;
            }
            return result;
        }

        private static unsafe void ConvertToGrayscale(Bitmap bitmap)
        {
            // handle byte order
            var bOffset = 3;
            var gOffset = 2;
            var rOffset = 1;
            if (BitConverter.IsLittleEndian)
            {
                bOffset = 0;
                gOffset = 1;
                rOffset = 2;
            }
            
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);

            var sourceImageData = bitmapData.Scan0;

            for (var y = 0; y < bitmapData.Height; ++y)
            {
                var scanline = (byte*)sourceImageData;
                scanline += bitmapData.Stride * y;
                // пропускаем строку сверху и первый ряд

                for (var x = 0; x < bitmapData.Width; ++x)
                {
                    var grayValue = (byte)((*(scanline + rOffset) + *(scanline + gOffset) + *(scanline + bOffset)) / 3);
                    scanline[rOffset] = grayValue;
                    scanline[gOffset] = grayValue;
                    scanline[bOffset] = grayValue;
                    scanline += 4;
                }
            }
            bitmap.UnlockBits(bitmapData);
        }

        public Image ProcessImage(Image sourceImage)
        {
            if (sourceImage.PixelFormat != PixelFormat.Format32bppArgb)
            {
                throw new FormatException("only supported format is PixelFormat.Format32bppArgb");
            }
            var sourceBitmap = new Bitmap(sourceImage);

            if (_setting.Grayscale)
            {
                ConvertToGrayscale(sourceBitmap);
            }
            
            var bitmapDataResult = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadWrite, sourceBitmap.PixelFormat);
            var resultImageData = bitmapDataResult.Scan0;

            // handle byte order
            var bOffset = 3;
            var gOffset = 2;
            var rOffset = 1;
            if (BitConverter.IsLittleEndian)
            {
                bOffset = 0;
                gOffset = 1;
                rOffset = 2;
            }

            var contrastLevel = Math.Pow((100.0 + _setting.Threshold) / 100.0, 2);
            
            unsafe
            {
                for (var y = 0; y < sourceBitmap.Height; ++y)
                {
                    var scanline = (byte*)resultImageData;
                    scanline += y * bitmapDataResult.Stride;
                    for (var x = 0; x < bitmapDataResult.Width; ++x)
                    {
                        int r = *(scanline + rOffset);
                        int g = *(scanline + gOffset);
                        int b = *(scanline + bOffset);

                        b = (int)(((b / 255.0 - 0.5) * contrastLevel + 0.5) * 255.0);
                        g = (int)(((g / 255.0 - 0.5) * contrastLevel + 0.5) * 255.0);
                        r = (int)(((r / 255.0 - 0.5) * contrastLevel + 0.5) * 255.0);

                        b = b > 255 ? 255 : b;
                        b = b < 0 ? 0 : b;

                        g = g > 255 ? 255 : g;
                        g = g < 0 ? 0 : g;

                        r = r > 255 ? 255 : r;
                        r = r < 0 ? 0 : r;

                        *(scanline + rOffset) = (byte)r;
                        *(scanline + gOffset) = (byte)g;
                        *(scanline + bOffset) = (byte)b;
                        // next pixel
                        scanline += 4;
                    }
                }
            }
            
            sourceBitmap.UnlockBits(bitmapDataResult);

            return sourceBitmap;
        }
    }
}
