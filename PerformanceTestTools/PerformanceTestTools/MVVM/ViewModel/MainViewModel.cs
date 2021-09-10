using PerformanceTestTools.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTestTools.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand LoginPerformanceViewCommand { get; set; }
        public RelayCommand FileUploadPerformanceViewCommand { get; set; }

        public LoginPerformanceViewModel LoginPerformanceVm { get; set; }
        public FileUploadPerformanceViewModel FileUploadPerformanceVm { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChange();
            }
        }
        public MainViewModel()
        {
            LoginPerformanceVm = new LoginPerformanceViewModel();
            FileUploadPerformanceVm = new FileUploadPerformanceViewModel();
            CurrentView = LoginPerformanceVm;

            LoginPerformanceViewCommand = new RelayCommand( o => {
                CurrentView = LoginPerformanceVm;
            });

            FileUploadPerformanceViewCommand = new RelayCommand( o => {
                CurrentView = FileUploadPerformanceVm;
            });
        }
    }
}
