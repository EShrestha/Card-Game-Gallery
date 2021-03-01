using Card_Game_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Card_Game_Gallery.Games.War
{
    public static class WarLogic
    {
        private const int CARDS_FOR_WAR = 1;

        public static void DealCards(WarSaveGame war)
        {
            war.Deck.Shuffle();
            int pIndex = 0;
            while (!war.Deck.IsDeckEmpty())
            {
                Card temp = war.Deck.DrawCard();
                war.Players[pIndex].cards.Add(temp);
                if (pIndex == war.Players.Length - 1)
                {
                    pIndex = 0;
                }
                else
                {
                    pIndex++;
                }
            }
        }

        /// <summary>
        /// Returns a <c>List</c> of a <c>Stack</c> of cards with the first index sharing the same index as the position of the player in <c>WarSaveGame.Players</c>
        /// </summary>
        /// <param name="war"></param>
        /// <returns></returns>
        public static List<List<Card>> GetPlayersCardsForWar(WarSaveGame war)
        {
            List<List<Card>> cfw = new List<List<Card>>();
            for (int i = 0; i < war.Players.Length; i++)
            {
                cfw.Add(new List<Card>());
            }
            HandleGettingCardsForWar(war, cfw, CARDS_FOR_WAR);
            return cfw;
            
        }

        // Saves teh current state of the game
        public static bool SaveGame(WarSaveGame gs)
        {
            // Only job is to serialize the gs(game save) object to some path
            throw new NotImplementedException();
        }

        private static void HandleGettingCardsForWar(WarSaveGame war, List<List<Card>> cfw, int cardsToGet)
        {
            for (int i = 0; i < cfw.Count; i++)
            {
                for (int j = 0; j < cardsToGet; j++)
                {
                    if (cfw[i] != null)
                    {
                        // Get top card from player's stack of cards
                        // Remove from player's stack
                        // Add to cfw
                    }
                }
            }
        }

        public bool isWar(Card p1Card,Card p2Card) {
            if(p1Card.Face == p2Card.Face) {
                return true;
            }
            return false;
        }

        public void HandleWar() {
            //get each players card in the list
            while(isWar()) {
                
            }
        }
    }
}
