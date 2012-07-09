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
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuggesterAppTest
{
    [TestClass]
    public class BasicTests : SilverlightTest
    {
        [TestMethod]
        public void AlwaysPass()
        {
            Assert.IsTrue(true, "method intended to always pass");
        }

        [TestMethod, Ignore]
        [Description("This test always fails intentionally")]
        public void AlwaysFail()
        {
            Assert.IsFalse(true, "method intended to always fail");
        }
    }
}
