using Card_Game_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Card_Game_Gallery.Games.Blackjack
{
    [Serializable]
    public class BlackjackSaveGame
    {
        private const int MIN_PLAYERS = 1;
        private const int MAX_PLAYERS = 5;

        public Deck Deck { get; set; }

        public Player House { get; set; }

        public Player[] Players { get; set; }

        /// <summary>
        /// Constructor to use when  creating a new game of Blackjack
        /// </summary>
        /// <param name="players"></param>
        public BlackjackSaveGame(Player[] players)
        {
            if (players.Length < MIN_PLAYERS || players.Length > MAX_PLAYERS)
            {
                throw new ArgumentException($"players must contain at least {MIN_PLAYERS} player(s) and at most {MAX_PLAYERS} players");
            }
            Deck = new Deck();
            House = new Player("House", true, new List<Card>(), 0);
            Players = players;
        }

        public BlackjackSaveGame(Deck deck, Player house, Player[] players)
        {
            if (players.Length < MIN_PLAYERS || players.Length > MAX_PLAYERS)
            {
                throw new ArgumentException($"players must contain at least {MIN_PLAYERS} player(s) and at most {MAX_PLAYERS} players");
            }
            Deck = new Deck();
            House = new Player("House", true, new List<Card>(), 0);
            Players = players;
        }
    }
}
