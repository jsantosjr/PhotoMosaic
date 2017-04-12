using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace PhotoMosaic
{
    public partial class MainForm : Form, IProgressObserver
    {
        #region Fields
        private ImageManager _imageManager;
        private readonly string _imagePath = "C:\\Users\\Jose\\Desktop\\PhotoMosaic\\Images\\Source\\Picasso.png";
        private readonly string _mosaicPath = "E:\\Jose\\Pictures\\Pet Pictures";
        private delegate void HandleInitialProgressStepDelegate(string description);
        private delegate void HandleProgressStepDelegate(string imagePath, int imageOrder, int totalImageCount);
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            _imageManager = new ImageManager();
            _imageManager.AddObserver(this);
            LoadImage(_imagePath);
        }
        #endregion

        #region IProgressObserver Methods
        /// <summary>
        /// This method is called when a progress step occurs.
        /// </summary>
        /// <param name="description">A description of the progress step.</param>
        /// <param name="stepNumber">The progress step number.</param>
        /// <param name="totalSteps">The total number of progress steps.</param>
        public void OnProgressStep(string description, int stepNumber, int totalSteps)
        {
            HandleProgressStepDelegate method = new HandleProgressStepDelegate(OnHandleProgressStep);
            Invoke(method, new object[] { description, stepNumber, totalSteps });
        }

        /// <summary>
        /// This method is called when an initial progress step occurs.
        /// </summary>
        /// <param name="description">A description of the progress step.</param>
        public void OnInitialProgressStep(string description)
        {
            HandleInitialProgressStepDelegate method = new HandleInitialProgressStepDelegate(OnHandleInitialProgressStep);
            Invoke(method, new object[] { description });
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Creates an OpenFileDialog that is used to select a single image file from the disk.
        /// </summary>
        /// <returns>An OpenFileDialog instance.</returns>
        private OpenFileDialog CreateOpenFileDialog()
        {
            string filter = string.Empty;
            string[,] allowedFilters = {{"JPEG Files", "*.JPG;*.JPEG"},
                                        {"BMP Files",  "*.BMP"},
                                        {"GIF Files",  "*.GIF"},
                                        {"TIFF Files", "*.TIF;*.TIFF"},
                                        {"PNG Files",  "*.PNG"}};
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            fileDialog.Multiselect = false;
            for (int i = 0; i < allowedFilters.GetLength(0); i++)
            {
                string filterName = allowedFilters[i, 0];
                string filterType = allowedFilters[i, 1];
                string seperator = (i > 0) ? "|" : string.Empty;
                string typeSeperator = (i > 0) ? ";" : string.Empty;
                filter += string.Format("{0}{1} ({2})|{2}", seperator, filterName, filterType);
            }
            fileDialog.Filter = filter;
            return fileDialog;
        }
        
        /// <summary>
        /// Loads the specified image.
        /// </summary>
        /// <param name="imagePath">The path location of the image to load.</param>
        private bool LoadImage(string imagePath)
        {
            bool loaded = false;
            if (_imageManager.LoadImage(imagePath))
            {
                _imageNameTxt.Text = Path.GetFileName(imagePath);
                _imagePathTxt.Text = imagePath;
                loaded = true;
            }
            return loaded;
        }

        /// <summary>
        /// This method is called when the "Load Image" button is clicked. It takes care of displaying an
        /// open file dialog, allowing the user to select an existing image file from the disk.
        /// </summary>
        /// <param name="sender">The sender of the click event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnLoadImage(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = CreateOpenFileDialog();
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string imagePath = fileDialog.FileName;
                if (!LoadImage(imagePath))
                {
                    MessageBox.Show(string.Format(Properties.Resources.FileNotLoadedMsg, imagePath));
                }
            }
        }

        /// <summary>
        /// This method is called when the "Display Image" button is clicked. It takes care of displaying
        /// the image that's currently loaded.
        /// </summary>
        /// <param name="sender">The sender of the click event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnDisplayImage(object sender, EventArgs e)
        {
            if (_imageManager.Image != null)
            {
                try
                {
                    ImageForm imageForm = new ImageForm();
                    imageForm.SetImage(_imageManager.Image);
                    if (imageForm.ShowDialog(this) == DialogResult.Cancel)
                    {
                    }
                    imageForm.Close();
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// This method is called when an initial progress step occurs.
        /// </summary>
        /// <param name="description">A description of the progress step.</param>
        public void OnHandleInitialProgressStep(string description)
        {
            _progressBarDescription.Visible = true;
            _progressBarPercent.Visible = true;
            _progressBar.Visible = true;
            _progressBar.Value = 0;
            _progressBarDescription.Text = (description != null) ? description : string.Empty;
            _progressBarPercent.Text = string.Format("{0}%", 0);
        }

        /// <summary>
        /// This method is called when a progress step occurs.
        /// </summary>
        /// <param name="description">A description of the progress step.</param>
        /// <param name="stepNumber">The progress step number.</param>
        /// <param name="totalSteps">The total number of progress steps.</param>
        public void OnHandleProgressStep(string description, int stepNumber, int totalSteps)
        {
            if (stepNumber > totalSteps)
                stepNumber = totalSteps;

            int progressValue = (totalSteps == 0) ? 100 : (int)(((double)stepNumber / (double)totalSteps) * 100);
            _progressBar.Value = progressValue;
            _progressBarPercent.Text = string.Format("{0}%", progressValue);
        }

        /// <summary>
        /// This method is called when the "Process Image" button is clicked. It takes care of processing the
        /// loaded image and turning it into a mosaic image.
        /// </summary>
        /// <param name="sender">The sender of the click event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnProcessImage(object sender, EventArgs e)
        {
            if (_imageManager.Image != null)
            {
                _imageManager.Run(_mosaicPath);
            }
        }
        #endregion
    }
}
