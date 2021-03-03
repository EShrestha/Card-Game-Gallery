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
        public Stack<Card> deck = new Stack<Card>();

        public bool IsChanged { get; set; } // Flag for if the deck has been touched(cards moved around) or not

        // Getting a new deck
        public Deck()
        {
            AddAllCards();
        }

        // Makes a fresh deck
        private void AddAllCards()
        {
            // Should add the cards to the deck in the same order each time Ace-King for all suits
            for (int suit = 0; suit < 4; suit++) // There are four suits
            {
                for (int face = 13; face > 0; face--) // Each suit has 13 cards Ace(1)-King(13)
                {
                    Card card = new Card((Card.CardSuite)suit, (Card.CardFace)face);
                    deck.Push(card);
                }
            }

            IsChanged = false;
        }

        // Deck must have >=1 card to be not empty
        public bool DeckNotEmpty()
        {
            return (deck.Count > 0); // If the deck has atleast 1 card it is not empty
        }

        // Removes the top card from the deck and returns it
        public Card DrawCard()
        {
            Card temp = deck.Peek(); // Gettin the very top card of the deck
            deck.Pop(); // Removing the top card
            return temp; // Returning what card was drawn
        }

        // Randomly shuffles the deck
        public void Shuffle()
        {
            if (!DeckNotEmpty()) return; // Can't shuffle a empty deck
            deck = new Stack<Card>(deck.OrderBy(card => new Random().Next())); // Shuffling the deck
            IsChanged = true; // Shuffling the deck changes the card

        }

        // Makes the current deck brand new, with all cards in order
        public void UnShuffle()
        {
            if (!IsChanged) return; // Can't unshuffle a fresh deck
            deck.Clear(); // Removing all cards form the deck
            AddAllCards(); // Reseting the deck
        }
    }
}
