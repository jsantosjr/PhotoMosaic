using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Linq;

namespace PhotoMosaic
{
    public class ProcessParameters
    {
        #region Properties
        /// <summary>
        /// Gets and sets the bool value that determines whether or not colors should be adjusted.
        /// </summary>
        public bool AdjustColors { get; set; }

        /// <summary>
        /// Gets and sets the path of an image.
        /// </summary>
        public string ImagePath { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a ProcessParameters instance.
        /// </summary>
        /// <param name="adjustColors">A value that determines whether or not colors should be adjusted.</param>
        /// <param name="imagePath">The path of an image.</param>
        public ProcessParameters(bool adjustColors, string imagePath)
        {
            AdjustColors = adjustColors;
            ImagePath = imagePath;
        }
        #endregion
    }

    public class ImageManager : IProgressNotifier
    {
        #region Fields
        private List<ImageProperty> _mosaicProperties;
        private ProgressNotifierImpl _notifierImpl;
        private Thread _processingThread;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the aspect ratio of the loaded image.
        /// </summary>
        public double AspectRatio 
        {
            get
            {
                double aspectRatio = 0;
                if (Image != null & Image.Height > 0)
                    aspectRatio = (double)Image.Width / Image.Height;
                return aspectRatio;
            }
        }

        /// <summary>
        /// Gets the loaded image.
        /// </summary>
        public Image Image { get; private set; }

        /// <summary>
        /// Gets the full path of the loaded image.
        /// </summary>
        public string ImagePath { get; private set; }

        /// <summary>
        /// Gets the directory path of the images that are used to compose the mosaic.
        /// </summary>
        public string MosaicPath { get; private set; } 
        #endregion

        #region IProgressNotifier Methods
        /// <summary>
        /// Adds an observer to this ProgressNotifierImpl. It is not added if the observer has already been added.
        /// </summary>
        /// <param name="observer">The observer to add.</param>
        /// <returns>A value of true if the observer is successfully added and false otherwise.</returns>
        public bool AddObserver(IProgressObserver observer)
        {
            return _notifierImpl.AddObserver(observer);
        }

        /// <summary>
        /// Notifies all observers of of the final progress step.
        /// <param name="description">A description of the final progress step.</param>
        /// </summary>
        public void NotifyFinalProgressStep(string description)
        {
            _notifierImpl.NotifyFinalProgressStep(description);
        }

        /// <summary>
        /// Notifies all observers of an initial progress step.
        /// <param name="description">A description of the progress step.</param>
        /// </summary>
        public void NotifyInitialProgressStep(string description)
        {
            _notifierImpl.NotifyInitialProgressStep(description);
        }

        /// <summary>
        /// Notifies all observers of a progress step.
        /// <param name="description">A description of the progress step.</param>
        /// <param name="stepNumber">The progress step number.</param>
        /// <param name="totalSteps">The total number of progress steps.</param>
        /// </summary>
        public void NotifyProgressStep(string description, int stepNumber, int totalSteps)
        {
            _notifierImpl.NotifyProgressStep(description, stepNumber, totalSteps);
        }

        /// <summary>
        /// Removes a specific observer from the stored collection of IProgressObserver's.
        /// </summary>
        /// <param name="observer">The observer to remove.</param>
        /// <returns>A value of true if the observer is successfully removed and false otherwise.</returns>
        public bool RemoveObserver(IProgressObserver observer)
        {
            return _notifierImpl.RemoveObserver(observer);
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates an ImageManager instance.
        /// </summary>
        public ImageManager()
        {
            _processingThread = new Thread(OnProcessingThreadStarted);
            _notifierImpl = new ProgressNotifierImpl();
            Image = null;
            ImagePath = String.Empty;
            MosaicPath = String.Empty;
            _mosaicProperties = new List<ImageProperty>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the ImageProperty corresponding to the image in the mosaic directory that most closely matches the specified brightness.
        /// </summary>
        /// <param name="brightness">The brightness of search.</param>
        /// <returns>The ImageProperty corresponding to the image in the mosaic directory that most closely matches the specified brightness.</returns>
        public ImageProperty FindMosaicImage(float brightness)
        {
            ImageProperty property = null;
            if (_mosaicProperties != null && brightness >= 0)
            {
                float delta = 0;
                float smallestDelta = 0;
                for (int i = 0; i < _mosaicProperties.Count; i++)
                {
                    ImageProperty existingProperty = _mosaicProperties[i];
                    delta = Math.Abs(existingProperty.AverageBrightness - brightness);
                    if (i == 0 || delta < smallestDelta)
                    {
                        smallestDelta = delta;
                        property = existingProperty;
                    }
                }
            }
            return property;
        }

        /// <summary>
        /// Attempts to load image at the passed in path location.
        /// </summary>
        /// <param name="imagePath">The path location of the image to load.</param>
        /// <param name="mosaicPath">The directory path of the images that are used to compose the mosaic.</param>
        /// <returns>A value of true if the specified image is successfully loaded and false otherwise.</returns>
        public bool LoadImage(string imagePath)
        {
            bool retVal = false;
            try
            {
                if (imagePath != null && File.Exists(imagePath))
                {
                    Image = Image.FromFile(imagePath);
                    ImagePath = imagePath;
                    retVal = true;
                }
            }
            catch (Exception)
            {
            }
            return retVal;
        }

        /// <summary>
        /// Loads the images in the stored mosaic path.
        /// </summary>
        /// <param name="mosaicPath">The directory path of the mosaic images.</param>
        /// <returns>A value of true if the images are successfully loaded and false, otherwise.</returns>
        public bool LoadMosaicImages(string mosaicPath)
        {
            bool retVal = false;
            List<ImageProperty> mosaicProperties = null;
            try
            {
                if (mosaicPath != null && Directory.Exists(mosaicPath))
                {
                    // Load the mosaic images in the passed in directory.
                    mosaicProperties = new List<ImageProperty>();
                    List<string> files = Directory.GetFiles(mosaicPath, "*", SearchOption.AllDirectories).Where(s => File.Exists(s) && ImageProperty.IsImage(s)).ToList();

                    // We'll want to notify observers of the number of total images to load.
                    string stepDescription = "Step 1/2: Loading Mosaic";
                    NotifyInitialProgressStep(stepDescription);
                    for (int i = 0; i < files.Count; i++)
                    {
                        string filePath = files[i];
                        ImageProperty imageProperty = new ImageProperty(filePath);
                        mosaicProperties.Add(imageProperty);

                        // We'll of course notify observers that the image has been loaded.
                        NotifyProgressStep(filePath, i + 1, files.Count);
                    }
                    _mosaicProperties = mosaicProperties;
                    MosaicPath = mosaicPath;
                    retVal = true;
                    NotifyFinalProgressStep(string.Format("{0} - Finished!", stepDescription));
                }
            }
            catch (Exception)
            {
                mosaicProperties = null;
            }
            return retVal;
        }

        /// <summary>
        /// Processes the loaded image and turns it into a mosaic composed of smaller images.
        /// </summary>
        /// <param name="mosaicPath">The directory path of the mosaic images.</param>
        public bool ProcessImage(bool adjustColors, string mosaicPath)
        {
            bool retVal = false;

            // Note that calling LoadMosaicImages may take a while to finish its execution since it'll be
            // loading and unloading images to and from memory. The time it takes will depend on the number
            // of images in the passed in directory.
            if (Image != null && LoadMosaicImages(mosaicPath))
            {
                // We'll resize our loaded image so that we don't have to process as many pixels. The image will be downsized to a
                // width of 50 pixels and its height will be re-adjusted so that it maintains its original aspect ratio.
                Image resizedImage = Image;
                int smallWidth = 50;
                int smallHeight = (Image.Height * smallWidth) / ((Image.Width > 0) ? Image.Width : 1);
                if (smallWidth < Image.Width || smallHeight < Image.Height)
                    resizedImage = ImageProperty.ResizeImage(Image, smallWidth, smallHeight);

                // We'll then iterate though the pixels of the image and for each pixel, find an image that best matches the pixel's brightness.
                string stepDescription = "Step 2/2: Building Mosaic";
                NotifyInitialProgressStep(stepDescription);
                Bitmap resizedBitmap = new Bitmap(resizedImage);
                int count = 0;
                int numPixels = resizedBitmap.Height * resizedBitmap.Width;
                int cellWidth = (smallWidth > 0) ? (Image.Width / smallWidth) : 1;
                int cellHeight = (smallHeight > 0) ? (Image.Height / smallHeight) : 1;
                Bitmap targetBitmap = new Bitmap(cellWidth * smallWidth, cellHeight * smallHeight);
                Dictionary<string, Tuple<int, int>> cachedImageCoords = new Dictionary<string, Tuple<int, int>>();
                for (int x = 0; x < resizedBitmap.Width; x++)
                {
                    for (int y = 0; y < resizedBitmap.Height; y++)
                    {
                        Color color = resizedBitmap.GetPixel(x, y);
                        ImageProperty property = FindMosaicImage(color.GetBrightness());
                        if (property != null)
                        {
                            using (Graphics graphics = Graphics.FromImage(targetBitmap))
                            {
                                Image smallImage;
                                int xPos = x * cellWidth;
                                int yPos = y * cellHeight;
                                if (cachedImageCoords.ContainsKey(property.Path))
                                {
                                    // Since we've already encountered the small image, instead of loading it from a file, we'll
                                    // simply retrieve a region from the destination mosaic image that contains the small image.
                                    int cachedXPos = cachedImageCoords[property.Path].Item1;
                                    int cachedYPos = cachedImageCoords[property.Path].Item2;
                                    Rectangle imageRect = new Rectangle(cachedXPos, cachedYPos, cellWidth, cellHeight);
                                    smallImage = targetBitmap.Clone(imageRect, targetBitmap.PixelFormat);
                                }
                                else
                                {
                                    smallImage = Image.FromFile(property.Path);
                                    cachedImageCoords[property.Path] = new Tuple<int, int>(xPos, yPos);
                                }

                                if (adjustColors)
                                    ImageProperty.RecolorImage((Bitmap)smallImage, color);
                                graphics.DrawImage(smallImage, xPos, yPos, cellWidth, cellHeight);
                                smallImage.Dispose();
                            }
                            NotifyProgressStep(property.Path, ++count, numPixels);
                        }
                    }
                }
                Image.Dispose();
                Image = targetBitmap;
                NotifyFinalProgressStep(string.Format("{0} - Finished!", stepDescription));
            }
            return retVal;
        }

        /// <summary>
        /// Resizes the loaded image by a scale value. A positive scale value will increase the width and height of
        /// the image while a negative scale value will decrease them. A copy of the resulting image is returned.
        /// </summary>
        /// <param name="scale">The scale value. A positive scale value will increase the width and height of the image while a negative value will decrease them.</param>
        /// <returns>The rescaled image or null if the rescale fails.</returns>
        private Image RescaleImage(int scale)
        {
            return ImageProperty.RescaleImage(Image, scale);
        }

        /// <summary>
        /// Attempts to resize the loaded image.
        /// </summary>
        /// <param name="width">The new width of the image.</param>
        /// <param name="height">The new height of the image.</param>
        /// <returns>The resized image or null if the resize fails.</returns>
        private Image ResizeImage(int width, int height)
        {
            return ImageProperty.ResizeImage(Image, width, height);
        }

        /// <summary>
        /// Triggers a worker thread that takes care of processing the loaded image.
        /// </summary>
        /// <param name="mosaicPath">The path of the mosaic images.</param>
        public void Run(bool adjustColors, string mosaicPath)
        {
            try
            {
                if (!_processingThread.IsAlive)
                {
                    ProcessParameters parameters = new ProcessParameters(adjustColors, mosaicPath);
                    _processingThread = new Thread(OnProcessingThreadStarted);
                    _processingThread.Start(parameters);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// This method is called when the processing thread is started. It takes care of processing the loaded image and creating a mosaic out of it.
        /// </summary>
        /// /// <param name="data">The directory path of the mosaic images.</param>
        private void OnProcessingThreadStarted(object data)
        {
            try
            {
                ProcessParameters parameters = (ProcessParameters)data;
                ProcessImage(parameters.AdjustColors, parameters.ImagePath);
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                string directoryPath = Path.GetDirectoryName(ImagePath);
                string fileName = string.Format("{0}_{1}.png", Path.GetFileNameWithoutExtension(ImagePath), unixTimestamp);
                string filePath = string.Format("{0}\\{1}", directoryPath, fileName);
                Image.Save(filePath, ImageFormat.Png);
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
