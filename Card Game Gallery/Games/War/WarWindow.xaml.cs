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

namespace Card_Game_Gallery.Games.War
{
    /// <summary>
    /// Interaction logic for WarWindow.xaml
    /// </summary>
    public partial class WarWindow : Window
    {

        private const int PLAYERS = 2;

        private Window calledFrom;

        public WarWindow(Window calledFrom)
        {
            InitializeComponent();
            this.calledFrom = calledFrom;
        }

        private void p1_Checked(object sender, RoutedEventArgs e)
        {
            // Required to compile
        }

        private void p2_Checked(object sender, RoutedEventArgs e)
        {
            // Required to compile
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Player[] players = new Player[PLAYERS];
            players[0] = new Player(txtName1.Text, false, new List<Card>(), 0);
            players[1] = new Player(txtName2.Text, (bool)p2.IsChecked, new List<Card>(), 0);
            WarSaveGame newWar = new WarSaveGame((bool)p2.IsChecked, players);
            PlayWarWindow playWar = new PlayWarWindow(newWar, this);
            Hide();
            playWar.Show();
        }

    }
}
