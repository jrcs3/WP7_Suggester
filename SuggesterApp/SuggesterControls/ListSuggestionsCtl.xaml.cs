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
    public partial class ListSuggestionsCtl : UserControl
    {

        public event EventHandler ClickAdd;
        public event EventHandler SelectItem;
        private StorageHelper<Suggestion> _storageHelper;

        public Suggestion SelectedSuggestion { get; private set; }

        public ListSuggestionsCtl()
        {
            InitializeComponent();
        }


        public string ListName { get; set; }
        private string _fileName = string.Empty;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                if (!string.IsNullOrWhiteSpace(_fileName))
                {
                    try
                    {
                        _storageHelper = new StorageHelper<Suggestion>(_fileName);
                        //_lstSuggestions.ItemsSource = _storageHelper.GetList();
                        Refresh();
                    }
                    catch { }
                }
            }
        }

        private void _btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ClickAdd != null)
            {
                ClickAdd(this, new EventArgs());
            }
        }

        public void Refresh()
        {
            _lstSuggestions.ItemsSource = _storageHelper.GetList().OrderBy(l => l.Text);
        }

        private void _btnReload_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void _lstSuggestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SelectedSuggestion = (Suggestion)e.AddedItems[0];
                if (SelectItem != null)
                {
                    SelectItem(this, EventArgs.Empty);
                }
            }
        }
    }
}
