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
    /// Responsible for asking user information about the game to set it up, such as pvp or pvc, number of players...
    /// </summary>
    public partial class GoFishWindow : Window
    {
        Window whatWindowCalledThisWindow;

        public GoFishWindow(Window whatWindowCalledThisWindow)
        {
            this.whatWindowCalledThisWindow = whatWindowCalledThisWindow;
            InitializeComponent();
        }




        // Is called as the window is closing
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Displaying what ever window called this window after this window is closed
            whatWindowCalledThisWindow.Show();
        }
    }
}
