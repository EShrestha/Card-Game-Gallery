using System;
using System.Collections.Generic;
using System.Text;

namespace Card_Game_Gallery.Models
{
    public class GoFishPlayer : Player
    {
        public List<Card> matchedCards = new List<Card>();

        public GoFishPlayer(string name, bool isAi = false, List<Card> cards = null, int score = 0) : base(name,isAi,cards,score)
        {
        }
    }
}
