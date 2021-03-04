using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Card_Game_Gallery.Models
{
    [Serializable]
    public class Player : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<Card> cards;
        public bool isAi;

        private string _name;

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                FieldChanged();
            }
        }

        private int _score;

        public int score
        {
            get { return _score; }
            set
            {
                _score = value;
                FieldChanged();
            }
        }

        /// <summary>
        /// The constructor for a player for any game
        /// </summary>
        /// <param name="name">The name the player chose to go by</param>
        /// <param name="isAi">Flag for if the player is an ai or not</param>
        /// <param name="cards">What cards the player currenty has</param>
        /// <param name="points">How many points the player has</param>
        public Player(string name, bool isAi = false, List<Card> cards = null, int score = 0)
        {
            this.name = name;
            this.isAi = isAi;
            this.cards = cards == null ? new List<Card>() : cards;
            this.score = score;
        }

        protected void FieldChanged([CallerMemberName] string field = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
        }
    }
}
