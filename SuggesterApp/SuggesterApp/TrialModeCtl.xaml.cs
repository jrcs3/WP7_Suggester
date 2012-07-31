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
using Microsoft.Phone.Tasks;

namespace SuggesterApp
{
    public partial class TrialModeCtl : UserControl
    {
        public TrialModeCtl()
        {
            InitializeComponent();
        }

        private void _btnBuyNow_Click(object sender, RoutedEventArgs e)
        {
#if FAKESTORE
            if (MessageBox.Show("Here we would send you to the app store.\nDo you want to simulate activation?", "Fake App Store", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var x = ((App)Application.Current);
                x.DoFakeActivete();
            }
#else
            MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();
            marketplaceDetailTask.Show();
#endif
        }
    }
}
