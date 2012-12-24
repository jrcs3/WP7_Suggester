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
using SuggesterTools;

namespace SuggesterControls
{
    public partial class ListSuggestionListsCtl : UserControl
    {
        public SuggesterAppConfig Config { get; set; }
        public SuggestionList SelectedSuggestion;

        public event EventHandler EditListSttings;
        public event EventHandler UploadList;
        public event EventHandler ListSuggestions;

        public ListSuggestionListsCtl()
        {
            InitializeComponent();
        }

        public void LoadList()
        {
            if (Config != null)
            {
                _lstSuggestionLists.ItemsSource = Config.Lists.OrderBy(l => l.SortPriority);
            }
        }

        private void editList_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            SelectedSuggestion = Config.Lists.Where(l => l.ListName == b.Tag.ToString()).SingleOrDefault();

            if (EditListSttings != null)
            {
                EditListSttings(this, EventArgs.Empty);
            }
        }

        private void listItems_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            SelectedSuggestion = Config.Lists.Where(l => l.ListName == b.Tag.ToString()).SingleOrDefault();

            if (ListSuggestions != null)
            {
                ListSuggestions(this, EventArgs.Empty);
            }
        }

        private void uploadList_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            SelectedSuggestion = Config.Lists.Where(l => l.ListName == b.Tag.ToString()).SingleOrDefault();

            if (UploadList != null)
            {
                UploadList(this, EventArgs.Empty);
            }
        }


        private void _lstSuggestionLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SelectedSuggestion = (SuggestionList)e.AddedItems[0];
            }
        }
    }
}
