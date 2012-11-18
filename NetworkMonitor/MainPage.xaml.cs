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

namespace NetworkMonitor
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //ServerCollection col = App.ServerCollection;
            //col.Add(new Server() { DisplayName = "Test", Order = 0, Uri = "http://www.google.com" });
            //col.Add(new Server() { DisplayName = "Test2", Order = 0, Uri = "http://www.aa" });

            //col.Save();
           
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ServerCollection serverCollection = App.ServerCollection;
            serverCollection.Load();

            MainListBox.ItemsSource = serverCollection;
            serverCollection.GetStatus();

            if (serverCollection.Count == 0)
            {
                AddText.Visibility = System.Windows.Visibility.Visible;
            }
            else
                AddText.Visibility = System.Windows.Visibility.Collapsed;
        }

        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailPage.xaml?selectedItem=" + MainListBox.SelectedIndex, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }

        private void appbar_refreshButton_Click(object sender, EventArgs e)
        {
            App.ServerCollection.GetStatus();
        }

        private void appbar_addButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/EditPage.xaml", UriKind.Relative));

        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }
    }
}
