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

namespace PairingTools
{
    public class Player
    {
        //public Player()
        //{
        //    LastPickGeneration = -1;
        //}
        public int PlayerNumber { get; set; }
        public bool ElgibleForPick { get; set; }
        public int? LastParing { get; set; }
        public int TimesPicked { get; set; }
        public int LastPickGeneration { get; set; }
    }
}
