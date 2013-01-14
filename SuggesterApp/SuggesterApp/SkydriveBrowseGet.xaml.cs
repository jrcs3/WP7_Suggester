using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Live;
using Microsoft.Phone.Controls;
using SuggesterTools;

namespace SuggesterApp
{
    public partial class SkydriveBrowseGet : PhoneApplicationPage
    {
        private LiveConnectClient _client;
        string _folderName = "/me/skydrive";
        public List<SkyDriveContent> _contentList = new List<SkyDriveContent>();
        public string _displayName = string.Empty;
        public string _xmlFile = string.Empty;
        private SkyDriveContent selectedItem;

        public SkydriveBrowseGet()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (this.NavigationContext.QueryString.Keys.Contains("folderId"))
            {
                _folderName = this.NavigationContext.QueryString["folderId"];
            }
            if (this.NavigationContext.QueryString.Keys.Contains("fileName"))
            {
                this.txtFileName.Text = this.NavigationContext.QueryString["fileName"];
            }
            if (this.NavigationContext.QueryString.Keys.Contains("fullPath"))
            {
                this.txtFullPath.Text = this.NavigationContext.QueryString["fullPath"];
            }
            if (this.NavigationContext.QueryString.Keys.Contains("xmlFile"))
            {
                _xmlFile = this.NavigationContext.QueryString["xmlFile"];
            }
            if (this.NavigationContext.QueryString.Keys.Contains("listName"))
            {
                this.PageTitle.Text = this.NavigationContext.QueryString["listName"];
                if(string.IsNullOrWhiteSpace(this.txtFileName.Text))
                {
                    this.txtFileName.Text = this.PageTitle.Text + ".txt";
                }
            }

            if (App.LiveSession != null)
            {
                loadFolder(_folderName);
            }
            base.OnNavigatedTo(e);
        }

        private void loadFolder(string folderName)
        {
            LiveConnectClient client = new LiveConnectClient(App.LiveSession);
            client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(clientDataFetch_GetCompleted);
            client.GetAsync(folderName + "/files");
        }

        void clientDataFetch_GetCompleted(object sender, LiveOperationCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                _contentList.Clear();
                List<object> data = (List<object>)e.Result["data"];
                foreach (IDictionary<string, object> content in data)
                {
                    string name = (string)content["name"];
                    //dumpContentToDebug(name, content);
                    SkyDriveContent skyContent = new SkyDriveContent
                    {
                        Name = name,
                        Id = (string)content["id"],
                        Type = (string)content["type"],
                        ParentId = (string)content["parent_id"],
                        Ext = Path.GetExtension(name),
                        Size = (int)content["size"],
                        CreatedTime = DateTime.Parse((string)content["created_time"]),
                        UpdatedTime = DateTime.Parse((string)content["updated_time"])
                    };
                    if (content.ContainsKey("count"))
                    {
                        skyContent.Count = (int)content["count"];
                    }
                    //if (skyContent.Type == "folder" || (skyContent.Type == "file" && skyContent.Ext.ToUpper() == ".TXT"))
                    //if (skyContent.IsTextFile || skyContent.IsTextFile)
                    {
                        _contentList.Add(skyContent);
                    }
                    //ContentList.Add(skyContent.Name);
                }

                //this.contentList.Items.Clear();
                this.contentList.ItemsSource = _contentList.OrderBy(x => x.Name).OrderBy(x => x.SortPriority);
            }
        }


        private void TextBlock_Tap(object sender, GestureEventArgs e)
        {
            SkyDriveContent x = contentList.SelectedItem as SkyDriveContent;
            if (x != null)
            {
                if (x.Type == "folder")
                {
                    this.NavigationService.Navigate(new Uri(string.Format("/SkydriveBrowseGet.xaml?folderId={0}&displayName={1}&fileName={2}&fullPath={3}\\{1}&listName={4}&xmlFile={5}", x.Id, x.Name, this.txtFileName.Text, this.txtFullPath.Text, this.PageTitle.Text, _xmlFile), UriKind.Relative));
                }
                else
                {
                    selectedItem = x;
                    txtFileName.Text = x.Name;
                    //MessageBox.Show(string.Format("Write code to handle opening {0}: {1} of type {2}", x.Id, x.Name, x.Type));
                }
            }
        }

        private void _signInButton_SessionChanged(object sender, Microsoft.Live.Controls.LiveConnectSessionChangedEventArgs e)
        {
            if (e.Status == LiveConnectSessionStatus.Connected)
            {
                if (App.LiveSession != e.Session)
                {
                    _client = new LiveConnectClient(e.Session);
                    App.LiveSession = e.Session;

                    _client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(OnGetCompleted);
                    _client.GetAsync("me", null);
                }
            }
            else
            {
                _client = null;
            }
        }

        private void OnGetCompleted(object sender, LiveOperationCompletedEventArgs e)
        {
            loadFolder(_folderName);
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            //if ((_contentList.Where(x => x.Name.ToLower() == txtFileName.Text.ToLower()).Count() == 0) ||
            //    MessageBox.Show("File Already Exists", "Title?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            //{
            //    var _storageHelper = new StorageHelper<Suggestion>(_xmlFile);
            //    var s = ListHelper.ConvertSuggestionListToStreamy(_storageHelper.GetList());
            //    _client.UploadCompleted += new EventHandler<LiveOperationCompletedEventArgs>(_client_UploadCompleted);
            //    _client.UploadAsync(_folderName, txtFileName.Text, s, OverwriteOption.Overwrite);
            //}
            MessageBox.Show("Do Select and dowload here", "Improv Suggester", MessageBoxButton.OK);
        }

        void _client_UploadCompleted(object sender, LiveOperationCompletedEventArgs e)
        {
            MessageBox.Show("Saved");
        }
    }
}