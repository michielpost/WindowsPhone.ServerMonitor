using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace NetworkMonitor.Model
{
    [DataContract]
    public class Server : INotifyPropertyChanged
    {
       
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public int Order { get; set; }

        public bool IsSuccess { get; set; }

        private string _statusText;
        private string _errorMessage;
        private string _uri;
        private string _displayName;

        public string StatusText
        {
            get { 
                return _statusText; }
            set
            {
                _statusText = value;
                NotifyPropertyChanged("StatusText");
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        [DataMember]
        public string Uri
        {
            get { return _uri; }
            set
            {
                _uri = value;
                NotifyPropertyChanged("Uri");
            }
        }

        [DataMember]
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                NotifyPropertyChanged("DisplayName");
            }
        }

     

        UrlReader reader;

        public void GetStatus()
        {
            this.StatusText = "Loading...";

            reader = new UrlReader();
            reader.ReadFinished += new EventHandler(reader_ReadFinished);
            reader.Read(Uri);
        }

        void reader_ReadFinished(object sender, EventArgs e)
        {
            IsSuccess = reader.IsSuccess;
            StatusText = reader.StatusText;
            ErrorMessage = reader.ErrorMessage;
        }

        #region INotifyPropertyChanged Members

        private void NotifyPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
