using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Card_Game_Gallery.Models
{
    [Serializable]
    public class Card : IComparable, IComparable<Card>
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
            ACE = 1, TWO = 2, THREE = 3, FOUR = 4, FIVE = 5, SIX = 6, SEVEN = 7, EIGHT = 8, NINE = 9, TEN = 10, JACK = 11, QUEEN = 12, KING = 13
        }

        public CardSuite Suite { get; set; }

        public CardFace Face { get; set; }

        public int BlackjackValue { get; set; }

        public bool Revealed { get; set; }

        public Card(CardSuite suite, CardFace face)
        {
            Suite = suite;
            Face = face;
            HandleBlackjackValue();
        }

        public void Flip()
        {
            Revealed = !Revealed;
        }

        /// <summary>
        /// Returns a numeric value representing which of the two cards are of a greater value based on their <c>Face</c>.
        /// Returns 1 if this card is of a greater value than the other card, returns -1 if this card is of a lesser value, and returns 0 if both cards are of equal value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareCardFaceValues(Card other)
        {
            if (Face == CardFace.ACE || other.Face == CardFace.ACE)
            {
                return Face == other.Face ? 0 : (Face > other.Face ? -1 : 1);
            }
            else
            {
                return Face == other.Face ? 0 : (Face > other.Face ? 1 : -1);
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }
            if (obj is Card)
            {
                return CompareTo(obj as Card);
            }
            else
            {
                return 1;
            }
        }

        public int CompareTo([AllowNull] Card other)
        {
            if (other is null)
            {
                return 1;
            }
            else
            {
                return Face == other.Face ? 0 : (Face > other.Face ? 1 : -1);
            }
        }

        private void HandleBlackjackValue()
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
