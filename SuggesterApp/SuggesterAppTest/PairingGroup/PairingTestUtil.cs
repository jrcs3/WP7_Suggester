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
using PairingTools;

namespace SuggesterAppTest.PairingGroup
{
    internal class PairingTestUtil
    {
        internal static int MatchesInPairing(Pairing p1, Pairing p2)
        {
            int matchCount = 0;
            foreach (Player pl1 in p1.Participants)
            {
                foreach (Player pl2 in p2.Participants)
                {
                    if (pl1 == pl2)
                    {
                        ++matchCount;
                    }
                }
            }
            return matchCount;
        }

    }
}
