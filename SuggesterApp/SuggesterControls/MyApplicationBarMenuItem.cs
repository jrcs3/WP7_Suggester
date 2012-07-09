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
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SuggesterControls
{
    public class MyApplicationBarMenuItem : ApplicationBarMenuItem
    {
        public MakeSuggestionCtl MyMakeSuggestionCtl { get; set; }
        public PanoramaItem MyPanoramaItem { get; set; }
    }
}
