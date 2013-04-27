using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Linq;
//using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Search;
using System.Threading.Tasks;

namespace SuggesterTools
{
    public class SuggesterAppConfig
    {
        private const string CONFIG_ROOT_ELEMENT_NAME = "Config";
        private const string COLOR_MODE_ELEMENT_NAME = "ColorMode";
        private const string LISTS_GROUP_ELEMENT_NAME = "Lists";
        private const string LIST_SINGLE_ELEMENT_NAME = "List";
        private const string ID_SINGLE_ELEMENT_NAME = "Id";
        private const string LIST_FILE_NAME_ELEMENT_NAME = "ListFileName";
        private const string HEADER_TEXT_ELEMENT_NAME = "HeaderText";
        private const string LIST_NAME_ELEMENT_NAME = "ListName";
        private const string PLURAL_NAME_ELEMENT_NAME = "PluralName";
        private const string SINGULAR_NAME_ELEMENT_NAME = "SingularName";
        private const string HISTORY_COUNT_ELEMENT_NAME = "HistoryCount";
        private const string READ_ONLY_ELEMENT_NAME = "ReadOnly";
        private const string LIST_SOURCE_ELEMENT_NAME = "ListSource";
        private const string SOURCE_URI_ELEMENT_NAME = "SourceUri";
        private const string LIST_DATE_ELEMENT_NAME = "ListDate";
        private const string SORT_PRIORITY_ELEMENT_NAME = "SortPriority";
        private const string IS_VISIBLE_ELEMENT_NAME = "IsVisible";
        private const string IS_TRIAL_LIST_ELEMENT_NAME = "IsTrialList";

        private const string CONFIG_FILE_NAME = "Config.xml";
        private const string RESOURCE_NAME = "Lists";

        public void LoadFromStream(Stream stream)
        {
            XDocument doc = XDocument.Load(stream);
            LoadFromXml(doc);
        }

        public void LoadFromXml(XDocument doc)
        {
            XElement config = doc.Element(CONFIG_ROOT_ELEMENT_NAME);

            ColorMode = (ColorMode)Enum.Parse(typeof(ColorMode), config.Element(COLOR_MODE_ELEMENT_NAME).Value, true);

            Lists = loadLists(config.Element(LISTS_GROUP_ELEMENT_NAME));
        }

        public async void SaveXmlToFileInIS(string fileName = CONFIG_FILE_NAME)
        {
            TextWriter writer = null;
            try
            {
                //ApplicationData.Current.LocalFolder
                //IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();
                //StorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();
                var file = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(fileName, CreationCollisionOption.ReplaceExisting);
                //isoStorage.CreateFile(_fileName);
                //IsolatedStorageFileStream file = isoStorage.OpenFile(fileName, FileMode.Create);
                writer = new StreamWriter(file);

                StringWriter wr = new StringWriter();
                ToXml().Save(wr);

                string xmlText = wr.GetStringBuilder().ToString();//ToXml().ToString();
                writer.Write(xmlText);
                
                //xs.Serialize(writer, jogs);
                //writer.Close();
                //file.Close();
            }
            finally
            {
                if (writer != null)
                {
                    writer.Dispose();
                }
            }
        }

        public string GetFileName()
        {
            return CONFIG_FILE_NAME;
        }

        public async static Task<bool> IsInOS(string fileName = CONFIG_FILE_NAME)
        {
            //IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();
            var p = ApplicationData.Current.LocalFolder.Path;
            //return isoStorage.FileExists(fileName);
            //return File.Exis
            var folder = await StorageFolder.GetFolderFromPathAsync(ApplicationData.Current.LocalFolder.Path);
            //var folder = ApplicationData.Current.LocalFolder.Path;
            var files = await folder.GetFilesAsync(CommonFileQuery.OrderByName);
            var file = files.FirstOrDefault(x => x.Name == "fileName");
            if (file != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<SuggestionList> loadLists(XElement lists)
        {
            List<SuggestionList> listList = new List<SuggestionList>();
            foreach (XElement item in lists.Descendants(LIST_SINGLE_ELEMENT_NAME))
            {
                SuggestionList newItem = new SuggestionList
                {
                    Id = int.Parse(item.Element(ID_SINGLE_ELEMENT_NAME).Value),
                    ListFileName = item.Element(LIST_FILE_NAME_ELEMENT_NAME).Value,
                    HeaderText = item.Element(HEADER_TEXT_ELEMENT_NAME).Value,
                    ListName = item.Element(LIST_NAME_ELEMENT_NAME).Value,
                    PluralName = item.Element(PLURAL_NAME_ELEMENT_NAME).Value,
                    SingularName = item.Element(SINGULAR_NAME_ELEMENT_NAME).Value,
                    HistoryCount = int.Parse(item.Element(HISTORY_COUNT_ELEMENT_NAME).Value),
                    ReadOnly = bool.Parse(item.Element(READ_ONLY_ELEMENT_NAME).Value),
                    ListSource = (ListSourceType)Enum.Parse(typeof(ListSourceType), item.Element(LIST_SOURCE_ELEMENT_NAME).Value, true),
                    SourceUri = item.Element(SOURCE_URI_ELEMENT_NAME).Value,
                    ListDate = DateTime.Parse(item.Element(LIST_DATE_ELEMENT_NAME).Value),
                    SortPriority = int.Parse(item.Element(SORT_PRIORITY_ELEMENT_NAME).Value),
                    IsVisible = bool.Parse(item.Element(IS_VISIBLE_ELEMENT_NAME).Value),
                    IsTrialList = bool.Parse(item.Element(IS_TRIAL_LIST_ELEMENT_NAME).Value)
                };
                listList.Add(newItem);
            }
            return listList;
        }
        public List<SuggestionList> Lists { get; private set; }
        public ColorMode ColorMode { get; set; }

        public XDocument ToXml()
        {
            //XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            XDocument doc = new XDocument();
            doc.Add(                 
                new XElement(CONFIG_ROOT_ELEMENT_NAME,
                new XElement(COLOR_MODE_ELEMENT_NAME, this.ColorMode.ToString()),
                getListMarkup()));
            return doc;
        }

        private XElement getListMarkup()
        {
            XElement rVal = new XElement(LISTS_GROUP_ELEMENT_NAME);
            foreach (SuggestionList listIem in Lists)
            {
                rVal.Add(new XElement(LIST_SINGLE_ELEMENT_NAME,
                    new XElement(ID_SINGLE_ELEMENT_NAME, listIem.Id),
                    new XElement(LIST_FILE_NAME_ELEMENT_NAME, listIem.ListFileName),
                    new XElement(HEADER_TEXT_ELEMENT_NAME, listIem.HeaderText),
                    new XElement(LIST_NAME_ELEMENT_NAME, listIem.ListName),
                    new XElement(PLURAL_NAME_ELEMENT_NAME, listIem.PluralName),
                    new XElement(SINGULAR_NAME_ELEMENT_NAME, listIem.SingularName),
                    new XElement(HISTORY_COUNT_ELEMENT_NAME, listIem.HistoryCount),
                    new XElement(READ_ONLY_ELEMENT_NAME, listIem.ReadOnly),
                    new XElement(LIST_SOURCE_ELEMENT_NAME, listIem.ListSource.ToString()),
                    new XElement(SOURCE_URI_ELEMENT_NAME, listIem.SourceUri),
                    new XElement(LIST_DATE_ELEMENT_NAME, listIem.ListDate),
                    new XElement(SORT_PRIORITY_ELEMENT_NAME, listIem.SortPriority),
                    new XElement(IS_VISIBLE_ELEMENT_NAME, listIem.IsVisible),
                    new XElement(IS_TRIAL_LIST_ELEMENT_NAME, listIem.IsTrialList)
                ));
            }
            return rVal;
        }

        public bool LoadSuggestionListFromIS(string fileName = CONFIG_FILE_NAME)
        {
            /*
            try
            {
                IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();
                IsolatedStorageFileStream file = isoStorage.OpenFile(fileName, FileMode.OpenOrCreate);

                using (StreamReader reader = new StreamReader(file))
                {
                    string contents = reader.ReadToEnd();
                    XDocument doc = XDocument.Parse(contents);
                    LoadFromXml(doc);
                }
                file.Close();
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return true;
             */
            return false;
        }

        public bool LoadSuggestionListFromResources()
        {
            try
            {
                var x = ListHelper.GetSuggesterAppConfig(RESOURCE_NAME);
                ColorMode = x.ColorMode;
                Lists = x.Lists;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
