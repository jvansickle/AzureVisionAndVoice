namespace AzureVisionAndVoice.ViewModels
{
    public class MainViewModel : ViewModel
    {
        bool _isAnalyzing;
        public bool Analyzing
        {
            get => _isAnalyzing;
            set
            {
                _isAnalyzing = value;
                NotifyPropertyChanged();
            }
        }
    }
}
