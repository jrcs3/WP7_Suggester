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

namespace LameSuggester
{
    public partial class AddEditSuggestionPage : PhoneApplicationPage
    {
        private string _listName;
        public string ListName
        {
            get { return _listName; }
            set
            {
                _listName = value;
                PageTitle.Text = string.Format("new {0}", _listName);
            }
        }
        public AddEditSuggestionPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            addSuggestionCtl1.FileName = this.NavigationContext.QueryString["xmlFile"];
            ListName = Uri.UnescapeDataString(this.NavigationContext.QueryString["listName"]);
            if (this.NavigationContext.QueryString.ContainsKey("suggestionId"))
            {
                string sis = this.NavigationContext.QueryString["suggestionId"];
                int suggestionId;
                if (int.TryParse(sis, out suggestionId))
                {
                    addSuggestionCtl1.SuggestionId = suggestionId;
                }
                //MessageBox.Show(string.Format("SuggestionId: {0}", sis));
            }
            base.OnNavigatedTo(e);
        }

        private void addSuggestionCtl1_Saved(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void savedButton_Click(object sender, EventArgs e)
        {
            addSuggestionCtl1.DoSave();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            addSuggestionCtl1.DoDelete();
        }

        private void addSuggestionCtl1_Deleted(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}