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

namespace SuggesterApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();


            addMakeSuggestionCtlToPanorama(_basePanorama, "girl's names", "FemaleName.xml", "girl's names", "girl's name", 25);
            addMakeSuggestionCtlToPanorama(_basePanorama, "boy's names", "MaleName.xml", "boy's names", "boy's name", 25);
            addMakeSuggestionCtlToPanorama(_basePanorama, "professions", "Profession.xml", "professions", "profession", 25);
            addMakeSuggestionCtlToPanorama(_basePanorama, "objects", "Object.xml", "objects", "object", 20);
            addMakeSuggestionCtlToPanorama(_basePanorama, "locations", "Location.xml", "locations", "location", 20);
            addMakeSuggestionCtlToPanorama(_basePanorama, "adjectives", "Adjective.xml", "adjectives", "adjective", 20);
            addMakeSuggestionCtlToPanorama(_basePanorama, "magic 8 ball", "Magic8.xml", "answers", "answer", 1);
        }
        private static MakeSuggestionCtl addMakeSuggestionCtlToPanorama(Panorama para, string header, string fileName, string pluralName, string singularName, int historyCount)
        {
            //EventHandler gotFocusMethod = _suggester_GotFocus;
            var item = new PanoramaItem
            {
                Header = header,
            };
            var sug = new MakeSuggestionCtl
            {
                FileName = fileName,
                PluralName = pluralName,
                SingularName = singularName,
                HistoryCount = historyCount
            };
            //sug.ClickList += clickListMethod;
            //sug.FocusToMe += gotFocusMethod;
            item.Content = sug;
            para.Items.Add(item);
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
    }
}