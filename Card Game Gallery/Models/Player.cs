using System;
using System.Collections.Generic;
using System.Text;

namespace Card_Game_Gallery.Models
{
    [Serializable]
    public class Player
    {
        public string name;
        public List<Card> cards;
        public int score;
        public bool isAi;

        /// <summary>
        /// The constructor for a player for any game
        /// </summary>
        /// <param name="name">The name the player chose to go by</param>
        /// <param name="isAi">Flag for if the player is an ai or not</param>
        /// <param name="cards">What cards the player currenty has</param>
        /// <param name="points">How many points the player has</param>
        public Player(string name, bool isAi=false, List<Card> cards = null, int score = 0)
        {
            this.name = name;
            this.isAi = isAi;
            this.cards = cards == null ? new List<Card>() : cards;
            this.score = score;
        }
    }
}
