using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PerformanceTestTools.MVVM.View
{
    /// <summary>
    /// HomeView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginPerformanceView : UserControl
    {
        public LoginPerformanceView()
        {
            InitializeComponent();
        }

        private void ThreadCount_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ThreadCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ThreadCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ipno = 1;
            try
            {
                ipno = Convert.ToInt32(ThreadCount.Text);

                if (ipno > 2000)
                {
                    /// usercontrol은 부모 창에 패널로 붙어 있기 때문에 자신의 x,y 좌표를 가지고 있지 않다. 그렇기 때문에 화면의 중앙에 띄울수 있도록 함.
                    MessageBox.Show("스레드 개수는 최대 2000개 이하여야 합니다.", "Thread Error", MessageBoxButton.OK);
                    ThreadCount.Text = "2000";
                }
                else if (ipno < 1)
                {
                    MessageBox.Show("스레드 개수는 최소 1개 이어야 합니다.", "Thread Error", MessageBoxButton.OK);
                    ThreadCount.Text = "1";
                }
            }
            catch (FormatException)
            {
                ipno = 1;
                ThreadCount.Text = "";
            }
        }

        private void UpCount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownCount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ThreadStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            int threadcount = Int32.Parse(ThreadCount.Text);
            string UID = UserID.Text;
            string UPW = UserPW.Password;
            MessageBox.Show("로그인 정보 : ID : " + UID + ", 암호 : " + UPW + ", 스레드 개수 : " + threadcount , "로그인", MessageBoxButton.OK);

            // 스레드 갯수 만큼 스레드 제작

            // 스레드 하나 하나 마다 통신 모듈 붙이기 dll을 붙여야 할거같은데...

        }

        private void Logview_Click(object sender, RoutedEventArgs e)
        {
            string filePath = Directory.GetCurrentDirectory()+ "\\Log";
            Process.Start("explorer.exe", filePath);
        }
    }
}
