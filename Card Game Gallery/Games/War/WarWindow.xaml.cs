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

namespace Card_Game_Gallery.Games.War
{
    /// <summary>
    /// Interaction logic for WarWindow.xaml
    /// </summary>
    public partial class WarWindow : Window
    {

        private const int PLAYERS = 2;

        private FileStream stream;  // Used for deserilization
        private IFormatter formatter; // Used for deserilization

        private Window calledFrom;

        public WarWindow(Window calledFrom)
        {
            InitializeComponent();
            this.calledFrom = calledFrom;
            formatter = new BinaryFormatter();
        }

        private void p1_Checked(object sender, RoutedEventArgs e)
        {
            // Required to compile
        }

        private void p2_Checked(object sender, RoutedEventArgs e)
        {
            // Required to compile
        }

        private void btnOpenSave_Click(object sender, RoutedEventArgs e)
        {
            // Creating \Pentegames directory so there is no error
            System.IO.Directory.CreateDirectory(@"\CardGameGallery\War");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\CardGameGallery\War";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "War Saves|*.War";
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Get the file path chosen by the user
                    stream = new FileStream(@$"{openFileDialog.FileName}", FileMode.Open, FileAccess.Read);
                    // Deserializing the game save file to a GameSave object
                    WarSaveGame save = (WarSaveGame)formatter.Deserialize(stream);
                    // Opening the play window with the GameSave object
                    PlayWarWindow playWar = new PlayWarWindow(save, openFileDialog.FileName, this);
                    // Hiding main menu withle play window is up
                    Hide();
                    // Displaying the play window
                    playWar.Show();
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

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Player[] players = new Player[PLAYERS];
            players[0] = new Player(txtName1.Text, false, new List<Card>(), 0);
            players[1] = new Player(txtName2.Text, (bool)p2.IsChecked, new List<Card>(), 0);
            WarSaveGame newWar = new WarSaveGame(players);
            PlayWarWindow playWar = new PlayWarWindow(newWar, this);
            Hide();
            playWar.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Displaying what ever window called this window after this window is closed
            calledFrom.Show();
        }
        private void Window_Closed(object sender, EventArgs e)
        {

        }

    }
}
