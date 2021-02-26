using System;
using System.Collections.Generic;
using System.Text;

namespace Card_Game_Gallery.Models
{
    public class Card
    {
        public enum CardSuite
        {
            CLUBS, DIAMONDS, HEARTS, SPADES
        }

        /// <summary>
        /// CardFace has its value associated with the general value of the card in most card games.
        /// </summary>
        public enum CardFace
        {
            TWO = 2, THREE = 3, FOUR = 4, FIVE = 5, SIX = 6, SEVEN = 7, EIGHT = 8, NINE = 9, TEN = 10, JACK = 11, QUEEN = 12, KING = 13, ACE = 14
        }

        public CardSuite Suite { get; set; }

        public CardFace Face { get; set; }

        public int BlackjackValue { get; private set; }

        public bool Revealed { get; private set; }

        public Card(CardSuite suite, CardFace face)
        {
            Suite = suite;
            Face = face;
            handleBlackjackValue();
        }

        public void flip()
        {
            Revealed = !Revealed;
        }

        private void handleBlackjackValue()
        {
            switch (Face)
            {
                case CardFace.TWO:
                    BlackjackValue = (int)CardFace.TWO;
                    break;
                case CardFace.THREE:
                    BlackjackValue = (int)CardFace.THREE;
                    break;
                case CardFace.FOUR:
                    BlackjackValue = (int)CardFace.FOUR;
                    break;
                case CardFace.FIVE:
                    BlackjackValue = (int)CardFace.FIVE;
                    break;
                case CardFace.SIX:
                    BlackjackValue = (int)CardFace.SIX;
                    break;
                case CardFace.SEVEN:
                    BlackjackValue = (int)CardFace.SEVEN;
                    break;
                case CardFace.EIGHT:
                    BlackjackValue = (int)CardFace.EIGHT;
                    break;
                case CardFace.NINE:
                    BlackjackValue = (int)CardFace.NINE;
                    break;
                case CardFace.TEN:
                case CardFace.JACK:
                case CardFace.QUEEN:
                case CardFace.KING:
                    BlackjackValue = (int)CardFace.TEN;
                    break;
                case CardFace.ACE:
                    BlackjackValue = 11;
                    break;
                default:
                    throw new ArgumentException("an unexpected value was passed in for Face");
            }
        }
    }
}
