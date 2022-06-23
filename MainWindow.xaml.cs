using System;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }


        #endregion
        #region Private Members

        private MarkType[] nResults;
        private bool mPlayer1Turn;
        private bool mGameEnded;

        #endregion

        private void NewGame()
        {
            nResults = new MarkType[9];

            for (var i = 0; i < nResults.Length; i++)
            {
                nResults[i] = MarkType.Free;
            }

            mPlayer1Turn = true;
            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game has not finished 
            mGameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            //Cast a sender to a button
            var button = (Button)sender;

            //Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Don't do anything if rhe cell already has a value in it
            if(nResults[index] != MarkType.Free)
                return;
            // Set the cell value based on which players turn it on
            nResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;
            // Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // Change noughts to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;
            // Toggle the player turns
            mPlayer1Turn ^= true;

            // Check for a winner
            CheckForWinner();
        }

        //Check if there is a winner of a 3 line straight 
        private void CheckForWinner()
        {
            #region Horizontal Wins
            //Check for horizontal wins
            // Row 0
            if (nResults[0] != MarkType.Free && (nResults[0] & nResults[1] & nResults[2]) == nResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            // Row 1
            if (nResults[3] != MarkType.Free && (nResults[3] & nResults[4] & nResults[5]) == nResults[3])
            {
                mGameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            // Row 2
            if (nResults[6] != MarkType.Free && (nResults[6] & nResults[7] & nResults[8]) == nResults[6])
            {
                mGameEnded = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical Wins
            //Check for vertical wins
            //Column 0
            if (nResults[0] != MarkType.Free && (nResults[0] & nResults[3] & nResults[6]) == nResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //Column 1
            if (nResults[1] != MarkType.Free && (nResults[1] & nResults[4] & nResults[7]) == nResults[1])
            {
                mGameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //Column 2
            if (nResults[2] != MarkType.Free && (nResults[2] & nResults[5] & nResults[8]) == nResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region No Winners
            // Check for no winner and full board
            if (!nResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion

            #region Diagonal Wins
            //Check for diagonal wins
            //Top Left Botton Right
            if (nResults[0] != MarkType.Free && (nResults[0] & nResults[4] & nResults[8]) == nResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            //Top Right Botton Left
            if (nResults[2] != MarkType.Free && (nResults[2] & nResults[4] & nResults[6]) == nResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion
        }
    }
}
