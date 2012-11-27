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
using System.Collections.Generic;
using PairingTools;

namespace SuggesterAppTest.PairingGroup
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod, Description("Load Player List")]
        public void LoadPlayerListSimple()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = false, LastParing = 2},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = 1}
            };
        }

    }
}