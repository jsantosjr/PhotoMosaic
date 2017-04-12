using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMosaic
{
    public interface IProgressNotifier
    {
        bool AddObserver(IProgressObserver observer);
        bool RemoveObserver(IProgressObserver observer);
        void NotifyInitialProgressStep(string description);
        void NotifyProgressStep(string description, int stepNumber, int totalSteps);
        void NotifyFinalProgressStep(string description);
    }
}
