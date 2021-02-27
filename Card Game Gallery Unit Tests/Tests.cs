using NUnit.Framework;
using Card_Game_Gallery.Models;
using System;

namespace Card_Game_Gallery_Unit_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region Card Tests
        [Test]
        public void CardConstructor_Test()
        {
            Card.CardSuite[] suites = (Card.CardSuite[])Enum.GetValues(typeof(Card.CardSuite));
            Card.CardFace[] faces = (Card.CardFace[])Enum.GetValues(typeof(Card.CardFace));
            Card card = null;
            for (int i = 0; i < suites.Length; i++)
            {
                for (int j = 0; j < faces.Length; j++)
                {
                    card = new Card(suites[i], faces[j]);
                    Assert.AreEqual(card.Suite, suites[i], $"test failed for suit:{suites[i]}, face:{faces[i]}");
                    Assert.AreEqual(card.Face, faces[j], $"test failed for suit:{suites[i]}, face:{faces[i]}");
                    if (card.Face == Card.CardFace.ACE)
                    {
                        Assert.AreEqual(card.BlackjackValue, 11, $"test failed for suit:{suites[i]}, face:{faces[i]}");
                    }
                    else if (card.Face == Card.CardFace.JACK || card.Face == Card.CardFace.QUEEN || card.Face == Card.CardFace.KING)
                    {
                        Assert.AreEqual(card.BlackjackValue, (int)Card.CardFace.TEN, $"test failed for suit:{suites[i]}, face:{faces[i]}");
                    }
                    else
                    {
                        Assert.AreEqual(card.BlackjackValue, (int)card.Face, $"test failed for suit:{suites[i]}, face:{faces[i]}");
                    }
                }
            }
            Assert.Pass();
        }
        #endregion
    }
}