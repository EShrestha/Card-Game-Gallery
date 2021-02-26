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

        // This constructor means it is a new game, maight have to add more parameters to get stuff like list of players... anything that is needed to setup a go fish game
        public PlayGoFishWindow(Window whatWindowCalledThisWindow)
        {
            InitializeComponent();
            callingWindow = whatWindowCalledThisWindow;
        }

        // This constructor means a saved game is being loaded
        public PlayGoFishWindow(Window whatWindowCalledThisWindow, GoFishSaveGame gs)
        {
            InitializeComponent();
            callingWindow = whatWindowCalledThisWindow;
            // Do stuff with the gs(saved game) object to setup the game properly

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
            if(!GoFishLogic.SaveGame(new GoFishSaveGame(/***Passe int the required parameters to create a new game save object***/)))
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
