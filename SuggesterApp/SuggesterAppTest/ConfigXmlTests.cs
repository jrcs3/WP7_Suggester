using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SuggesterTools;
using System.Reflection;
using System.Xml.Linq;
using System.IO.IsolatedStorage;
using System.Collections.Generic;


namespace SuggesterAppTest
{
    [TestClass]
    public class ConfigXmlTests
    {
        private const string CONFIG_XML_FILE_NAME = "Config.xml";
        private const string RESOURCE_STREAM_FORMAT = "SuggesterAppTest.{0}";


        [TestMethod, Description("Read Color Mode")]
        public void ReadColorMode01()
        {
            var config = new SuggesterAppConfig();
            using (Stream stream = getResoureFileStream(CONFIG_XML_FILE_NAME))
            {
                config.LoadFromStream(stream);
                Assert.AreEqual(ColorMode.Traditional, config.ColorMode);
            }
        }
        [TestMethod, Description("Read SourceUrl of First File")]
        public void ReadFirstFileUri01()
        {
            var config = new SuggesterAppConfig();
            using (Stream stream = getResoureFileStream(CONFIG_XML_FILE_NAME))
            {
                config.LoadFromStream(stream);
                Assert.AreEqual("Object.xml", config.Lists[0].SourceUri);               
            }
        }
        [TestMethod, Description("Get List Count (expect 5)")]
        public void ReadListContains5Items01()
        {
            var config = new SuggesterAppConfig();
            using (Stream stream = getResoureFileStream(CONFIG_XML_FILE_NAME))
            {
                config.LoadFromStream(stream);
                Assert.AreEqual(5, config.Lists.Count);
            }
        }

        [TestMethod]
        public void CompareXml01()
        {
            SuggesterAppConfig config = new SuggesterAppConfig();
            using (Stream stream = getResoureFileStream(CONFIG_XML_FILE_NAME))
            {
                XDocument docA = XDocument.Load(stream);
                config.LoadFromXml(docA);
                XDocument docB = config.ToXml();

                bool areEqual = areXmlDocsEqual(docA, docB);
                Assert.IsTrue(areEqual);
            }
        }

        [TestMethod]
        public void AddListToXml01()
        {
            SuggesterAppConfig config = new SuggesterAppConfig();
            using (Stream stream = getResoureFileStream(CONFIG_XML_FILE_NAME))
            {
                config.LoadFromStream(stream);
                int itemCountBefore = config.Lists.Count();
                SuggestionList newList = new SuggestionList
                {
                    HeaderText = "New List",
                    ListName = "New List",
                    PluralName = "List Items",
                    SingularName = "List Item",
                    HistoryCount = 42,
                    ReadOnly = false,
                    ListSource = ListSourceType.SkyDrive,
                    SourceUri = "foo/list.txt",
                    ListDate = DateTime.Today,
                    SortPriority = 100,
                    IsVisible = false
                };
                config.Lists.Add(newList);
                int itemCountAfter = config.Lists.Count();
                Assert.AreEqual(itemCountBefore + 1, itemCountAfter);
            }
        }

        [TestMethod]
        public void RemoveFromListToXml01()
        {
            SuggesterAppConfig config = new SuggesterAppConfig();
            using (Stream stream = getResoureFileStream(CONFIG_XML_FILE_NAME))
            {
                config.LoadFromStream(stream);
                int itemCountBefore = config.Lists.Count();
                SuggestionList doomedList = config.Lists[2];
                config.Lists.Remove(doomedList);
                int itemCountAfter = config.Lists.Count();
                Assert.AreEqual(itemCountBefore - 1, itemCountAfter);
            }
        }

        [TestMethod]
        public void IsInISBeforeAndAfterSave01()
        {
            SuggesterAppConfig config = new SuggesterAppConfig();
            if (SuggesterAppConfig.IsInOS())
            {
                IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();
                isoStorage.DeleteFile(config.GetFileName());
            }
            Assert.IsFalse(SuggesterAppConfig.IsInOS());
            using (Stream stream = getResoureFileStream(CONFIG_XML_FILE_NAME))
            {
                config.LoadFromStream(stream);
                config.SaveXmlToFileInIS();
                Assert.IsTrue(SuggesterAppConfig.IsInOS());
            }
        }

        [TestMethod]
        public void LoadListFromIS01()
        {
            SuggesterAppConfig config = new SuggesterAppConfig();
            if (!SuggesterAppConfig.IsInOS())
            {
                using (Stream stream = getResoureFileStream(CONFIG_XML_FILE_NAME))
                {
                    config.LoadFromStream(stream);
                    config.SaveXmlToFileInIS();
                }
            }
            bool loaded = config.LoadSuggestionListFromIS();
            Assert.IsTrue(loaded);
        }

        [TestMethod]
        public void LoadListFromResources01()
        {
            SuggesterAppConfig config = new SuggesterAppConfig();
            bool loaded = config.LoadSuggestionListFromResources();
            Assert.IsTrue(loaded);
        }

        [TestMethod]
        public void LoadListFromResources()
        {
            SuggesterAppConfig list = ListHelper.GetSuggesterAppConfig("Lists");
            Assert.AreEqual(ColorMode.Traditional, list.ColorMode);
        }

        private static bool areXmlDocsEqual(XDocument docA, XDocument docB)
        {
            bool areEqual = true;
            var rootNameA = docA.Root.Name;
            var rootNameB = docB.Root.Name;
            var equalRootNames = rootNameB.Equals(rootNameA);

            var descendantsA = docA.Root.Descendants();
            var descendantsB = docB.Root.Descendants();
            for (int i = 0; i < descendantsA.Count(); i++)
            {
                var descendantA = descendantsA.ElementAt(i);
                var descendantB = descendantsB.ElementAt(i);
                var equalChildNames = descendantA.Name.Equals(descendantB.Name);

                var valueA = descendantA.Value;
                var valueB = descendantB.Value;
                if (!valueA.Equals(valueB))
                {
                    areEqual = false;
                    break;
                }
            }
            return areEqual;
        }
        private Stream getResoureFileStream(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format(RESOURCE_STREAM_FORMAT, fileName));
        }
    }
}
