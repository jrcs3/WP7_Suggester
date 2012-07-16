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
using SuggesterControls;
using Microsoft.Phone.Shell;
using SuggesterTools;

namespace SuggesterApp
{
    public partial class MainPage : PhoneApplicationPage
    {

        private AccelerometerSensorWithShakeDetection _shakeSensor = new AccelerometerSensorWithShakeDetection();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            addMakeSuggestionCtlToPanorama(_basePanorama, ApplicationBar, "objects", "Object.xml", "objects", "object", 20);
            addMakeSuggestionCtlToPanorama(_basePanorama, ApplicationBar, "locations", "Location.xml", "locations", "location", 20);
            addMakeSuggestionCtlToPanorama(_basePanorama, ApplicationBar, "adjectives", "Adjective.xml", "adjectives", "adjective", 20);
            addMakeSuggestionCtlToPanorama(_basePanorama, ApplicationBar, "emotions", "Emotions.txt", "emotions", "emotions", 45);
            addMakeSuggestionCtlToPanorama(_basePanorama, ApplicationBar, "relationships", "Relationship.txt", "relationships", "relationship", 15);

            _shakeSensor.ShakeDetected += ShakeDetected;
            _shakeSensor.Start();

        }
        private MakeSuggestionCtl addMakeSuggestionCtlToPanorama(Panorama para, IApplicationBar applicationBar, string header, string fileName, string pluralName, string singularName, int historyCount)
        {
            //EventHandler gotFocusMethod = _suggester_GotFocus;
            var item = new PanoramaItem
            {
                //Header = header,               
                FontSize = 42
            };
            var sug = new MakeSuggestionCtl
            {
                FileName = fileName,
                PluralName = pluralName,
                SingularName = singularName,
                HistoryCount = historyCount                
            };
            sug.DoSelect();
            //sug.ClickList += clickListMethod;
            //sug.FocusToMe += gotFocusMethod;
            item.Content = sug;
            para.Items.Add(item);

            var newItem = new MyApplicationBarMenuItem
            {
                Text = pluralName,
                MyMakeSuggestionCtl = sug,
                MyPanoramaItem = item
            };
            newItem.Click += newItem_Click;
            applicationBar.MenuItems.Add(newItem);


            return sug;
        }

        private void ListSuggestions_Click(object sender, EventArgs e)
        {
            PanoramaItem si = (PanoramaItem)_basePanorama.SelectedItem;
            //Console.WriteLine(si.ToString());
            var s = si.Content as MakeSuggestionCtl;
            if (s != null)
            {
                this.NavigationService.Navigate(new Uri(string.Format("/ListSuggestions.xaml?xmlFile={0}&listName={1}", s.FileName, Uri.EscapeUriString(s.PluralName)), UriKind.Relative));
            }
        }

        private void newItem_Click(object sender, EventArgs e)
        {
            MyApplicationBarMenuItem s = (MyApplicationBarMenuItem)sender;

            PanoramaItem[] tempSug = new PanoramaItem[_basePanorama.Items.Count];
            int selectedItemIndex = -1;
            for (int i = 0; i < _basePanorama.Items.Count; ++i)
            {
                var item = _basePanorama.Items[i] as PanoramaItem;
                if (item != null)
                {
                    tempSug[i] = item;
                    if (item == s.MyPanoramaItem)
                    {
                        selectedItemIndex = i;
                    }
                }
            }
            _basePanorama.Items.Clear();
            foreach (var item in tempSug)
            {
                _basePanorama.Items.Add(item);
            }
            if (selectedItemIndex != -1)
            {
                _basePanorama.DefaultItem = _basePanorama.Items[selectedItemIndex];
            }
            System.Diagnostics.Debug.WriteLine(sender.ToString());
        }

        private void ShakeDetected(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                PanoramaItem si = (PanoramaItem)_basePanorama.SelectedItem;
                var s = si.Content as MakeSuggestionCtl;
                if (s != null)
                {
                    s.DoSelect();
                }
                System.Diagnostics.Debug.WriteLine("-");
                //DoSelect();
            });
        }

    }
}