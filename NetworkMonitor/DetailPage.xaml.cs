using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Windows.Data;
using NetworkMonitor.Model;

namespace NetworkMonitor
{
    public partial class DetailPage : PhoneApplicationPage
    {
        

        // Constructor
        public DetailPage()
        {
            InitializeComponent();                        
        }

        private Server _server;

        // When page is navigated to, set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                int index = int.Parse(selectedIndex);

                if (App.ServerCollection.Count > index)
                {
                    _server = App.ServerCollection[index];
                    this.DataContext = _server;
                }
                else
                {
                    if (NavigationService.CanGoBack)
                        NavigationService.GoBack();
                    else
                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
               
            }
        }

        private void appbar_refreshButton_Click(object sender, EventArgs e)
        {
            _server.GetStatus();
        }

        private void appbar_editButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/EditPage.xaml?selectedItem=" + App.ServerCollection.IndexOf(_server), UriKind.Relative));
        }
    }
}