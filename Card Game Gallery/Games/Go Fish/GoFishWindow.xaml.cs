using Card_Game_Gallery.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Responsible for asking user information about the game to set it up, such as pvp or pvc, number of players...
    /// </summary>
    public partial class GoFishWindow : Window
    {
        Window whatWindowCalledThisWindow;
        PlayGoFishWindow playGoFishWindow;
        int numOfPlayers = 1;
        List<Player> playersList = new List<Player>();
        GoFishSaveGame save;

        private FileStream stream;  // Used for deserilization
        IFormatter formatter = new BinaryFormatter(); // Used for deserilization

        public GoFishWindow(Window callingWindow)
        {
            System.Diagnostics.Debug.WriteLine("In here! A");
            InitializeComponent();
            whatWindowCalledThisWindow = callingWindow;
        }






        private void rb1_Checked(object sender, RoutedEventArgs e)
        {
            numOfPlayers = 1;

            p1.IsChecked = false;
            p1.IsEnabled = false;
            txtName1.IsEnabled = true;

            p2.IsChecked = true;
            p2.IsEnabled = false;
            txtName2.IsEnabled = true;

            p3.IsChecked = false;
            p3.IsEnabled = false;
            txtName3.IsEnabled = false;

            p4.IsChecked = false;
            p4.IsEnabled = false;
            txtName4.IsEnabled = false;
        }

        private void rb2_Checked(object sender, RoutedEventArgs e)
        {
            numOfPlayers = 2;

            p1.IsChecked = false;
            p1.IsEnabled = false;
            txtName1.IsEnabled = true;

            p2.IsChecked = false;
            p2.IsEnabled = true;
            txtName2.IsEnabled = true;

            p3.IsChecked = false;
            p3.IsEnabled = false;
            txtName3.IsEnabled = false;

            p4.IsChecked = false;
            p4.IsEnabled = false;
            txtName4.IsEnabled = false;
        }

        private void rb3_Checked(object sender, RoutedEventArgs e)
        {
            numOfPlayers = 3;
            p1.IsChecked = false;
            p1.IsEnabled = false;
            txtName1.IsEnabled = true;

            p2.IsChecked = false;
            p2.IsEnabled = true;
            txtName2.IsEnabled = true;

            p3.IsChecked = false;
            p3.IsEnabled = true;
            txtName3.IsEnabled = true;

            p4.IsChecked = false;
            p4.IsEnabled = false;
            txtName4.IsEnabled = false;
        }

        private void rb4_Checked(object sender, RoutedEventArgs e)
        {
            numOfPlayers = 4;

            p1.IsChecked = false;
            p1.IsEnabled = false;
            txtName1.IsEnabled = true;

            p2.IsChecked = false;
            p2.IsEnabled = true;
            txtName2.IsEnabled = true;

            p3.IsChecked = false;
            p3.IsEnabled = true;
            txtName3.IsEnabled = true;

            p4.IsChecked = false;
            p4.IsEnabled = true;
            txtName4.IsEnabled = true;
        }

        private void p1_Checked(object sender, RoutedEventArgs e)
        {
            // Required to compile
        }

        private void p2_Checked(object sender, RoutedEventArgs e)
        {
            // Required to compile
        }

        private void p3_Checked(object sender, RoutedEventArgs e)
        {
            // Required to compile
        }

        private void p4_Checked(object sender, RoutedEventArgs e)
        {
            // Required to compile
        }

        // Is called as the window is closing
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (!((bool)rb1.IsChecked | (bool)rb2.IsChecked | (bool)rb3.IsChecked | (bool)rb4.IsChecked)) { MessageBox.Show("Please select number of players"); return; }
            this.Hide();

            // If only one human player is playing
            if (numOfPlayers == 1)
            {
                // Creating a new player with witht he parameters (name, isAi, number of captures, is player AI?)
                playersList.Add(new Player(txtName1.Text));
                playersList.Add(new Player(txtName2.Text, true)); // If there is only one human player there must be an ai
            }
            else if (numOfPlayers == 2)
            {
                playersList.Add(new Player(txtName1.Text));
                playersList.Add(new Player(txtName2.Text, (bool)p2.IsChecked));
            }
            else if (numOfPlayers == 3)
            {
                playersList.Add(new Player(txtName1.Text));
                playersList.Add(new Player(txtName2.Text,(bool)p2.IsChecked));
                playersList.Add(new Player(txtName3.Text,(bool)p2.IsChecked));
            }
            else if (numOfPlayers == 4)
            {
                playersList.Add(new Player(txtName1.Text));
                playersList.Add(new Player(txtName2.Text, (bool)p2.IsChecked));
                playersList.Add(new Player(txtName3.Text, (bool)p2.IsChecked));
                playersList.Add(new Player(txtName4.Text, (bool)p2.IsChecked));
            }



            // Creating a new play window, the game will carry on there
            playGoFishWindow = new PlayGoFishWindow(playersList, this);
            Hide(); // Hiding the main menu while the play window is up
            playGoFishWindow.Show();
        }

        private void btnOpenSave_Click(object sender, RoutedEventArgs e)
        {
            // Creating \Pentegames directory so there is no error
            System.IO.Directory.CreateDirectory(@"\CardGameGallery\Go Fish");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\CardGameGallery\Go Fish";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Go Fish Saves|*.GoFish";
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Get the file path chosen by the user
                    stream = new FileStream(@$"{openFileDialog.FileName}", FileMode.Open, FileAccess.Read);
                    // Deserializing the game save file to a GameSave object
                    save = (GoFishSaveGame)formatter.Deserialize(stream);
                    // Opening the play window with the GameSave object
                    playGoFishWindow = new PlayGoFishWindow(save, openFileDialog.FileName, this);
                    // Hiding main menu withle play window is up
                    Hide();
                    // Displaying the play window
                    playGoFishWindow.Show();
                    // CLosing the stream to free up resources
                    stream.Close();
                }
                catch
                {
                    // If an error occurs while opening save file
                    MessageBox.Show("Something went wrong, try again.");
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("In here! B");
            // Displaying what ever window called this window after this window is closed
            whatWindowCalledThisWindow.Show();
        }
        private void Window_Closed(object sender, EventArgs e)
        {

        }

    }
}
