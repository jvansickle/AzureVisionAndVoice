using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AzureVisionAndVoice.ViewModels
{
    abstract public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]string callerName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callerName));
        }
    }
}
