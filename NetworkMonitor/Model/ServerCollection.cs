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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using NetworkMonitor.Classes;
using System.Runtime.Serialization;

namespace NetworkMonitor.Model
{
   
    public class ServerCollection : ObservableCollection<Server>
    {
        public void GetStatus()
        {
            foreach (Server server in this)
            {
                server.GetStatus();
            }
        }

        public void Save()
        {
            //Save to Isolated Storage
            IsolatedStorageCacheManager<ObservableCollection<Server>>.Store("servercollection.xml", this);  



        }

        public void Load()
        {
            //Load from Isolated Storage
            ObservableCollection<Server> loadList = IsolatedStorageCacheManager<ObservableCollection<Server>>.Retrieve("servercollection.xml");

            this.Clear();

            if (loadList != null)
            {
                foreach (var item in loadList)
                    this.Add(item);
            }
        }
    }
}
