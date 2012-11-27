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
using System.Linq;

namespace PairingTools
{
    public class PairingPicker
    {
        public PairingPicker()
        {
            Generation = 1;
        }

        public List<Player> Players { get; set; }
        public int ParingNumber { get; set; }
        public int Generation { get; set; }
        private int pickInGeneration;

        public bool IsEven
        {
            get
            {
                if (Players != null)
                {
                    var rVal = ((elegibleItemCount() % 2) == 0);
                    return rVal;
                }
                else
                {
                    return true;
                }
            }
        }

        public int GenerationEvenPicks
        {
            get
            {
                return GenerationOddPicks + (IsEven ? 0 : 1);
            }
        }

        public int GenerationOddPicks
        {
            get
            {
                if (Players != null)
                {
                    var rVal = elegibleItemCount() / 2;
                    return rVal;
                }
                else
                {
                    return 0;
                }
            }
        }

        private int elegibleItemCount()
        {
            return Players.Where(x => x.ElgibleForPick).Count();
        }

        public Pairing CreatePairing()
        {
            //int paringNumber = ParingNumber;

            ++ParingNumber;
            ++pickInGeneration;

            //int paringNumber = lastParingNumber + 1;
            //lastParingNumber = paringNumber;
            Player player1;
            Player player2;
            if (!IsEven && (isEvenGeneration()) && (pickInGeneration == 1))
            {
                player1 = Players.Where(x => (x.LastPickGeneration == Generation - 2)).Single();
                player1.LastParing = ParingNumber;
                player1.LastPickGeneration = Generation - 1;
                ++player1.TimesPicked;
            }
            else
            {
                player1 = pickPlayer();
            }
            player2 = pickPlayer();
            if (player1 == player2)
            {
                do
                {
                    player2 = pickPlayer();
                } while (player1 == player2);
            }
            var rVal = new Pairing
            {
                ParingNumber = ParingNumber,
                Participants = new List<Player>
                {
                    player1, 
                    player2
                }
            };
            if (IsTimeForNextGeneration())
            {
                ++Generation;
                pickInGeneration = 0;
            }
            return rVal;
        }

        private bool isEvenGeneration()
        {
            return (Generation % 2) == 0;
        }

        private bool IsTimeForNextGeneration()
        {
            if (isEvenGeneration())
            {
                return (pickInGeneration % GenerationEvenPicks) == 0;
            }
            else
            {
                return (pickInGeneration % GenerationOddPicks) == 0;
            }
        }

        private Player pickPlayer()
        {
            Player player;
            do
            {
                player = Players[randomPick()];
            } while (!player.ElgibleForPick || !isValidPick(player));
            player.LastParing = ParingNumber;
            player.LastPickGeneration = Generation;
            ++player.TimesPicked;
            return player;
        }

        private bool isValidPick(Player player)
        {
            // When there are 4 players AND this is the second pairing in a generation AND the first pick has already been made.
            if (Generation > 1 && pickInGeneration == 1 && (Players.Where(x => x.ElgibleForPick).Count() == 4) && Players.Where(x => x.LastParing == ParingNumber).Count() == 1)
            {
                // We must pick someone from the last pairing
                return !player.LastParing.HasValue || player.LastParing == (ParingNumber - 1);
                // Otherwise, the same two pairs will be repeaded forever.
            }
            else
            {
                return (!player.LastParing.HasValue || player.LastParing < (ParingNumber - 1)) && player.LastPickGeneration != Generation;
            }
        }

        private int randomPick()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, Players.Count());
            return randomNumber;
        }
    }
}
