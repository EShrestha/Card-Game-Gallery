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

namespace Card_Game_Gallery.Games.Blackjack
{
    /// <summary>
    /// Interaction logic for BlackjackWindow.xaml
    /// </summary>
    public partial class BlackjackWindow : Window
    {
        private Window calledFrom;

        public BlackjackWindow(Window calledFrom)
        {
            InitializeComponent();
            this.calledFrom = calledFrom;
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
