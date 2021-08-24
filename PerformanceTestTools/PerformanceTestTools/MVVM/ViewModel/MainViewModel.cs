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

        public HomeViewModel HomeVm { get; set; }
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
            HomeVm = new HomeViewModel();
            DiscoveryVm = new DiscoveryViewDodel();
            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand( o => {
                CurrentView = HomeVm;
            });

            DiscoveryViewCommand = new RelayCommand( o => {
                CurrentView = DiscoveryVm;
            });
        }
    }
}
