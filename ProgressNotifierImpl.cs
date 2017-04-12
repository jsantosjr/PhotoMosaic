using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMosaic
{
    public class ProgressNotifierImpl : IProgressNotifier
    {
        #region Fields
        private List<IProgressObserver> _observers;
        #endregion

        #region Constructor
        public ProgressNotifierImpl()
        {
            _observers = new List<IProgressObserver>();
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Adds an observer to this ProgressNotifierImpl. It is not added if the observer has already been added.
        /// </summary>
        /// <param name="observer">The observer to add.</param>
        /// <returns>A value of true if the observer is successfully added and false otherwise.</returns>
        public bool AddObserver(IProgressObserver observer)
        {
            bool retVal = false;
            if (!Exists(observer))
            {
                _observers.Add(observer);
                retVal = true;
            }
            return retVal;
        }

        /// <summary>
        /// Returns a value of true if the passed in observer exists in the stored collection of IProgressObserver's and false otherwise.
        /// </summary>
        /// <param name="observer">The observer to search for.</param>
        /// <returns>A value of true if the passed in observer exists in the stored collection of IProgressObserver's and false otherwise.</returns>
        private bool Exists(IProgressObserver observer)
        {
            bool retVal = false;
            if (observer != null)
            {
                foreach (IProgressObserver existingObserver in _observers)
                {
                    if (existingObserver == observer)
                    {
                        retVal = true;
                        break;
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// Notifies all observers of of the final progress step.
        /// <param name="description">A description of the final progress step.</param>
        /// </summary>
        public void NotifyFinalProgressStep(string description)
        {
            foreach (IProgressObserver observer in _observers)
            {
                observer.OnFinalProgressStep(description);
            }
        }

        /// <summary>
        /// Notifies all observers of an initial progress step.
        /// <param name="description">A description of the initial progress step.</param>
        /// </summary>
        public void NotifyInitialProgressStep(string description)
        {
            foreach (IProgressObserver observer in _observers)
            {
                observer.OnInitialProgressStep(description);
            }
        }

        /// <summary>
        /// Notifies all observers of a progress step.
        /// <param name="description">A description of the progress step.</param>
        /// <param name="stepNumber">The progress step number.</param>
        /// <param name="totalSteps">The total number of progress steps.</param>
        /// </summary>
        public void NotifyProgressStep(string description, int stepNumber, int totalSteps)
        {
            foreach (IProgressObserver observer in _observers)
            {
                observer.OnProgressStep(description, stepNumber, totalSteps);
            }
        }

        /// <summary>
        /// Removes a specific observer from the stored collection of IProgressObserver's.
        /// </summary>
        /// <param name="observer">The observer to remove.</param>
        /// <returns>A value of true if the observer is successfully removed and false otherwise.</returns>
        public bool RemoveObserver(IProgressObserver observer)
        {
            bool retVal = false;
            if (Exists(observer))
            {
                _observers.Remove(observer);
                retVal = true;
            }
            return retVal;
        }
        #endregion
    }
}
