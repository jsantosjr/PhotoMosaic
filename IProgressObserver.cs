using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMosaic
{
    public interface IProgressObserver
    {
        void OnFinalProgressStep(string description);
        void OnInitialProgressStep(string description);
        void OnProgressStep(string description, int stepNumber, int totalSteps);
    }
}
