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
    public partial class AddEditSuggestionListCtl : UserControl
    {
        public event EventHandler Save;
        public event EventHandler LoadList;

        private SuggestionList _list;

        public SuggestionList List
        {
            get
            {
                //SuggestionList rVal = new SuggestionList();
                //_list.ListName = _txtListName.Text;
                //_list.HeaderText = _txtHeader.Text;
                //_list.SingularName = _txtSingular.Text;
                //_list.PluralName = _txtPlural.Text;
                //_list.IsVisible = _chkIsVisible.IsChecked ?? false;
                //_list.SortPriority = int.Parse(_txtSort.Text);
                return _list;
            }
            set
            {
                _list = value;
                //_txtListName.Text = _list.ListName;
                //_txtHeader.Text = _list.HeaderText;
                //_txtSingular.Text = _list.SingularName;
                //_txtPlural.Text = _list.PluralName;
                //_chkIsVisible.IsChecked = _list.IsVisible;
                //_txtSort.Text = _list.SortPriority.ToString();
            }
        }

        public AddEditSuggestionListCtl()
        {
            InitializeComponent();
        }

        public void Load()
        {
            _txtListName.Text = noNull(List.ListName);
            _txtHeader.Text = noNull(List.HeaderText);
            _txtSingular.Text = noNull(List.SingularName);
            _txtPlural.Text = noNull(List.PluralName);
            _chkIsVisible.IsChecked = List.IsVisible;
            _txtSort.Text = List.SortPriority.ToString();
            _txtHist.Text = List.HistoryCount.ToString();

            _txtListName.IsEnabled = !List.ReadOnly;
            _txtHeader.IsEnabled = !List.ReadOnly;
            _txtSingular.IsEnabled = !List.ReadOnly;
            _txtPlural.IsEnabled = !List.ReadOnly;
            _btnList.IsEnabled = !List.ReadOnly;
        }

        public static string noNull(string theString)
        {
            if (theString == null)
            {
                return string.Empty;
            }
            else
            {
                return theString;
            }
        }

        public void Retreave()
        {
            _list.ListName = _txtListName.Text;
            _list.HeaderText = _txtHeader.Text;
            _list.SingularName = _txtSingular.Text;
            _list.PluralName = _txtPlural.Text;
            _list.IsVisible = _chkIsVisible.IsChecked ?? false;
            int SortPriority;
            if (int.TryParse(_txtSort.Text, out SortPriority))
            {
                _list.SortPriority = SortPriority;
            }
            int HistoryCount;
            if (int.TryParse(_txtHist.Text, out HistoryCount))
            {
                _list.HistoryCount = HistoryCount;
            }
        }

        private void _btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Save != null)
            {
                Save(this, EventArgs.Empty);
            }
        }

        private void _btnList_Click(object sender, RoutedEventArgs e)
        {
            if (LoadList != null)
            {
                LoadList(this, EventArgs.Empty);
            }
        }
    }
}
