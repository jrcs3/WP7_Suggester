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
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Reflection;

namespace SuggesterTools
{
    public class SuggestionListHelper
    {
        //private static List<SuggestionList> LoadSuggestionListFromResources()
        //{
        //    string fileName = "LameSuggester.SampleData.Lists.xml";

        //    using (Stream stream = Assembly.GetExecutingAssembly()
        //       .GetManifestResourceStream(fileName))
        //    {
        //        return loadSuggestionListFromStream(stream);
        //    }
        //}

        public static List<SuggestionList> ReadResourceFile(string assemblyname, string fileName)
        {
            Assembly a = Assembly.Load(assemblyname);
            using (Stream stream = a.GetManifestResourceStream(ListHelper.getAssemblyFirstName(a) + "." + fileName))
            {
                return loadSuggestionListFromStream(stream);
            }
        }

        private static List<SuggestionList> loadSuggestionListFromStream(Stream stream)
        {
            XDocument doc = XDocument.Load(stream);
            var listList = new List<SuggestionList>();

            foreach (var item in doc.Descendants("List"))
            {
                var newItem = new SuggestionList
                {
                    HeaderText = item.Element("HeaderText").Value,
                    ListName = item.Element("ListName").Value,
                    PluralName = item.Element("PluralName").Value,
                    SingularName = item.Element("SingularName").Value,
                    HistoryCount = int.Parse(item.Element("HistoryCount").Value),
                    ReadOnly = bool.Parse(item.Element("ReadOnly").Value),
                    ListSource = (ListSourceType)Enum.Parse(typeof(ListSourceType), item.Element("ListSource").Value, true),
                    SourceUri = item.Element("SourceUri").Value,
                    ListDate = DateTime.Parse(item.Element("ListDate").Value),
                    SortPriority = int.Parse(item.Element("SortPriority").Value),
                    IsVisible = bool.Parse(item.Element("IsVisible").Value)
                };
                listList.Add(newItem);
            }
            return listList;
        }
    }
}
