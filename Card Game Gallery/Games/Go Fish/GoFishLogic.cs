using Card_Game_Gallery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Card_Game_Gallery.Games.Go_Fish
{

    // Holds all of the logic for Go Fish, such as winCheck...
    public class GoFishLogic
    {
        private static object saveGamePath;


        // Saves the current state of the game
        public static bool SaveGame(GoFishSaveGame save, string saveGamePath)
        {
            try
            {
                // Save files name is the current time
                string time = DateTime.Now.ToString("T");

                time = time.Replace(':', '-');

                IFormatter formatter = new BinaryFormatter();
                System.IO.Directory.CreateDirectory(@"\CardGameGallery\Go Fish");
                string path = saveGamePath.Equals("") ? @$"\CardGameGallery\Go Fish\{time}.GoFish" : saveGamePath;
                Stream stream = new FileStream(@$"{path}", FileMode.Create, FileAccess.Write);

                formatter.Serialize(stream, save);
                stream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Checks if a player has a certain card (only face value)
        public bool playerHasCard(GoFishPlayer player, Card card)
        {
            foreach(Card c in player.cards)
            {
                if (c.Face == card.Face)
                {
                    return true;
                }
            }
            return false;
        }


        //
        public void getCardFromPlayer(GoFishPlayer askingPlayer, GoFishPlayer askedPlayer, Card card)
        {
            foreach(Card c in askedPlayer.cards) // Going through each card of the asked player's hand
            {
                if(c.Face == card.Face) // If the asked player's card matches the card the asking player wants...
                {
                    askingPlayer.matchedCards.Add(c); // Give card to asking player
                    askedPlayer.cards.Remove(c); // Remove card from asked palyer
                    seperateMatchingCards(askingPlayer);
                }
            }
        }

        // Seperates matching pairs into a different list
        public void seperateMatchingCards(GoFishPlayer player)
        {
            for(int i = 0; i<player.cards.Count; i++)
            {
                for(int j = i+1; j<player.cards.Count; j++)
                {
                    if(player.cards[i] == player.cards[j])
                    {
                        player.matchedCards.Add(player.cards[i]); // Moving card 1 to matchedCards list
                        player.matchedCards.Add(player.cards[j]); // Moving card 2 to matchedCards list
                        // Removing form players hand after moving card to seperate list
                        player.cards.RemoveAt(i);
                        player.cards.RemoveAt(j);
                        player.score++;
                        continue;
                    }
                }
            }
   
        }

        // Causes player if wrong to draw a card.
        public void drawCardForWrongGuess(GoFishPlayer player, Deck deck)
        {
            player.cards.Add(deck.drawCard); // Adds card from top of deck to said player's hand.
        }

        public bool compareDrawnCards(GoFishPlayer player, Deck deck)
        {
            if (player.cards[player.cards.Count - 1] == deck.peek)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
