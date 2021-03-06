﻿using System;
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
using System.IO;

namespace SuggesterAppTest
{
    [TestClass]
    public class ListTests
    {
        private const string FILENAME = "FemaleName.txt";
        private const string EXPECTED_ITEM = "Ada";
        //private const string FILENAME = "Adjective.txt";
        //private const string EXPECTED_ITEM = "abrasive";
        [TestMethod, Description("Get third adjective")]
        public void SuggesterAppTest_GetThirdAdjectiveFromStringList()
        {
            var list = ListHelper.GetListFromAssemblyResources("Lists", FILENAME);
            Assert.AreEqual(EXPECTED_ITEM, list[2]);
        }
        [TestMethod, Description("Get third adjective")]
        public void SuggesterAppTest_GetThirdAdjectiveFromSuggestionList()
        {
            var list = ListHelper.ReadResourceFile("Lists", FILENAME);
            Assert.AreEqual(EXPECTED_ITEM, list[2].Text);
        }
        [TestMethod]
        public void SuggesterAppTest_ConvertToText()
        {
            var list = ListHelper.ReadResourceFile("Lists", FILENAME);
            string[] textLinesAsStringArray = ListHelper.ConvertSuggestionListToStringArray(list);
            //string text = list.to
            Assert.AreEqual(EXPECTED_ITEM, textLinesAsStringArray[2]);
        }
        [TestMethod]
        public void SuggesterAppTest_ConvertToStream()
        {
            var list = ListHelper.ReadResourceFile("Lists", FILENAME);
            using (Stream textLinesAsStream = ListHelper.ConvertSuggestionListToStreamy(list))
            {
                textLinesAsStream.Seek(0, SeekOrigin.Begin);
                StreamReader rdr = new StreamReader(textLinesAsStream);

                string str = rdr.ReadToEnd();

                string[] lines = str.Replace("\r", "").Split('\n');
                Assert.AreEqual(EXPECTED_ITEM, lines[2]);
            }
        }
        //[TestMethod]
        //public void SuggesterAppTest_DeleteItemFromList()
        //{
        //    var list = ListHelper.ReadResourceFile("Lists", FILENAME);
        //    Assert.AreEqual(EXPECTED_ITEM, list[2].Text);
        //    list.Add(
        //}
    }
}
