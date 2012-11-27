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
    public class PairingEvenTests
    {
        [TestMethod]
        public void PickPairingGetNextParingNumber()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = false, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null}
            };

            Random random = new Random();
            int randomNumber = random.Next(0, 10000);
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers,
                ParingNumber = randomNumber
            };
            //Pairing p = PairingUtil.CreatePairing(randomNumber, myPlayers);
            Pairing p = pp.CreatePairing();

            Assert.AreEqual(randomNumber + 1, p.ParingNumber);
        }
        [TestMethod]
        public void PickPairingCountOfTwo()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = false, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers
            };
            //Pairing p = PairingUtil.CreatePairing(randomNumber, myPlayers);
            Pairing p = pp.CreatePairing();
            //Pairing p = PairingUtil.CreatePairing(42, myPlayers);

            Assert.AreEqual(2, p.Participants.Count);
        }
        [TestMethod]
        public void PickPairingLastPairingSet42()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = false, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers,
                ParingNumber = 42
            };
            Pairing p = pp.CreatePairing();

            Assert.AreEqual(43, p.Participants[0].LastParing);
            Assert.AreEqual(43, p.Participants[1].LastParing);
        }

        [TestMethod]
        public void PickPairingLastPairingSet0()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = false, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers
            };
            Pairing p = pp.CreatePairing();

            Assert.AreEqual(1, p.Participants[0].LastParing);
            Assert.AreEqual(1, p.Participants[1].LastParing);
        }

        [TestMethod]
        public void PickPairingGenerationPicks6Ignore2()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = false, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 6, ElgibleForPick = false, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers
            };

            Assert.AreEqual(2, pp.GenerationOddPicks);
            Assert.AreEqual(2, pp.GenerationEvenPicks);
            Assert.IsTrue(pp.IsEven);
        }

        [TestMethod]
        public void PickPairingGenerationPicks6Ignore0()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 6, ElgibleForPick = true, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers
            };

            Assert.AreEqual(3, pp.GenerationOddPicks);
            Assert.AreEqual(3, pp.GenerationEvenPicks);
            Assert.IsTrue(pp.IsEven);
        }

        [TestMethod]
        public void PickPairingGenerationPicks8Itnore1()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = false, LastParing = null},
                new Player {PlayerNumber = 6, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 7, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 8, ElgibleForPick = true, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers
            };

            Assert.AreEqual(3, pp.GenerationOddPicks);
            Assert.AreEqual(4, pp.GenerationEvenPicks);
            Assert.IsFalse(pp.IsEven);
        }

        [TestMethod]
        public void PickPairingLastPairingTwoPicksNoDupe5Ignore1Picks2()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = false, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers
            };
            Assert.AreEqual(1, pp.Generation);
            Pairing p1 = pp.CreatePairing();
            Assert.AreEqual(1, pp.ParingNumber);
            Assert.AreEqual(1, pp.Generation);
            Pairing p2 = pp.CreatePairing();
            Assert.AreEqual(2, pp.ParingNumber);
            Assert.AreEqual(2, pp.Generation);

            Assert.IsNotNull(myPlayers[0].LastParing);
            Assert.IsNotNull(myPlayers[1].LastParing);
            Assert.IsNull(myPlayers[2].LastParing);
            Assert.IsNotNull(myPlayers[3].LastParing);
            Assert.IsNotNull(myPlayers[4].LastParing);
        }

        [TestMethod]
        public void PickPairingLastPairingTwoPicksNoDupe6Ignore0Picks3()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 6, ElgibleForPick = true, LastParing = null}
            };
            PairingPicker pp = new PairingPicker
            {
                Players = myPlayers
            };
            Assert.AreEqual(1, pp.Generation);
            Pairing p1 = pp.CreatePairing();
            Pairing p2 = pp.CreatePairing();
            Pairing p3 = pp.CreatePairing();
            Assert.AreEqual(3, pp.ParingNumber);
            Assert.AreEqual(2, pp.Generation);
            Assert.IsNotNull(myPlayers[0].LastParing);
            Assert.IsNotNull(myPlayers[1].LastParing);
            Assert.IsNotNull(myPlayers[2].LastParing);
            Assert.IsNotNull(myPlayers[3].LastParing);
            Assert.IsNotNull(myPlayers[4].LastParing);
            Assert.IsNotNull(myPlayers[5].LastParing);
        }
        [TestMethod, Description("Special Case: With 4 players, the second pick of a generation will need to contain a single pick from the last pairing")]
        public void PickPairingLastPairingTwoPicksNoDupe5Ignore1Picks3()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = false, LastParing = null},
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
            Assert.AreEqual(1, PairingTestUtil.MatchesInPairing(p2, p3));
            Pairing p4 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p3, p4));
            Pairing p5 = pp.CreatePairing();
            Assert.AreEqual(1, PairingTestUtil.MatchesInPairing(p4, p5));
            Pairing p6 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p5, p6));
            Pairing p7 = pp.CreatePairing();
            Assert.AreEqual(1, PairingTestUtil.MatchesInPairing(p6, p7));

        }
        [TestMethod]
        public void PickPairingLastPairingTwoPicksNoDupe6Ignore1Picks4()
        {
            List<Player> myPlayers = new List<Player>
            {
                new Player {PlayerNumber = 1, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 2, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 3, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 4, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 5, ElgibleForPick = true, LastParing = null},
                new Player {PlayerNumber = 6, ElgibleForPick = true, LastParing = null}
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
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p1, p3));

            Pairing p4 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p3, p4));
            Pairing p5 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p4, p5));
            Pairing p6 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p5, p6));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p4, p6));

            Pairing p7 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p6, p7));
            Pairing p8 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p7, p8));
            Pairing p9 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p8, p9));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p7, p9));
        }

        [TestMethod]
        public void PickPairingLastPairingTwoPicksNoDupe8Ignore0Picks5()
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
                new Player {PlayerNumber = 8, ElgibleForPick = true, LastParing = null}
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
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p1, p3));
            Pairing p4 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p3, p4));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p2, p4));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p1, p4));

            Assert.IsNotNull(myPlayers[0].LastParing);
            Assert.IsNotNull(myPlayers[1].LastParing);
            Assert.IsNotNull(myPlayers[2].LastParing);
            Assert.IsNotNull(myPlayers[3].LastParing);
            Assert.IsNotNull(myPlayers[4].LastParing);
            Assert.IsNotNull(myPlayers[5].LastParing);
            Assert.IsNotNull(myPlayers[6].LastParing);
            Assert.IsNotNull(myPlayers[7].LastParing);

            Pairing p5 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p4, p5));
            Pairing p6 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p5, p6));
            Pairing p7 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p6, p7));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p5, p7));
            Pairing p8 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p7, p8));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p6, p8));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p5, p8));

            Assert.AreEqual(2, myPlayers[0].TimesPicked);
            Assert.AreEqual(2, myPlayers[1].TimesPicked);
            Assert.AreEqual(2, myPlayers[2].TimesPicked);
            Assert.AreEqual(2, myPlayers[3].TimesPicked);
            Assert.AreEqual(2, myPlayers[4].TimesPicked);
            Assert.AreEqual(2, myPlayers[5].TimesPicked);
            Assert.AreEqual(2, myPlayers[6].TimesPicked);
            Assert.AreEqual(2, myPlayers[7].TimesPicked);

        }
        [TestMethod]
        public void PickPairingLastPairingTwoPicksNoDupe10Ignore0Picks5()
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
                new Player {PlayerNumber = 10, ElgibleForPick = true, LastParing = null}
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
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p1, p3));
            Pairing p4 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p3, p4));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p2, p4));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p1, p4));
            Pairing p5 = pp.CreatePairing();
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p4, p5));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p3, p5));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p2, p5));
            Assert.AreEqual(0, PairingTestUtil.MatchesInPairing(p1, p5));

            Assert.IsNotNull(myPlayers[0].LastParing);
            Assert.IsNotNull(myPlayers[1].LastParing);
            Assert.IsNotNull(myPlayers[2].LastParing);
            Assert.IsNotNull(myPlayers[3].LastParing);
            Assert.IsNotNull(myPlayers[4].LastParing);
            Assert.IsNotNull(myPlayers[5].LastParing);
            Assert.IsNotNull(myPlayers[6].LastParing);
            Assert.IsNotNull(myPlayers[7].LastParing);
            Assert.IsNotNull(myPlayers[8].LastParing);
            Assert.IsNotNull(myPlayers[9].LastParing);
        }
    }
}
