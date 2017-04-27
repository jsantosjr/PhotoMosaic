using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMosaic
{
    public class RGB
    {
        #region Fields
        private int _red;
        private int _green;
        private int _blue;
        #endregion

        #region Properties
        /// <summary>
        /// Gets and sets the red value.
        /// </summary>
        public int Red
        { 
            get
            {
                return _red;
            }
            set
            {
                if (value > 255)
                    _red = 255;
                else if (value < 0)
                    _red = 0;
                else
                    _red = value;
            }
        }

        /// <summary>
        /// Gets and sets the green color.
        /// </summary>
        public int Green
        {
            get
            {
                return _green;
            }
            set
            {
                if (value > 255)
                    _green = 255;
                else if (value < 0)
                    _green = 0;
                else
                    _green = value;
            }
        }

        /// <summary>
        /// Gets and sets the blue color.
        /// </summary>
        public int Blue
        {
            get
            {
                return _blue;
            }
            set
            {
                if (value > 255)
                    _blue = 255;
                else if (value < 0)
                    _blue = 0;
                else
                    _blue = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public RGB(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
        #endregion
    }
}
