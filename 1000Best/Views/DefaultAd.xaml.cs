using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace _1000Best.Views
{
    public partial class DefaultAd : UserControl
    {
        public DefaultAd()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var searchTask = new MarketplaceSearchTask();
            searchTask.SearchTerms = "Mom";
            searchTask.Show();
        }
    }
}
