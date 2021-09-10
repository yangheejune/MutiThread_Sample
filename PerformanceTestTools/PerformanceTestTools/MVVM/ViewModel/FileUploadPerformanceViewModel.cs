using System.ComponentModel;

namespace PerformanceTestTools.MVVM.ViewModel
{
    class FileUploadPerformanceViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private string _userID;
        public string userID
        {
            get { return this._userID; }
            set { this._userID = value; }
        }
        private string _userPW;
        public string userPW
        {
            get { return this._userPW; }
            set { this._userPW = value; }
        }

        private string _filePath;
        public string filePath
        {
            get { return this._filePath; }
            set
            {
                this._filePath = value;
                this.RaisePropertyChanged(nameof(filePath));
            }
        }

        private int _SuccessCount;
        public int SuccessCount
        {
            get { return this._SuccessCount; }
            set
            {
                this._SuccessCount = value;
                this.RaisePropertyChanged(nameof(SuccessCount));
            }
        }

        private int _FailCount;
        public int FailCount
        {
            get { return this._FailCount; }
            set
            {
                this._FailCount = value;
                this.RaisePropertyChanged(nameof(FailCount));
            }
        }
    }
}
