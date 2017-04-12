using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoMosaic
{
    public partial class ImageForm : Form
    {
        public ImageForm()
        {
            InitializeComponent();
        }

        public void SetImage(Image image)
        {
            if (image != null)
            {
                ClientSize = new Size(image.Width, image.Height);
                _imageArea.Height = image.Height;
                _imageArea.Width = image.Width;
                _imageArea.Image = image;
            }
        }
    }
}
