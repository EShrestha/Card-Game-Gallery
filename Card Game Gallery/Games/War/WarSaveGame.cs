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
        public WarSaveGame(bool playingWithComputer, Player[] players)
        {
            Deck = new Deck();
            Players = new Player[MAX_PLAYERS];
            if (players.Length > MAX_PLAYERS || players.Length == 0)
            {
                throw new ArgumentException($"players needs to contain at most {MAX_PLAYERS} players");
            }
            if (playingWithComputer)
            {
                if (players.Length != MAX_PLAYERS - 1)
                {
                    throw new ArgumentException($"players needs to contain {MAX_PLAYERS - 1} players when playingWithComputer is {playingWithComputer}");
                }
                SetupPlayers(players);
                Players[MAX_PLAYERS - 1] = new Player("Computer", new List<Card>(), 0);
            }
            else
            {
                if (players.Length != MAX_PLAYERS)
                {
                    throw new ArgumentException($"players needs to contain {MAX_PLAYERS} players when playingWithComputer is {playingWithComputer}");
                }
                SetupPlayers(players);
            }
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
