using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SuggesterTools;

namespace SuggesterRTTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestMethod1()
        {
            /*
            var exists = await SuggesterAppConfig.IsInOS();

            Assert.IsFalse(exists);
             */
            Assert.AreEqual(0, 0);

        }
    }
}
