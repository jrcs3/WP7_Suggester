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
//using System.Windows.Shapes;
using SuggesterTools;
using System.IO;
using System.Reflection;
using System.IO.IsolatedStorage;

namespace SuggesterControls
{
    public partial class MakeSuggestionCtl : UserControl
    {
        //public event EventHandler ClickList;
        //public event EventHandler FocusToMe;
        public MakeSuggestionCtl()
        {
            InitializeComponent();

            //_txtSuggestion.Text = string.Empty;
            DoSelect();
        }
        private StorageHelper<Suggestion> _storageHelper;
        private Suggestion[] _list;
        private bool _listValid = false;

        private IsolatedStorageSettings _settings;


        private string _singularName;
        public string SingularName
        {
            get { return _singularName; }
            set
            {
                _singularName = value;
                if (string.IsNullOrWhiteSpace(_txtSuggestion.Text))
                {
                    _txtSuggestion.Text = "First " + _singularName;
                    //DoSelect();
                }
            }
        }

        private string _pluralName;
        public string PluralName
        {
            get { return _pluralName; }
            set
            {
                _pluralName = value;
                _txtTitle.Text = value;
                //_btnSuggest.Content = "Next " + _pluralName;
            }
        }
        private string _fileName = string.Empty;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                if (!string.IsNullOrWhiteSpace(_fileName))
                {
                    ensureList();
                }
            }
        }

        public int HistoryCount { get; set; }

        private List<int> pickHistory
        {
            get
            {
                int[] rVal;
                ensureSettings();
                if (_settings.TryGetValue<int[]>(historyKeyName, out rVal))
                {
                    return rVal.ToList();
                }
                else
                {
                    return new List<int>();
                }
            }
            set
            {
                if (_settings.Contains(historyKeyName))
                {
                    _settings[historyKeyName] = value.ToArray();
                }
                else
                {
                    _settings.Add(historyKeyName, value.ToArray());
                }
            }
        }
        private string historyKeyName
        {
            get { return PluralName + "History"; }
        }

        private void ensureSettings()
        {
            if (_settings == null)
            {
                _settings = IsolatedStorageSettings.ApplicationSettings;
            }
        }


        private void ensureList()
        {
            if (!_listValid)
            {
                try
                {
                    _btnSuggest.IsEnabled = true;
                    _storageHelper = new StorageHelper<Suggestion>(_fileName);
                    //_list = _storageHelper.GetList().ToArray();
                    _list = getSuggestionList().ToArray();
                    if (_list.Length == 0)
                    {
                        _btnSuggest.IsEnabled = false;
                    }
                    _listValid = true;
                }
                catch (Exception ex)
                {
                    _btnSuggest.IsEnabled = false;
                }
            }
        }

        private void _btnSuggest_Click(object sender, RoutedEventArgs e)
        {
            DoSelect();
        }

        private List<Suggestion> getSuggestionList()
        {
            List<Suggestion> list = _storageHelper.GetList();
            if (list.Count == 0)
            {
                //list = ReadResourceFile("SuggesterControls.SampleData." + Path.GetFileNameWithoutExtension(_fileName) + ".txt");
                list = ListHelper.ReadResourceFile("Lists", Path.GetFileNameWithoutExtension(_fileName) + ".txt");
                _storageHelper.SaveList(list);
            }
            return list;
        }

        public void DoSelect()
        {
            try
            {
                ensureList();
                List<int> hList = pickHistory;
                Random random = new Random();
                int randomNumber;
                do
                {
                    randomNumber = random.Next(0, _list.Length);
                } while (hList.Contains(randomNumber));
                hList.Insert(0, randomNumber);
                while (hList.Count > HistoryCount)
                {
                    hList.RemoveAt(HistoryCount);
                }
                pickHistory = hList;
                _txtSuggestion.Text = _list[randomNumber].Text;
            }
            catch
            {
                _btnSuggest.IsEnabled = false;
            }
        }
    }
}
