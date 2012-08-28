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
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace SuggesterTools
{
    public class SuggesterAppConfig
    {
        public void LoadFromStream(Stream stream)
        {
            XDocument doc = XDocument.Load(stream);
            LoadFromXml(doc);
        }

        public void LoadFromXml(XDocument doc)
        {
            var config = doc.Element("Config");

            ColorMode = (ColorMode)Enum.Parse(typeof(ColorMode), config.Element("ColorMode").Value, true);

            Lists = loadLists(config.Element("Lists"));
        }

        private static List<SuggestionList> loadLists(XElement lists)
        {
            var listList = new List<SuggestionList>();
            foreach (var item in lists.Descendants("List"))
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
        public List<SuggestionList> Lists { get; private set; }
        public ColorMode ColorMode { get; set; }

        public XDocument ToXml()
        {
            XDocument doc = new XDocument();
            doc.Add(new XElement("Config",
                new XElement("ColorMode", this.ColorMode.ToString()),
                getListMarkup()));
            return doc;
        }

        private XElement getListMarkup()
        {
            XElement rVal = new XElement("Lists");
            foreach (var listIem in Lists)
            {
                rVal.Add(new XElement("List",
                    new XElement("HeaderText", listIem.HeaderText),
                    new XElement("ListName", listIem.ListName),
                    new XElement("PluralName", listIem.PluralName),
                    new XElement("SingularName", listIem.SingularName),
                    new XElement("HistoryCount", listIem.HistoryCount),
                    new XElement("ReadOnly", listIem.ReadOnly),
                    new XElement("ListSource", listIem.ListSource.ToString()),
                    new XElement("SourceUri", listIem.SourceUri),
                    new XElement("ListDate", listIem.ListDate),
                    new XElement("SortPriority", listIem.SortPriority),
                    new XElement("IsVisible", listIem.IsVisible)));
            }
            return rVal;
        }
    }
}
