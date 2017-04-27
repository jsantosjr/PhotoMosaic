using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace PhotoMosaic
{
    public class ImageProperty
    {
        #region Properties
        /// <summary>
        /// Gets the average brightness of the stored image.
        /// </summary>
        public float AverageBrightness { get; private set; }

        /// <summary>
        /// Gets the full path of the stored image.
        /// </summary>
        public string Path { get; private set; }
        #endregion

        #region Constructor
        public ImageProperty(string imagePath)
        {
            Path = imagePath;
            AverageBrightness = GetAverageBrightness(imagePath);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adjusts the passed in color so that it assumes the passed in brightness.
        /// </summary>
        /// <param name="brightness">The new brightness of the passed in color.</param>
        /// <returns>The new color after it has been adjusted.</returns>
        public static Color GetAdjustedColor(Color color, float brightness)
        {
            Color adjustedColor = color;
            if (color != null && brightness >= 0.0 && brightness <= 1.0)
            {
                RGB rgbValues = new RGB((int)(color.R * brightness), (int)(color.G * brightness), (int)(color.B * brightness));
                adjustedColor = Color.FromArgb(rgbValues.Red, rgbValues.Green, rgbValues.Blue);
            }
            return adjustedColor;
        }

        /// <summary>
        /// Returns the average brightness of the specified image.
        /// </summary>
        /// <param name="image">The path of the image to check.</param>
        /// <returns>The average brightness of the specified image.</returns>
        public static float GetAverageBrightness(string imagePath)
        {
            float sum = 0;
            float average = 0;
            Image image = null;
            try
            {
                if (imagePath != null && File.Exists(imagePath))
                {
                    image = Image.FromFile(imagePath);

                    // We'll resize the image so that we don't have to process as many pixels. The image will be downsized to a
                    // width of 20 pixels and its height will be re-adjusted so that it maintains its original aspect ratio.
                    Image resizedImage = image;
                    int smallWidth = 20;
                    int smallHeight = (image.Height * smallWidth) / ((image.Width > 0) ? image.Width : 1);
                    if (smallWidth < image.Width || smallHeight < image.Height)
                        resizedImage = ResizeImage(image, smallWidth, smallHeight);

                    // We'll then iterate though the pixels of the image and calculate the average brightness of the pixels.
                    Bitmap resizedBitmap = new Bitmap(resizedImage);
                    for (int x = 0; x < resizedBitmap.Width; x++)
                    {
                        for (int y = 0; y < resizedBitmap.Height; y++)
                        {
                            Color color = resizedBitmap.GetPixel(x, y);
                            sum += color.GetBrightness();
                        }
                    }

                    int numPixels = resizedBitmap.Width * resizedBitmap.Height;
                    average = (numPixels > 0) ? (float)sum / (float)numPixels : 0;
                    if (average < 0)
                        average *= -1;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (image != null)
                    image.Dispose();
            }
            return average;
        }

        /// <summary>
        /// Returns a value of true if specified file corresponds to a valid image type.
        /// </summary>
        /// <param name="filePath">The full path of the file to check.</param>
        /// <returns>A value of true if the specified file corresponds to a valid image type.</returns>
        public static bool IsImage(string filePath)
        {
            bool retVal = false;
            string[] extensions = { ".jpg", ".jpeg", ".bmp", ".gif", ".tif", ".tiff", ".png" };
            if (filePath != null)
            {
                for (int i = 0; i < extensions.Length; i++)
                {
                    if (filePath.EndsWith(extensions[i], StringComparison.OrdinalIgnoreCase))
                    {
                        retVal = true;
                        break;
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// Recolors each pixel of the passed in image to the specified color.
        /// </summary>
        /// <param name="original">The image to recolor.</param>
        /// <param name="color">The new color of each image pixel.</param>
        /// <returns></returns>
        public static unsafe void RecolorImage(Bitmap original, Color color)
        {
            try
            {
                if (original != null)
                {
                    BitmapData imageData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    byte* firstScanLine = (byte*)imageData.Scan0.ToPointer();
                    int scanLineWidth = imageData.Stride;
                    int bitsPerPixel = 3;

                    for (int y = 0; y < imageData.Height; y++)
                    {
                        byte* currentScanLine = firstScanLine + (scanLineWidth * y);
                        for (int x = 0; x < imageData.Width; x++)
                        {
                            ////////////////////////////////////////////////////////////////////////////////////////////
                            // Note that each element in the current scan line contains 24 bits. This means that each
                            // segment of 8 bits will correspond to a red, green, or blue value. The 8 bit segments
                            // hold the following values,
                            //
                            //  - 1st 8 bits = blue value
                            //  - 2nd 8 bits = green value
                            //  - 3rd 8 bits = red value
                            ////////////////////////////////////////////////////////////////////////////////////////////
                            int blueIndex  = x * bitsPerPixel;
                            int greenIndex = blueIndex + 1;
                            int redIndex   = blueIndex + 2;
                            Color originalColor = Color.FromArgb(currentScanLine[redIndex], currentScanLine[greenIndex], currentScanLine[blueIndex]);
                            Color adjustedColor = GetAdjustedColor(color, originalColor.GetBrightness() + 0.2f);
                            currentScanLine[redIndex]   = adjustedColor.R;
                            currentScanLine[greenIndex] = adjustedColor.G;
                            currentScanLine[blueIndex]  = adjustedColor.B;
                        }
                    }
                    original.UnlockBits(imageData);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Resizes the passed in image by a scale value. A positive scale value will increase the width and height of
        /// the image while a negative scale value will decrease them. A copy of the resulting image is returned.
        /// </summary>
        /// <param name="scale">The scale value. A positive scale value will increase the width and height of the image while a negative value will decrease them.</param>
        /// <returns>The rescaled image or null if the rescale fails.</returns>
        public static Image RescaleImage(Image original, int scale)
        {
            Image destImage = null;
            if (original != null)
            {
                int width = original.Width;
                int height = original.Height;
                int i = width;
                int j = height;
                if (scale < 0)
                {
                    width /= -scale;
                    height /= -scale;
                }
                else
                {
                    width *= scale;
                    height *= scale;
                }
                destImage = ResizeImage(original, width, height);
            }
            return destImage;
        }

        /// <summary>
        /// Attempts to resize the passed in image.
        /// </summary>
        /// <param name="original">The image to resize.</param>
        /// <param name="width">The new width of the image.</param>
        /// <param name="height">The new height of the image.</param>
        /// <returns>The resized image or null if the resize fails.</returns>
        public static Image ResizeImage(Image original, int width, int height)
        {
            Bitmap destImage = null;
            if (original != null && width > 0 && height > 0)
            {
                Rectangle destRect = new Rectangle(0, 0, width, height);
                destImage = new Bitmap(width, height);
                destImage.SetResolution(original.HorizontalResolution, original.VerticalResolution);

                using (Graphics graphics = Graphics.FromImage(destImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (ImageAttributes attributes = new ImageAttributes())
                    {
                        attributes.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(original, destRect, 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                    }
                }
            }
            return destImage;
        }
        #endregion
    }
}
