using Card_Game_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Card_Game_Gallery.Games.War
{
    // Class holds all of the necessary things for saving a game of War, for example, turn, players list...
    [Serializable]
    public class WarSaveGame
    {
        private const int MAX_PLAYERS = 2;

        public Player[] Players { get; set; }

        public Deck Deck { get; set; }

        /// <summary>
        /// Creates a new instance of a game of War
        /// </summary>
        /// <param name="playingWithComputer"></param>
        /// <param name="players">Expects an array containing 2 players unless <c>playingwithComputer</c> is true in which case 1 player is expected</param>
        public WarSaveGame(Player[] players)
        {
            Deck = new Deck();
            if (players.Length > MAX_PLAYERS || players.Length == 0)
            {
                throw new ArgumentException($"players needs to contain at most {MAX_PLAYERS} players");
            }
            Players = players;
        }

        private void SetupPlayers(Player[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                Players[i] = players[i];
            }
        }
    }
}
