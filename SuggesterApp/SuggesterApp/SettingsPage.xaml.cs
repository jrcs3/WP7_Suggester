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
using System.Windows.Navigation;

namespace SuggesterApp
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            _listSuggestionListsCtl.Config = App.Config;
            _listSuggestionListsCtl.LoadList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _listSuggestionListsCtl.LoadList();
        }

        private void _listSuggestionListsCtl_ListSuggestions(object sender, EventArgs e)
        {
            var ctl = (ListSuggestionListsCtl)sender;
            var s = ctl.SelectedSuggestion;
            this.NavigationService.Navigate(new Uri(string.Format("/ListSuggestionsPage.xaml?xmlFile={0}&listName={1}", s.FileName, Uri.EscapeUriString(s.ListName)), UriKind.Relative));
        }

        private void _listSuggestionListsCtl_EditListSttings(object sender, EventArgs e)
        {
            var ctl = (ListSuggestionListsCtl)sender;
            var s = ctl.SelectedSuggestion;
            this.NavigationService.Navigate(new Uri(string.Format("/AddEditSuggestionListPage.xaml?&listName={0}", Uri.EscapeUriString(s.ListName)), UriKind.Relative));
        }

        private void _listSuggestionListsCtl_UploadList(object sender, EventArgs e)
        {
            var ctl = (ListSuggestionListsCtl)sender;
            var s = ctl.SelectedSuggestion;
            this.NavigationService.Navigate(new Uri(string.Format("/SkydriveBrowseSave.xaml?xmlFile={0}&listName={1}", s.FileName, Uri.EscapeUriString(s.ListName)), UriKind.Relative));
        }
    }
}