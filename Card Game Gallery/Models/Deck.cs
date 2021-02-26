using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Card_Game_Gallery.Models
{
    [Serializable]
    public class Deck
    {
        // Contains the cards
        List<Card> deck;

        public bool IsChanged { get; set; } // Flag for if the deck has been touched(cards moved around) or not

        public Deck()
        {
            AddAllCards();
        }

        private void AddAllCards()
        {
            // Should add the cards to the deck in the same order each time Ace-King for all suits

            IsChanged = false;
        }

        public void Shuffle()
        {
            if (!(deck.Count > 0)) return; // Can't shuffle a empty deck
            deck = deck.OrderBy(card => Guid.NewGuid()).ToList();
            IsChanged = true;
        }

        public void UnShuffle()
        {
            if (!IsChanged) return; // Can't unshuffle a fresh deck
            deck.Clear();
            AddAllCards();

        }
    }
}
