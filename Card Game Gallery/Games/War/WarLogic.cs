using Card_Game_Gallery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Card_Game_Gallery.Games.War
{
    public class WarLogic
    {
        public const int CARDS_FOR_WAR = 4;
        private const int CARDS_FOR_NORMAL_PLAY = 1;

        public void DealCards(WarSaveGame war)
        {
            war.Deck.Shuffle();
            int pIndex = 0;
            while (war.Deck.DeckNotEmpty())
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
        /// Returns a <c>List</c> of a <c>List</c> of cards with the first index sharing the same index as the position of the player in <c>WarSaveGame.Players</c>
        /// </summary>
        /// <param name="war"></param>
        /// <returns></returns>
        public List<List<Card>> GetPlayersCardsForWar(WarSaveGame war)
        {
            List<List<Card>> cfw = new List<List<Card>>();
            for (int i = 0; i < war.Players.Length; i++)
            {
                cfw.Add(new List<Card>());
            }
            HandleGettingCardsForWar(war, cfw, CARDS_FOR_WAR);
            return cfw;
            
        }

        public List<List<Card>> GetPlayersCardsForNormalPlay(WarSaveGame war)
        {
            List<List<Card>> cfw = new List<List<Card>>();
            for (int i = 0; i < war.Players.Length; i++)
            {
                cfw.Add(new List<Card>());
            }
            HandleGettingCardsForWar(war, cfw, CARDS_FOR_NORMAL_PLAY);
            return cfw;
            
        }

        // Saves teh current state of the game, returns whether the file was saved or not
        public bool SaveGame(WarSaveGame war, string saveGamePath)
        {
            try
            {
                // Save files name is the current time
                string time = DateTime.Now.ToString("T");

                time = time.Replace(':', '-');

                IFormatter formatter = new BinaryFormatter();
                System.IO.Directory.CreateDirectory(@"\CardGameGallery\War");
                string path = saveGamePath.Equals("") ? @$"\CardGameGallery\War\{time}.War" : saveGamePath;
                Stream stream = new FileStream(@$"{path}", FileMode.Create, FileAccess.Write);

                formatter.Serialize(stream, war);
                stream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void HandleGettingCardsForWar(WarSaveGame war, List<List<Card>> cfw, int cardsToGet)
        {
            for (int i = 0; i < cfw.Count; i++)
            {
                for (int j = 0; j < cardsToGet; j++)
                {
                    if (cfw[i] != null)
                    {
                        try
                        {
                            // Get top card from player's stack of cards
                            // Add to cfw
                            cfw[i].Add(war.Players[i].cards[0]);
                            // Remove from player's stack
                            war.Players[i].cards.RemoveAt(0);
                        }
                        catch (Exception) { }
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

        public void HandleWar(WarSaveGame war) {
            //get each players card in the list
            List<List<Card>> playersCards = GetPlayersCardsForWar(war);
            int count = 0;
            while(isWar(playersCards[0][count], playersCards[1][count])) {
                //go to the next pair of cards until they arent equal
                count++;
                continue;
            }

            //check which card is bigger in value and assign the winnings to the right player
            int winner;
            winner = playersCards[0][count].Face > playersCards[1][count].Face ?  0 : 1;

            //add cfw cards to the winning players stack
            foreach(List<Card> cl in playersCards) {
                foreach (Card c in cl)
                {
                    war.Players[winner].cards.Add(c);
                }
            }
        }

        public bool isPlayerCardsEmpty(WarSaveGame game)
        {
            foreach(Player p in game.Players)
            {
                if (p.cards.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
