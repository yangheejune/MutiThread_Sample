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
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }

        public LoginPerformanceViewModel LoginPerformanceVm { get; set; }
        public DiscoveryViewDodel DiscoveryVm { get; set; }

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
            DiscoveryVm = new DiscoveryViewDodel();
            CurrentView = LoginPerformanceVm;

            HomeViewCommand = new RelayCommand( o => {
                CurrentView = LoginPerformanceVm;
            });

            DiscoveryViewCommand = new RelayCommand( o => {
                CurrentView = DiscoveryVm;
            });
        }
    }
}
