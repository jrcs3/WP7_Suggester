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
using System.Windows.Navigation;
using SuggesterControls;

namespace SuggesterApp
{
    public partial class ListSuggestionsPage : PhoneApplicationPage
    {
        private string _listName;
        public string ListName
        {
            get { return _listName; }
            set
            {
                _listName = value;
                PageTitle.Text = string.Format("{0} list", _listName);
            }
        }
        public ListSuggestionsPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            listSuggestionsCtl1.FileName = this.NavigationContext.QueryString["xmlFile"];
            ListName = Uri.UnescapeDataString(this.NavigationContext.QueryString["listName"]);
            listSuggestionsCtl1.ListName = ListName;
            base.OnNavigatedTo(e);
        }

        private void addSuggestionCtl1_Saved(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void listSuggestionsCtl1_ClickAdd(object sender, EventArgs e)
        {
            var s = sender as ListSuggestionsCtl;
            if (s != null)
            {
                this.NavigationService.Navigate(new Uri(string.Format("/AddEditSuggestionPage.xaml?xmlFile={0}&listName={1}", s.FileName, Uri.EscapeUriString(s.ListName)), UriKind.Relative));
            }
        }

        private void listSuggestionsCtl1_SelectItem(object sender, EventArgs e)
        {
            var s = sender as ListSuggestionsCtl;
            if (s != null)
            {
                this.NavigationService.Navigate(new Uri(string.Format("/AddEditSuggestionPage.xaml?xmlFile={0}&listName={1}&suggestionId={2}", 
                    s.FileName, Uri.EscapeUriString(s.ListName), s.SelectedSuggestion.Id), UriKind.Relative));
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(
                new Uri(string.Format("/AddEditSuggestionPage.xaml?xmlFile={0}&listName={1}", listSuggestionsCtl1.FileName, Uri.EscapeUriString(listSuggestionsCtl1.ListName)), UriKind.Relative));
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}