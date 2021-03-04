using Card_Game_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Card_Game_Gallery.Games.Go_Fish
{
    /// <summary>
    /// Interaction logic for PlayGoFishWindow.xaml
    /// </summary>
    public partial class PlayGoFishWindow : Window
    {
        Window callingWindow;
        bool gameFinished = false;
        string saveGamePath = ""; // If user opens a previously saved game, saving the game agian should go to the same path

        // Things for the game
        List<GoFishPlayer> playersList;
        Deck deck;
        GoFishPlayer currentPlayer;

        // This constructor means it is a new game, maight have to add more parameters to get stuff like list of players... anything that is needed to setup a go fish game
        public PlayGoFishWindow(List<GoFishPlayer> playersList, Window whatWindowCalledThisWindow)
        {
            InitializeComponent();
            callingWindow = whatWindowCalledThisWindow;
            this.playersList = playersList;
            deck = new Deck();
            Setup();
            DisplayCurrentPlayer();
        }

        // This constructor means a saved game is being loaded
        public PlayGoFishWindow(GoFishSaveGame save, string filePath = "", Window whatWindowCalledThisWindow = null)
        {
            InitializeComponent();
            callingWindow = whatWindowCalledThisWindow;
            this.playersList = save.players;
            this.deck = save.deck;
            this.currentPlayer = save.currentPlayer;
            saveGamePath = filePath;

        }

        void DisplayCurrentPlayer()
        {
            
            foreach (Card c in currentPlayer.cards)
            {
                Button button = new Button();
                button.Height = 190;
                button.Width = 130;
                button.Content = (int)c.Face;
                button.FontSize = 40;
                button.Margin = new Thickness(uniformLength: 1);
                wpCardDisplay.Children.Add(button);
            }

            txtCurrentPlayer.Text = $"Current Player: {currentPlayer.name}";
            txtNextPlayer.Text = $"Player To Ask: {GetNextPlayer().name}";
            txtMatches.Text = $"{currentPlayer.matchedCards.Count}";

            DisplayScores();

        }

        void DisplayScores()
        {
            TextBlock tb = new TextBlock();
            tb.Text = $"{currentPlayer.name}: {currentPlayer.score}";
            tb.FontSize = 22;
            tb.FontWeight = FontWeights.Bold;
            
            spnlScorePanel.Children.Add(tb);

            foreach(GoFishPlayer p in playersList)
            {
                if(p != currentPlayer)
                {
                    TextBlock t = new TextBlock();
                    t.FontSize = 22;
                    t.FontWeight = FontWeights.Bold;
                    t.Text = $"{p.name}: {p.score}";
                    spnlScorePanel.Children.Add(t);
                }
            }
        }



        void Setup()
        {
            if(currentPlayer == null) { currentPlayer = playersList[0]; }
            deck.Shuffle();
            // Give everyone cards
            foreach(GoFishPlayer player in playersList)
            {
                for(int count = 0; count< 7; count++)
                {
                    player.cards.Add(deck.DrawCard()); // Giving each player 7 cards
                }
            }
        }

        GoFishPlayer GetNextPlayer()
        {
            if(playersList.IndexOf(currentPlayer) == playersList.Count - 1)
            {
                return playersList[0];
            }
            else
            {
                return playersList[playersList.IndexOf(currentPlayer) + 1];
            }
        }



        void GameLoop()
        {

        }










        // Asks if the user would like to quit, if so would they like to save the current game
        private bool askToClose()
        {
            if (MessageBox.Show("Do you really want to exit?", "Yes", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // If user wants to save the game before exiting
                if (MessageBox.Show("Would you like to save the game?", "Save", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Attempt to save the game
                    return SaveGame();
                }
                return true; // Means user wanted to exit but did not want to save the game
            }
            return false; // User does not want to exit
        }


        // To save the current game state
        private bool SaveGame()
        {
            // Creating a new save game object and sending it to logic class to be searlized
            if (!GoFishLogic.SaveGame(new GoFishSaveGame(playersList, deck, currentPlayer), saveGamePath))
            {
                MessageBox.Show("Error", "Failed to save", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        // Called when window is closing
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Only ask to save if the game is not yet finished
            if (!gameFinished)
            {
                if (!askToClose()) { e.Cancel = true; }
                else { callingWindow.Show(); } // Displaying the window that called this window
            }
            else { callingWindow.Show(); } // Displaying the window that called this window
        }

    }
}
