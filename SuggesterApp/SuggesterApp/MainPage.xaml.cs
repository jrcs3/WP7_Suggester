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
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Windows.Navigation;

namespace SuggesterApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private AccelerometerSensorWithShakeDetection _shakeSensor = new AccelerometerSensorWithShakeDetection();
        private const string TRIAL_MODE_MENU_TEXT = "Trial Mode";
        private static bool _wasTrialMode = false;
        //private static ColorMode _colorMode = ColorMode.Traditional;

        // Constructor
        public MainPage()
        {
            _wasTrialMode = App.IsTrial;
            InitializeComponent();

            var newItem = new MyApplicationBarMenuItem
            {
                Text = "About",
            };
            newItem.Click += navigateAbout; // 
            ApplicationBar.MenuItems.Add(newItem);

            App.Config = new SuggesterAppConfig();
            if (SuggesterAppConfig.IsInOS())
            {
                App.Config.LoadSuggestionListFromIS();
            }
            else
            {
                App.Config.LoadSuggestionListFromResources();
                App.Config.SaveXmlToFileInIS();
            }
            List<SuggestionList> listOfLists = App.Config.Lists;
            if (!App.IsTrial)
            {
                foreach (SuggestionList list in listOfLists.Where(lol => lol.IsVisible).OrderBy(lol => lol.SortPriority))
                {
                    loadSuggestionListToUI(_basePanorama, ApplicationBar, newItem_Click, list);
                }
            }
            else
            {
                foreach (SuggestionList list in listOfLists.Where(lol => lol.IsTrialList).OrderBy(lol => lol.SortPriority))
                {
                    loadSuggestionListToUI(_basePanorama, ApplicationBar, newItem_Click, list);
                }
                addTrialModeCtlToPanorama(_basePanorama, ApplicationBar, TRIAL_MODE_MENU_TEXT);
            }
                
            _shakeSensor.ShakeDetected += ShakeDetected;
            _shakeSensor.Start();

            App.ApplicationActivated += new EventHandler(App_ApplicationActivated);
        }

        private static void loadSuggestionListToUI(Panorama para, IApplicationBar appBar, EventHandler newItemClick, SuggestionList li)
        {
            addMakeSuggestionCtlToPanorama(para, appBar, li.Id, li.HeaderText, li.ListFileName, li.PluralName, li.SingularName, li.HistoryCount, newItemClick);
        }

        private void navigateAbout(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void App_ApplicationActivated(object sender, EventArgs e)
        {
            executeUpgrade();
        }

        private void addTrialModeCtlToPanorama(Panorama para, IApplicationBar applicationBar, string header)
        {
            var item = new PanoramaItem
            {
                FontSize = 42
            };
            var tmc = new TrialModeCtl();
            item.Content = tmc;
            para.Items.Add(item);

            var newItem = new MyApplicationBarMenuItem
            {
                Text = header,
                MyPanoramaItem = item
            };
            newItem.Click += newItem_Click;
            applicationBar.MenuItems.Add(newItem);
        }
        private static MakeSuggestionCtl addMakeSuggestionCtlToPanorama(Panorama para, IApplicationBar applicationBar, int id, string header, string fileName, string pluralName, string singularName, int historyCount, EventHandler newItemClick)
        {            
            var item = new PanoramaItem
            {
                FontSize = 42
            };
            var sug = new MakeSuggestionCtl
            {
                Id = id,
                FileName = fileName,
                PluralName = pluralName,
                SingularName = singularName,
                HistoryCount = historyCount                
            };
            sug.DoSelect();
            item.Content = sug;
            para.Items.Add(item);

            var newItem = new MyApplicationBarMenuItem
            {
                Text = pluralName,
                MyMakeSuggestionCtl = sug,
                MyPanoramaItem = item
            };
            newItem.Click += newItemClick;
            applicationBar.MenuItems.Add(newItem);

            return sug;
        }

        private void ListSuggestions_Click(object sender, EventArgs e)
        {
            PanoramaItem si = (PanoramaItem)_basePanorama.SelectedItem;
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
            });
        }

        #region Updgrade
        private void executeUpgrade()
        {
            removeTrialModeFromMenu();

            PanoramaItem[] tempSug = new PanoramaItem[_basePanorama.Items.Count];
            for (int i = 0; i < _basePanorama.Items.Count; ++i)
            {
                var item = _basePanorama.Items[i] as PanoramaItem;
                if (item != null)
                {
                    tempSug[i] = item;
                }
            }
            _basePanorama.Items.Clear();
            foreach (var item in tempSug)
            {
                var tcm = item.Content as TrialModeCtl;
                if (tcm == null)
                {
                    _basePanorama.Items.Add(item);
                }
            }

            List<SuggestionList> listOfLists = App.Config.Lists;
            foreach (SuggestionList list in listOfLists.Where(lol => !lol.IsTrialList && lol.IsVisible))
            {
                loadSuggestionListToUI(_basePanorama, ApplicationBar, newItem_Click, list);
            }

            App.Config.SaveXmlToFileInIS();
        }

        private void removeTrialModeFromMenu()
        {
            for (int i = ApplicationBar.MenuItems.Count - 1; i >= 0; --i)
            {
                var item = ApplicationBar.MenuItems[i];
                var castItem = item as MyApplicationBarMenuItem;
                if (castItem != null)
                {
                    if (castItem.Text == TRIAL_MODE_MENU_TEXT)
                    {
                        ApplicationBar.MenuItems.Remove(castItem);
                    }
                }
            }
        }
        #endregion Updgrade

        private void SettingsIconButton_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //MyApplicationBarMenuItem s = (MyApplicationBarMenuItem)sender;

            //PanoramaItem[] tempSug = new PanoramaItem[_basePanorama.Items.Count];
            //int selectedItemIndex = -1;
            //for (int i = 0; i < _basePanorama.Items.Count; ++i)
            //{
            //    var item = _basePanorama.Items[i] as PanoramaItem;
            //    if (item != null)
            //    {
            //        tempSug[i] = item;
            //        if (item == s.MyPanoramaItem)
            //        {
            //            selectedItemIndex = i;
            //        }
            //    }
            //}

            bool isSame = true;
            int nextItem = 0;
            var newList = App.Config.Lists.Where(l => l.IsVisible).OrderBy(l => l.SortPriority).ToList();
            PanoramaItem[] tempSug = new PanoramaItem[newList.Count];
            //foreach (var list in App.Config.Lists.Where(l => l.IsVisible).OrderBy(l => l.SortPriority))
            foreach (var list in newList)
            {
                bool foundControl = false;
                for (int i = 0; i < _basePanorama.Items.Count; ++i)
                {
                    var item = _basePanorama.Items[i] as PanoramaItem;                    
                    var controlItem = item.Content as MakeSuggestionCtl;
                    if ((controlItem != null) && (controlItem.Id == list.Id))
                    {
                        if (nextItem != i)
                        {
                            isSame = false;
                        }
                        tempSug[nextItem++] = item;
                        foundControl = true;
                        break;
                    }
                }
                if (!foundControl)
                {
                    var sug = new MakeSuggestionCtl
                    {
                        Id = list.Id,
                        FileName = list.FileName,
                        PluralName = list.PluralName,
                        SingularName = list.SingularName,
                        HistoryCount = list.HistoryCount
                    };
                    var panItem = new PanoramaItem
                    {
                        FontSize = 42
                    };
                    sug.DoSelect();
                    panItem.Content = sug;
                    tempSug[nextItem++] = panItem;
                    //foundControl = true;
                }
            }

            if (!isSame)
            {
                _basePanorama.Items.Clear();
                foreach (var item in tempSug)
                {
                    _basePanorama.Items.Add(item);
                }
            }
            //if (selectedItemIndex != -1)
            //{
            //    _basePanorama.DefaultItem = _basePanorama.Items[selectedItemIndex];
            //}
        }
    }
}