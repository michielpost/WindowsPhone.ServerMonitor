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
using NetworkMonitor.Model;

namespace NetworkMonitor
{
    public partial class ShowStatusControl : UserControl
    {
        public Server Server
        {
            get
            {
                if (this.DataContext != null)
                    return ((Server)this.DataContext);
                else
                    return null;

            }
            set
            {
                this.DataContext = value;
                InitBinding();
            }
        }

        public ShowStatusControl()
        {
           
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(ShowStatusControl_Loaded);

        }

        void ShowStatusControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitBinding();
        }


        private void InitBinding()
        {
            if (Server != null)
            {
                //Subscribe to PropertyChanged of the UserFile
                Server.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ShowStatusControl_PropertyChanged);

               
            }
        }

        private void ShowStatusControl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                
            }
            else if (e.PropertyName == "Percentage")
            {
               
            }

        }
    }
}
