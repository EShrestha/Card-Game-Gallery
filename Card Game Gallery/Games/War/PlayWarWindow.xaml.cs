using Card_Game_Gallery.Converters;
using Card_Game_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Card_Game_Gallery.Games.War
{
    /// <summary>
    /// Interaction logic for PlayWarWindow.xaml
    /// </summary>
    public partial class PlayWarWindow : Window
    {
        private Window calledFrom;

        private WarSaveGame war;

        private WarLogic logic;

        private string savePath;

        private DispatcherTimer _timer;

        private bool gameFinished;

        public PlayWarWindow(WarSaveGame newWar, WarWindow calledFrom)
        {
            InitializeComponent();
            this.calledFrom = calledFrom;
            war = newWar;
            HandleLogicSetup();
            BindNames();
        }


        public PlayWarWindow(WarSaveGame save, string savePath, WarWindow calledFrom)
        {
            InitializeComponent();
            this.war = save;
            this.savePath = savePath;
            this.calledFrom = calledFrom;
            BindNames();
        }

        private void HandleLogicSetup()
        {
            logic = new WarLogic();
            logic.DealCards(war);
            UpdateCardCounts();
        }

        private void BindNames()
        {
            Binding p1NameBind = new Binding();
            p1NameBind.Source = war.Players[0];
            p1NameBind.Path = new PropertyPath("name");
            p1NameBind.Mode = BindingMode.OneWay;
            p1Name.SetBinding(TextBlock.TextProperty, p1NameBind);
            Binding p2NameBind = new Binding();
            p2NameBind.Source = war.Players[1];
            p2NameBind.Path = new PropertyPath("name");
            p2NameBind.Mode = BindingMode.OneWay;
            p2Name.SetBinding(TextBlock.TextProperty, p2NameBind);
            Binding p1DrawnNameBind = new Binding();
            p1DrawnNameBind.Source = war.Players[0];
            p1DrawnNameBind.Path = new PropertyPath("name");
            p1DrawnNameBind.Mode = BindingMode.OneWay;
            p1NameForDrawn.SetBinding(TextBlock.TextProperty, p1DrawnNameBind);
            Binding p2DrawnNameBind = new Binding();
            p2DrawnNameBind.Source = war.Players[1];
            p2DrawnNameBind.Path = new PropertyPath("name");
            p2DrawnNameBind.Mode = BindingMode.OneWay;
            p2NameForDrawn.SetBinding(TextBlock.TextProperty, p2DrawnNameBind);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Only ask to save if the game is not yet finished
            if (!gameFinished)
            {
                if (!askToClose()) { e.Cancel = true; }
                else { calledFrom.Show(); } // Displaying the window that called this window
            }
            else { calledFrom.Show(); } // Displaying the window that called this window
        }

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

        private bool SaveGame()
        {
            // Creating a new save game object and sending it to logic class to be searlized
            if (!logic.SaveGame(war, savePath))
            {
                MessageBox.Show("Error", "Failed to save", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Draw_Button_Click(object sender, RoutedEventArgs e)
        {
            Draw_Button.IsEnabled = false;
            List<List<Card>> cards = logic.GetPlayersCardsForNormalPlay(war);
            UpdateCardCounts();
            HandleCardDraw(cards);
            foreach (List<Card> playerCards in cards)
            {
                playerCards.Where(c => !c.Revealed).FirstOrDefault().Flip();
            }
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 3), DispatcherPriority.Normal, delegate
            {
                _timer.Stop();
                int comp = cards[0].Where(c => c.Revealed).LastOrDefault().CompareCardFaceValues(cards[1].Where(c => c.Revealed).LastOrDefault());
                HandleComparisonValue(cards, comp);
            }, Application.Current.Dispatcher);
        }

        private void HandleComparisonValue(List<List<Card>> cards, int comp)
        {
            if (comp == 0)
            {
                HandleWar(cards);
            }
            else if (comp > 0)
            {
                GiveCards(0, cards);
            }
            else
            {
                GiveCards(1, cards);
            }
        }

        private void HandleWar(List<List<Card>> cards)
        {
            List<List<Card>> warCards = logic.GetPlayersCardsForWar(war);
            UpdateCardCounts();
            HandleCardDraw(warCards);
            for (int i = 0; i < warCards.Count; i++)
            {
                warCards[i].Where(c => !c.Revealed).LastOrDefault().Flip();
                for (int j = 0; j < warCards[i].Count; j++)
                {
                    cards[i].Add(warCards[i][j]);
                }
            }
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 3), DispatcherPriority.Normal, delegate
            {
                int comp = cards[0].Where(c => c.Revealed).LastOrDefault().CompareCardFaceValues(cards[1].Where(c => c.Revealed).LastOrDefault());
                if (comp == 0)
                {
                    if (warCards[0].Count < WarLogic.CARDS_FOR_WAR)
                    {
                        GiveCards(0, cards);
                    }
                    else if (warCards[1].Count < WarLogic.CARDS_FOR_WAR)
                    {
                        GiveCards(1, cards);
                    }
                }
                HandleComparisonValue(cards, comp);
            }, Application.Current.Dispatcher);
        }

        private void GiveCards(int playerIndex, List<List<Card>> cards)
        {
            foreach (List<Card> playerCards in cards)
            {
                foreach (Card card in playerCards)
                {
                    if (card.Revealed) card.Flip();
                    war.Players[playerIndex].cards.Add(card);
                }
            }
            ClearDrawn();
            UpdateCardCounts();
            if (logic.isPlayerCardsEmpty(war))
            {
                HandleWin();
            }
            else
            {
                Draw_Button.IsEnabled = true;
            }
        }

        private void HandleWin()
        {
            grdMiddle.Children.Clear();
            MessageBox.Show($"{war.Players.Where(p => p.cards.Count > 0).FirstOrDefault()} has won!");
            gameFinished = true;
        }

        private void ClearDrawn()
        {
            p1DrawnCards.Children.Clear();
            p2DrawnCards.Children.Clear();
        }

        private void UpdateCardCounts()
        {
            p1Cards.Text = war.Players[0].cards.Count.ToString();
            p2Cards.Text = war.Players[1].cards.Count.ToString();
        }

        private void HandleCardDraw(List<List<Card>> cards)
        {
            StringToImageSourceConverter converter = new StringToImageSourceConverter();
            for (int i = 0; i < cards.Count; i++)
            {
                foreach (Card c in cards[i])
                {
                    Button button = new Button();
                    button.Height = 190;
                    button.Width = 130;
                    Image img = new Image()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Stretch = Stretch.Fill
                    };
                    Binding b = new Binding() { Source = c, Path = new PropertyPath("ImageName"), Converter = converter, Mode = BindingMode.OneWay };
                    BindingExpressionBase be = img.SetBinding(Image.SourceProperty, b);
                    button.Content = img;
                    if (i == 0)
                    {
                        p1DrawnCards.Children.Add(button);
                    }
                    else
                    {
                        p2DrawnCards.Children.Add(button);
                    }
                }
            }
        }

    }
}
