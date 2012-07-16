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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuggesterTools;

namespace SuggesterAppTest
{
    [TestClass]
    public class ListTests
    {
        [TestMethod, Description("Get third adjective")]
        public void SuggesterAppTest_GetThirdAdjectiveFromStringList()
        {
            var list = ListHelper.GetListFromAssemblyResources("Lists", "Adjective.txt");
            Assert.AreEqual("abrasive", list[2]);
        }
        [TestMethod, Description("Get third adjective")]
        public void SuggesterAppTest_GetThirdAdjectiveFromSuggestionList()
        {
            var list = ListHelper.ReadResourceFile("Lists", "Adjective.txt");
            Assert.AreEqual("abrasive", list[2].Text);
        }
    }
}
