﻿using Card_Game_Gallery.Models;
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
    /// Interaction logic for PlayWarWindow.xaml
    /// </summary>
    public partial class PlayWarWindow : Window
    {
        private Window calledFrom;

        private WarSaveGame war;

        private WarLogic logic = new WarLogic();

        private string savePath;

        public PlayWarWindow(WarSaveGame newWar, WarWindow calledFrom)
        {
            InitializeComponent();
            this.calledFrom = calledFrom;
            war = newWar;
        }

        public PlayWarWindow(WarSaveGame save, string savePath, WarWindow calledFrom)
        {
            this.war = save;
            this.savePath = savePath;
            this.calledFrom = calledFrom;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Displaying what ever window called this window after this window is closed
            calledFrom.Show();
        }

        private void Draw_Button_Click(object sender, RoutedEventArgs e)
        {
            List<List<Card>> cards = logic.GetPlayersCardsForNormalPlay(war);
            Button button = new Button();
            button.Height = 190;
            button.Width = 130;
            button.Content = cards[0][0].Face;
            button.FontSize = 40;
            p1DrawnCards.Children.Add(button);
            p2DrawnCards.Children.Add(button);
        }

        public void PlayGame()
        {
            //do turns while there is still cards remaining for both players
            while(logic.isPlayerCardsEmpty(war) != true)
            {

            }
            //start war if the cards are the same
            
        }
    }
}
