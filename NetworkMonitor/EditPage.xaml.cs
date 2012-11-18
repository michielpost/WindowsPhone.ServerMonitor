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
using NetworkMonitor.Model;
using System.Windows.Navigation;
using Microsoft.Phone.Marketplace;
using Microsoft.Phone.Tasks;
using mpost.WP7.Client.Effects;

namespace NetworkMonitor
{
    public partial class EditPage : PhoneApplicationPage
    {
        public EditPage()
        {
            InitializeComponent();

        }

        
        public bool IsNew { get; set; }

        private Server _server;

        // When page is navigated to, set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                int index = int.Parse(selectedIndex);
                _server = App.ServerCollection[index];
                this.DataContext = _server;

            }
            else
            {
                if (App.ServerCollection.Count > 0 && App.IsTrial)
                {
                    ContentGrid.Visibility = System.Windows.Visibility.Collapsed;
                    TrialGrid.Visibility = System.Windows.Visibility.Visible;
                    PageTitle.Text = "full version";                   
                }
                else
                {
                    _server = new Server();
                    _server.Uri = "http://";
                    this.DataContext = _server;

                    IsNew = true;
                    this.PageTitle.Text = "new";                

                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (Uri.IsWellFormedUriString(_server.Uri, UriKind.Absolute) && !string.IsNullOrEmpty(_server.DisplayName))
            {
                if (IsNew && _server != null)
                    App.ServerCollection.Add(_server);

                App.ServerCollection.Save();

                if (NavigationService.CanGoBack)
                    NavigationService.GoBack();
                else
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }

        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceDetailTask detailTask = new MarketplaceDetailTask();
            detailTask.Show();
        }

        private void appbar_deleteButton_Click(object sender, EventArgs e)
        {
            App.ServerCollection.Remove(_server);
            App.ServerCollection.Save();

            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
            else                    
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }


        private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UrlTextBox.Focus();
                UrlTextBox.SelectionStart = UrlTextBox.Text.Length; 

            }
        }

        private void UrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveButton.Focus();
            }
        }
    }
}