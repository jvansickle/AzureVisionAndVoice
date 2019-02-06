using System.Collections.Generic;
using System.Linq;
using AzureVisionAndVoice.Models;
using AzureVisionAndVoice.Repositories;

namespace AzureVisionAndVoice.ViewModels
{
    public class AnalysisResultsListViewModel : ViewModel
    {
        List<ImageTagEntity> _analysisResults;
        public List<ImageTagEntity> AnalysisResults
        {
            get => _analysisResults;
            set
            {
                _analysisResults = value;
                NotifyPropertyChanged();
            }
        }

        public AnalysisResultsListViewModel()
        {
            AnalysisResults = ImageTagsRepository.GetAllImageTags().ToList();
        }
    }
}
