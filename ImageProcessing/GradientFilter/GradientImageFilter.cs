using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessing.Model;

namespace ImageProcessing.GradientFilter
{
    internal class GradientImageFilter : IImageFilter
    {
        private GradientImageFilterSetting _setting = new GradientImageFilterSetting();

        private static readonly int[,] SobelXMatrix = {
            { -1, 0, +1 },
            { -2, 0, +2 },
            { -1, 0, +1 }
        };

        private static readonly int[,] SobelYMatrix = {
            {-1, -2, -1},
            { 0,  0,  0},
            {+1, +2, +1}
        };

        private static readonly int[,] PrewittXMatrix = {
            { -1, 0, +1 },
            { -1, 0, +1},
            { -1, 0, +1 }
        };

        private static readonly int[,] PrewittYMatrix = {
            {-1, -1, -1},
            { 0,  0,  0},
            {+1, +1, +1}
        };

        private static readonly int[,] SobelFeldmanXMatrix = {
            { +3,  0, -3  },
            { +10, 0, -10 },
            { +3,  0, -3  }
        };

        private static readonly int[,] SobelFeldmanYMatrix = {
            {+3, +10, +3},
            { 0,   0,  0},
            {-3, -10, -3}
        };

        public string FilterDescription()
        {
            return "Gradient filter";
        }

        public string FilterId()
        {
            return "gradient";
        }

        public bool ApplySettings(IImageFilterSetting setting)
        {
            var result = setting is GradientImageFilterSetting;
            if (result)
            {
                _setting = (GradientImageFilterSetting)setting;
            }
            return result;
        }

        private static unsafe byte[] ToGraysaleByteArray(Image sourceImage)
        {
            var sourceBitmap = new Bitmap(sourceImage);
            var bitmapDataSource =
                    sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                        ImageLockMode.ReadOnly, sourceBitmap.PixelFormat);
            var sourceImageData = bitmapDataSource.Scan0;
            var grayscaleWidth = sourceImage.Width + 2;
            var grayscaleHeigth = sourceImage.Height + 2;
            var grayscaleData = new byte[grayscaleHeigth * grayscaleWidth];

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

            for (var y = 0; y < bitmapDataSource.Height; ++y)
            {
                var scanline = (byte*) sourceImageData;
                scanline += bitmapDataSource.Stride * y;
                // пропускаем строку сверху и первый ряд
                var grayscaleOffset = (y + 1) * grayscaleWidth + 1;

                for (var x = 0; x < bitmapDataSource.Width; ++x)
                {
                    var grayValue = (byte)((*(scanline + rOffset) + *(scanline + gOffset) + *(scanline + bOffset)) / 3);
                    grayscaleData[grayscaleOffset] = grayValue;
                    ++grayscaleOffset;
                    scanline += 4;
                }
            }

            // исходник больше нам не нужен
            sourceBitmap.UnlockBits(bitmapDataSource);

            // с второй строки
            var grayFromOffset = grayscaleWidth;
            // на первую
            var grayToOffset = 0;

            // c предпоследней строки 
            var grayFromOffset2 = grayscaleWidth * (grayscaleHeigth - 2);
            // на последнюю
            var grayToOffset2 = grayscaleWidth * (grayscaleHeigth - 1);

            for (var x = 0; x < grayscaleWidth; x++)
            {
                grayscaleData[grayToOffset++] = grayscaleData[grayFromOffset++];
                grayscaleData[grayToOffset2++] = grayscaleData[grayFromOffset2++];
            }

            // со второго ряда
            grayFromOffset = 1;
            // на первый
            grayToOffset = 0;

            // c пердпоследнего
            grayFromOffset2 = grayscaleWidth - 2;
            // на последний
            grayToOffset2 = grayscaleWidth - 1;

            for (var y = 0; y < grayscaleHeigth; ++y)
            {
                grayscaleData[grayToOffset] = grayscaleData[grayFromOffset];
                grayscaleData[grayToOffset2] = grayscaleData[grayFromOffset2];
                grayFromOffset += grayscaleWidth;
                grayFromOffset2 += grayscaleWidth;
                grayToOffset += grayscaleWidth;
                grayToOffset2 += grayscaleWidth;
            }

            return grayscaleData;
        }
        
        public Image ProcessImage(Image sourceImage)
        {
            if (sourceImage.PixelFormat != PixelFormat.Format32bppArgb)
            {
                throw new FormatException("only supported format is PixelFormat.Format32bppArgb");
            }
            var sourceBitmap = new Bitmap(sourceImage);
            var result = new Bitmap(sourceImage.Width, sourceImage.Height, sourceBitmap.PixelFormat);

            // для гранчных пикселей добваляем к ширине и высоте +1 справа и слева
            var grayscaleWidth = sourceImage.Width + 2;
            var grayscaleHeigth = sourceImage.Height + 2;
            
            unsafe
            {
                // переводим изображение в оттенки серого (average method)
                var grayscaleData = ToGraysaleByteArray(sourceImage);

                // handle byte order
                var bOffset = 3;
                var gOffset = 2;
                var rOffset = 1;
                var aOffset = 0;
                if (BitConverter.IsLittleEndian)
                {
                    bOffset = 0;
                    gOffset = 1;
                    rOffset = 2;
                    aOffset = 3;
                }

                int[,] xMatrix;
                int[,] yMatrix;

                switch (_setting.Operator)
                {
                    case GradientImageFilterSetting.GradientOperator.Sobel:
                        xMatrix = SobelXMatrix;
                        yMatrix = SobelYMatrix;
                        break;
                    case GradientImageFilterSetting.GradientOperator.Prewitt:
                        xMatrix = PrewittXMatrix;
                        yMatrix = PrewittYMatrix;
                        break;
                    case GradientImageFilterSetting.GradientOperator.SobelFeldman:
                        xMatrix = SobelFeldmanXMatrix;
                        yMatrix = SobelFeldmanYMatrix;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var bitmapDataResult = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.ReadWrite, result.PixelFormat);
                var resultImageData = bitmapDataResult.Scan0;
                var m = 0;

                
                for (var y = 1; y < (grayscaleHeigth - 1); ++y)
                {
                    var scanline = (byte*)resultImageData;
                    scanline += (y - 1) * bitmapDataResult.Stride;
                    for (var x = 1; x < (grayscaleWidth - 1); ++x)
                    {
                        float gy = 0;
                        float gx = 0;
                        // применяем оператор
                        for (var matX = 0; matX < 3; ++matX)
                        {
                            for (var matY = 0; matY < 3; matY++)
                            {
                                var currentX = x + matX - 1;
                                var currentY = y + matY - 1;
                                var currentGrayVal = grayscaleData[(currentY*grayscaleWidth) + currentX];
                                gy += currentGrayVal*xMatrix[matX, matY];
                                gx += currentGrayVal*yMatrix[matX, matY];
                            }
                        }

                        var res = (int) Math.Sqrt(gx * gx + gy * gy);
                        m = Math.Max(res, m);
                        
                        *(int*)scanline = res;
                        // next pixel
                        scanline += 4;
                    }
                }

                for (var y = 1; y < (grayscaleHeigth - 1); ++y)
                {
                    var scanline = (byte*) resultImageData;
                    scanline += (y - 1) * bitmapDataResult.Stride;
                    for (var x = 1; x < (grayscaleWidth - 1); ++x)
                    {
                        var res = (*(int*) scanline) / (m / 255);
                        *(scanline + rOffset) = (byte)res;
                        *(scanline + gOffset) = (byte)res;
                        *(scanline + bOffset) = (byte)res;
                        *(scanline + aOffset) = 0xff;
                        scanline += 4;
                    }
                }


                result.UnlockBits(bitmapDataResult);
                Debug.WriteLine(m);
            }
            return result;
        }
    }
}
