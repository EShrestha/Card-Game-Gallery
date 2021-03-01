using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Card_Game_Gallery.Games.Go_Fish;
using Card_Game_Gallery.Games.War;

namespace Card_Game_Gallery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGoFish_Click(object sender, RoutedEventArgs e)
        {
            GoFishWindow gfw = new GoFishWindow(this);
            gfw.Show();
            Hide();
        }

        private void btnWar_Click(object sender, RoutedEventArgs e)
        {
            WarWindow ww = new WarWindow();
            ww.Show();
            Hide();
        }

        private void btnBlackjack_Click(object sender, RoutedEventArgs e)
        {
   
        }

        private void btnPoker_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
