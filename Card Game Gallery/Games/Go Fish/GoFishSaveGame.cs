using Card_Game_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Card_Game_Gallery.Games.Go_Fish
{
    // This class will hold the state of the game, ex: how much time left, number of players, the deck...
    [Serializable]
    public class GoFishSaveGame
    {
        public List<GoFishPlayer> players; // Holds the list of players in the game
        public Deck deck; // Holds the state of the deck
        public GoFishPlayer currentPlayer; // Holds the current player to make their turn


        /// <summary>
        /// Constructor for what what a game save file requires to correctly save the state of a Go Fish game
        /// </summary>
        /// <param name="players">The list of players in the game</param>
        /// <param name="deck">State of the deck in the game</param>
        /// <param name="currentPlayer">Saving the current player that is to make their turn</param>
        public GoFishSaveGame(List<GoFishPlayer> players, Deck deck, GoFishPlayer currentPlayer)
        {
            this.players = players;
            this.deck = deck;
            this.currentPlayer = currentPlayer;
        }

    }
}
