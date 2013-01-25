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
using Microsoft.Live;
using System.IO;

namespace SuggesterApp
{
    public partial class AddEditSuggestionListPage : PhoneApplicationPage
    {
        private bool _isNewList = false;

        private SuggestionList _suggestionList;
        private string _cachedList = null;
        public AddEditSuggestionListPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _isNewList = getIsNewList();

            if (_isNewList)
            {
                _addEditSuggestionListCtl.List = new SuggestionList();
                if (this.NavigationContext.QueryString.ContainsKey("fieldId"))
                {
                    string fieldId = Uri.UnescapeDataString(this.NavigationContext.QueryString["fieldId"]);

                    if (this.NavigationContext.QueryString.ContainsKey("remoteFileName"))
                    {
                        string remoteFileName = Uri.UnescapeDataString(this.NavigationContext.QueryString["remoteFileName"]);
                        _addEditSuggestionListCtl.List.ListName = System.IO.Path.GetFileNameWithoutExtension(remoteFileName);
                    }
                    _addEditSuggestionListCtl.Load();

                    var client = new LiveConnectClient(App.LiveSession);

                    client.DownloadCompleted += new EventHandler<LiveDownloadCompletedEventArgs>(client_DownloadCompleted);
                    client.DownloadAsync(fieldId + "/content");

                }
            }
            else
            {
                string ListName = Uri.UnescapeDataString(this.NavigationContext.QueryString["listName"]);
                var Lists = App.Config.Lists;
                _suggestionList = Lists.Where(l => l.ListName == ListName).SingleOrDefault();
                _addEditSuggestionListCtl.List = _suggestionList;

                _addEditSuggestionListCtl.Load();
            }
            //listSuggestionsCtl1.ListName = ListName;
            base.OnNavigatedTo(e);
        }

        private bool getIsNewList()
        {
            bool isNewList = false;
            if (this.NavigationContext.QueryString.ContainsKey("fieldId"))
            {
                return true;
            }
            if (this.NavigationContext.QueryString.ContainsKey("newList"))
            {
                string isNewString = this.NavigationContext.QueryString["newList"];
                if (!string.IsNullOrWhiteSpace(isNewString) && (isNewString == "true"))
                {
                    isNewList = true;
                }
            }
            return isNewList;
        }

        private void _addEditSuggestionListCtl_Save(object sender, EventArgs e)
        {
            doSave();
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void doSave()
        {
            _addEditSuggestionListCtl.Retreave();
            if (_isNewList)
            {
                var Lists = App.Config.Lists;
                if (string.IsNullOrWhiteSpace(_addEditSuggestionListCtl.List.ListFileName))
                {
                    string proposedFileName = string.Format("{0}.xml", _addEditSuggestionListCtl.List.ListName);
                    int number = 0;
                    while (Lists.Where(x => x.ListFileName == proposedFileName).Count() > 0)
                    {
                        proposedFileName = string.Format("{0}_{1:00}.xml", _addEditSuggestionListCtl.List.ListName, ++number);
                    }
                    _addEditSuggestionListCtl.List.ListFileName = proposedFileName;
                }
                App.Config.Lists.Add(_addEditSuggestionListCtl.List);
            }
            App.Config.SaveXmlToFileInIS();
            if (!string.IsNullOrWhiteSpace(_cachedList))
            {
                var rVal = new List<Suggestion>();
                int lineNumber = 1;
                foreach (string line in _cachedList.Replace("\r", string.Empty).Split('\n'))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string trimmedLine = line.Trim();
                        if (rVal.Where(x => x.Text == trimmedLine).Count() == 0)
                        {
                            rVal.Add(new Suggestion(lineNumber++, trimmedLine));
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(trimmedLine);
                        }
                    }
                }
                StorageHelper<Suggestion> helper = new StorageHelper<Suggestion>(_addEditSuggestionListCtl.List.ListFileName);
                helper.SaveList(rVal);
                _cachedList = null;
                while (NavigationService.BackStack.First().Source.OriginalString.StartsWith("/SkydriveBrowseGet.xaml"))
                {
                    NavigationService.RemoveBackEntry();
                }
            }
        }

        private void _addEditSuggestionListCtl_LoadList(object sender, EventArgs e)
        {
            doSave();
            this.NavigationService.Navigate(new Uri(string.Format("/ListSuggestionsPage.xaml?xmlFile={0}&listName={1}", _suggestionList.ListFileName, Uri.EscapeUriString(_suggestionList.PluralName)), UriKind.Relative));
        }

        void client_DownloadCompleted(object sender, LiveDownloadCompletedEventArgs e)
        {
            Stream textLinesAsStream = e.Result;
            textLinesAsStream.Seek(0, SeekOrigin.Begin);
            StreamReader rdr = new StreamReader(textLinesAsStream);

            _cachedList = rdr.ReadToEnd();

            var count = _cachedList.ToArray().Where(x => x == '\n').Count();

            _addEditSuggestionListCtl.List.HistoryCount = (int)(count / 5m);
            _addEditSuggestionListCtl.Load();

            //_addEditSuggestionListCtl.List

        }
    }
}