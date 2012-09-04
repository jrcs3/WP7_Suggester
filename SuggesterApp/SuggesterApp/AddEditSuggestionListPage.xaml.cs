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
using SuggesterTools;

namespace SuggesterApp
{
    public partial class AddEditSuggestionListPage : PhoneApplicationPage
    {
        public AddEditSuggestionListPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string ListName = Uri.UnescapeDataString(this.NavigationContext.QueryString["listName"]);
            var Lists = App.Config.Lists;
            SuggestionList list = Lists.Where(l => l.ListName == ListName).SingleOrDefault();
            _addEditSuggestionListCtl.List = list;

            _addEditSuggestionListCtl.Load();
            //listSuggestionsCtl1.ListName = ListName;
            base.OnNavigatedTo(e);
        }

        private void _addEditSuggestionListCtl_Save(object sender, EventArgs e)
        {
            _addEditSuggestionListCtl.Retreave();
            App.Config.SaveXmlToFileInIS();
            this.NavigationService.GoBack();
        }

        private void _addEditSuggestionListCtl_LoadList(object sender, EventArgs e)
        {

        }

    }
}