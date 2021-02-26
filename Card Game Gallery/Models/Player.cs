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
        public int points;

        public Player(string name, List<Card> cards, int points = 0)
        {
            this.name = name;
            this.cards = cards;
            this.points = points;
        }
    }
}
