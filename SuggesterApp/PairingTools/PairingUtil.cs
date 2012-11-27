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
using System.Collections.Generic;

namespace PairingTools
{
    public static class PairingUtil
    {
        //public static Pairing CreatePairing(ref int lastParingNumber, List<Player> players, int playersInGroup = 2)
        //{
        //    //throw new NotImplementedException();
        //    int paringNumber = lastParingNumber + 1;
        //    lastParingNumber = paringNumber;
        //    Player player1 = players[0];
        //    player1.LastParing = paringNumber;
        //    Player player2 = players[1];
        //    player2.LastParing = paringNumber;
        //    return new Pairing
        //    {
        //        ParingNumber = paringNumber,
        //        Participants = new List<Player>
        //        {
        //            player1, 
        //            player2
        //        }
        //    };
        //}
        public static int getGeneration(PairingPicker pp)
        {
            return 0;
        }
    }
}
