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
    public partial class AddEditSuggestionCtl : UserControl
    {
        public event EventHandler Saved;
        public event EventHandler Deleted;

        private string _fileName = string.Empty;
        private StorageHelper<Suggestion> _storageHelper;
        private int? _suggestionId;

        public int SuggestionId
        {
            get { return _suggestionId.Value; }
            set
            {
                _suggestionId = value;
                loadEditedSuggestion();
            }
        }

        private void loadEditedSuggestion()
        {
            if (_suggestionId.HasValue && _storageHelper != null)
            {
                EditedSuggestion = _storageHelper.GetList().Where(s => s.Id == _suggestionId.Value).SingleOrDefault();
                _txtNewSuggestion.Text = EditedSuggestion.Text;
                _lblFieldCaption.Text = "Edit Suggestion:";
            }
            //throw new NotImplementedException();
        }

        public string ListName { get; set; }

        public Suggestion EditedSuggestion { get; set; }

        public AddEditSuggestionCtl()
        {
            InitializeComponent();
        }

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
                        loadEditedSuggestion();
                    }
                    catch { }
                }
            }
        }

        private void _btnSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave();
        }

        public void DoDelete()
        {
            EditedSuggestion.Text = _txtNewSuggestion.Text;
            List<Suggestion> list = _storageHelper.GetList();
            Suggestion match = list.Where(s => s.Id == EditedSuggestion.Id).SingleOrDefault();
            if (match != null)
            {
                list.Remove(match);
                _storageHelper.SaveList(list);
                if (Deleted != null)
                {
                    Deleted(this, EventArgs.Empty);
                }
            }
        }

        public void DoSave()
        {
            if (EditedSuggestion == null)
            {
                if (!string.IsNullOrWhiteSpace(_txtNewSuggestion.Text))
                {
                    List<Suggestion> list = _storageHelper.GetList();
                    if (list.Where(l => l.Text.Trim().ToUpper() == _txtNewSuggestion.Text.Trim().ToUpper()).Count() == 0)
                    {
                        int maxId = 0;
                        if (list.Count() > 0)
                        {
                            maxId = (from l in list select l.Id).Max();
                        }
                        EditedSuggestion = new Suggestion(maxId + 1, _txtNewSuggestion.Text);
                        list.Add(EditedSuggestion);
                        _storageHelper.SaveList(list);
                        _suggestionId = EditedSuggestion.Id;
                        if (Saved != null)
                        {
                            Saved(this, EventArgs.Empty);
                        }
                    }
                }
            }
            else
            {
                EditedSuggestion.Text = _txtNewSuggestion.Text;
                List<Suggestion> list = _storageHelper.GetList();
                Suggestion match = list.Where(s => s.Id == EditedSuggestion.Id).SingleOrDefault();
                if (match != null)
                {
                    match.Text = EditedSuggestion.Text;
                    _storageHelper.SaveList(list);
                    if (Saved != null)
                    {
                        Saved(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}