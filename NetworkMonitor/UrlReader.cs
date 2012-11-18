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

namespace NetworkMonitor
{
    public class UrlReader
    {
         WebClient webClient = new WebClient();

        // event declaration 
        public event EventHandler ReadFinished;

        public bool IsSuccess { get; set; }
        public string Text { get; set; }
        public string StatusText { get; set; }
        public string ErrorMessage { get; set; }

        public UrlReader()
        {
          
        }

        public void Read(string uri)
        {
            if (Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
                webClient.DownloadStringAsync(new Uri(uri));
            }
            else
            {
                StatusText = "Bad URL";

                if (ReadFinished != null)
                    ReadFinished(this, null);
            }
        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    IsSuccess = true;

                    StatusText = "OK";

                }
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                StatusText = "ERROR";
                ErrorMessage = ex.Message;
            }

            if (ReadFinished != null)
                ReadFinished(this, null);
        }
    }
}
