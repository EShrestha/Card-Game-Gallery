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
    }
}
