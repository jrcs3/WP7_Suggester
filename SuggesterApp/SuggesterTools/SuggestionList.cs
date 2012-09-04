using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SuggesterTools
{
    public class SuggestionList
    {
        public int Id { get; set; }
        public string HeaderText { get; set; }
        public string ListName { get; set; }
        public string PluralName { get; set; }
        public string SingularName { get; set; }
        public int HistoryCount { get; set; }
        public bool ReadOnly { get; set; }
        public ListSourceType ListSource { get; set; }
        public string SourceUri { get; set; }
        public DateTime ListDate { get; set; }
        public int SortPriority { get; set; }
        public bool IsVisible { get; set; }
        public bool IsTrialList { get; set; }
        public string IsVisiableString { get { return IsVisible ? "VISIBLE" : "Not Visible"; } }
        public bool AllowEditing { get { return !ReadOnly; } }

        public string FileName { get { return ListName + ".xml"; } }
    }
}
