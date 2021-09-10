using log4net;
using PerformanceTestTools.MVVM.ViewModel;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PerformanceTestTools.MVVM.View
{
    /// <summary>
    /// DiscoveryView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FileUploadPerformanceView : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        FileUploadPerformanceViewModel FileUploadPerformaceViewModel = new FileUploadPerformanceViewModel();

        static Object monitorLock = new System.Object();
        private static readonly int IV_LENGTH = 16;
        private readonly static char[] key = { 'J', 'i', 'r', 'a', 'n', 'S', 'e', 'c', 'u', 'r', 'i', 't', 'y', '!', 'N', 'e', 'w', 'T', 'e', 'c', 'h', '@', 'M', 'a', 's', 't', 'e', 'r', 'K', 'e', 'y', '#' };

        public FileUploadPerformanceView()
        {
            this.DataContext = FileUploadPerformaceViewModel;
            InitializeComponent();
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Logview_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FileSelect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ThreadCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
