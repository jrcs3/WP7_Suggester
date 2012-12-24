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
    public class PairingOddTests
    {
        [TestMethod]
        public void PickPairingGenerationPicks5Itnore0()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers
            };

            Assert.AreEqual(2, pp.GenerationOddPicks);
            Assert.AreEqual(3, pp.GenerationEvenPicks);
            Assert.IsFalse(pp.IsEven);
        }

        [TestMethod]
        public void PickPairingLastPairingTwoPicksNoDupe5Ignore0Picks5()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers
            };

            Pairing p1 = pp.CreatePairing();
            Pairing p2 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p1, p2));
            Pairing p3 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p2, p3));
            Pairing p4 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p3, p4));
            Pairing p5 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p4, p5));

            Assert.AreEqual(2, myPlayers[0].TimesPicked);
            Assert.AreEqual(2, myPlayers[1].TimesPicked);
            Assert.AreEqual(2, myPlayers[2].TimesPicked);
            Assert.AreEqual(2, myPlayers[3].TimesPicked);
            Assert.AreEqual(2, myPlayers[4].TimesPicked);
        }
        [TestMethod]
        public void PickPairingLastPairingTwoPicksNoDupe7Ignore0Picks7()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 6, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 7, ElgibleForPick = true, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers
            };

            Pairing p1 = pp.CreatePairing();
            Pairing p2 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p1, p2));
            Pairing p3 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p2, p3));
            Pairing p4 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p3, p4));
            Pairing p5 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p4, p5));
            Pairing p6 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p5, p6));
            Pairing p7 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p6, p7));

            Assert.AreEqual(2, myPlayers[0].TimesPicked);
            Assert.AreEqual(2, myPlayers[1].TimesPicked);
            Assert.AreEqual(2, myPlayers[2].TimesPicked);
            Assert.AreEqual(2, myPlayers[3].TimesPicked);
            Assert.AreEqual(2, myPlayers[4].TimesPicked);
            Assert.AreEqual(2, myPlayers[5].TimesPicked);
            Assert.AreEqual(2, myPlayers[6].TimesPicked);
        }

        [TestMethod]
        public void PickPairingLastPairingTwoPicksNoDupe13Ignore0Picks13()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 6, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 7, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 8, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 9, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 10, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 11, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 12, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 13, ElgibleForPick = true, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers
            };

            Pairing p1 = pp.CreatePairing();
            Pairing p2 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p1, p2));
            Pairing p3 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p2, p3));
            Pairing p4 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p3, p4));
            Pairing p5 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p4, p5));
            Pairing p6 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p5, p6));
            Pairing p7 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p6, p7));
            Pairing p8 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p7, p8));
            Pairing p9 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p8, p9));
            Pairing p10 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p9, p10));
            Pairing p11 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p10, p11));
            Pairing p12 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p11, p12));
            Pairing p13 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p12, p13));

            Assert.AreEqual(2, myPlayers[0].TimesPicked);
            Assert.AreEqual(2, myPlayers[1].TimesPicked);
            Assert.AreEqual(2, myPlayers[2].TimesPicked);
            Assert.AreEqual(2, myPlayers[3].TimesPicked);
            Assert.AreEqual(2, myPlayers[4].TimesPicked);
            Assert.AreEqual(2, myPlayers[5].TimesPicked);
            Assert.AreEqual(2, myPlayers[6].TimesPicked);
            Assert.AreEqual(2, myPlayers[7].TimesPicked);
            Assert.AreEqual(2, myPlayers[8].TimesPicked);
            Assert.AreEqual(2, myPlayers[9].TimesPicked);
            Assert.AreEqual(2, myPlayers[10].TimesPicked);
            Assert.AreEqual(2, myPlayers[11].TimesPicked);
            Assert.AreEqual(2, myPlayers[12].TimesPicked);
        }
    }
}
