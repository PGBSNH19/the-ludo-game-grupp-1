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
using EngineClasses;

namespace LudoGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameEngine gameEngine;
        public Grid gameBoardGrid;
        public Button button;
        public TextBox enterNameBox;
        public Label diceResult;
        public Label playerColor;
        public Button startGame;
        public Button rollDice;
        public StackPanel navStackPanel;


        public MainWindow()
        {
            InitializeComponent();
            Start();
        }
        private void Start()
        {
            gameEngine = new GameEngine(new Session(), new GameBoard(), new GameLog());
            CreateGameBoardGUI(gameEngine.GameBoard);
            CreateStartGameGUI();
            UpdateGameBoard(gameEngine.GameBoard);

            gameEngine.CreatePlayer("Mirko", "Red");
            gameEngine.CreatePlayer("Anas", "Blue");
            
        }

        private void UpdateGameBoard(GameBoard gameBoard)
        {
            Dictionary<string, int> gameBoard2D = GetGameBoard2DRepresentation(gameBoard);
            foreach (KeyValuePair<string, int> square in gameBoard2D)
            {
                gameBoardGrid.Children.OfType<Button>().Where(b => b.Tag.ToString() == square.Key).First().Content = gameBoard.BoardRoute[square.Value].GameSquareNumber;
            }
        }

        public void CreateStartGameGUI()
        {
            navStackPanel = new StackPanel
            {
                Background = Brushes.AliceBlue
            };
            gameBoardGrid.Children.Add(navStackPanel);
            Grid.SetColumn(navStackPanel, 11);

            Label enterNameInstruction = new Label
            {
                Content = "Skriv in ditt användarnamn!",
                FontSize = 30,
                Height = 1000,
                Width = 500
            };
            navStackPanel.Children.Add(enterNameInstruction);

            TextBox temp;
            for (int i = 0; i < 4; i++)
            {
                enterNameBox = new TextBox();
                enterNameBox.Tag = i;
                navStackPanel.Children.Add(enterNameBox);
            }

            startGame = new Button
            {
                Content = "Start Game!"
            };
            navStackPanel.Children.Add(startGame);
            startGame.Click += StartGame_Click;
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            navStackPanel.Children.Clear();
            CreateGameNavGUI();
        }

        public TextBox CreateUserNameTextBox(int playerNumber)
        {
            enterNameBox = new TextBox();
            enterNameBox.Tag = playerNumber;
            return enterNameBox;
        }

        public void CreateGameNavGUI()
        {
            MessageBox.Show("Hej Mirko!");
        }

        public void CreateGameBoardGUI(GameBoard gameBoard)
        {            
            gameBoardGrid = (Grid)Content;

            for (int i = 0; i < 11; i++)
            {
                gameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
                gameBoardGrid.RowDefinitions.Add(new RowDefinition());
            }
            gameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            for (int row = 0; row < 11; row++)
            {
                for (int column = 0; column < 11; column++)
                {                    
                    button = new Button();
                    button.Tag = $"{column}.{row}";
                    button.Click += Button_Click;

                    if (row == 1 && column == 1 || (row == 2 && column == 2) || (row == 1 && column == 2) || (row == 2 && column == 1) || (row == 4 && column == 0) || (row == 5 && column >= 0 && column < 5))
                    {
                        button.Background = Brushes.Blue;
                    }
                    else if (row == 1 && column == 8 || (row == 1 && column == 9) || (row == 2 && column == 8) || (row == 2 && column == 9) || (row == 0 && column == 6) || (row < 5 && column == 5))
                    {
                        button.Background = Brushes.Red;
                    }
                    else if (row == 8 && column == 1 || (row == 8 && column == 2) || (row == 9 && column == 1) || (row == 9 && column == 2) || (row == 10 && column == 4) || (row < 11 && row > 5 && column == 5))
                    {
                        button.Background = Brushes.Green;
                    }
                    else if (row == 8 && column == 8 || (row == 8 && column == 9) || (row == 9 && column == 8) || (row == 9 && column == 9) || (row == 6 && column == 10) || (row == 5 && column > 5))
                    {
                        button.Background = Brushes.Yellow;
                    }
                    else if (row == 5 && column == 5)
                    {
                        button.Background = Brushes.Gold;
                        button.Content = "GOAL";
                    }
                    else if (row == 4 && column > 0 || (column < 5 && column > 5) || (row == 6 && column >= 0) || (column < 5 && column > 5) || (column == 4 && row >= 0 && row <= 10) || (column == 6 && row >= 0 && row <= 10))
                    {
                        button.Background = Brushes.White;
                    }
                    else
                    {
                        button.Background = Brushes.Black;
                    }
                    gameBoardGrid.Children.Add(button);
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((sender as Button).Tag.ToString());
        }

        public Dictionary<string, int> GetGameBoard2DRepresentation(GameBoard gameBoard)
        {
            Dictionary<string, int> GameBoard2DTranslation = new Dictionary<string, int>();
            //Red squares
            GameBoard2DTranslation.Add("6.0", gameBoard.BoardRoute[0].GameSquareNumber);
            GameBoard2DTranslation.Add("5.1", gameBoard.BoardRoute[1].GameSquareNumber);
            GameBoard2DTranslation.Add("5.2", gameBoard.BoardRoute[2].GameSquareNumber);
            GameBoard2DTranslation.Add("5.3", gameBoard.BoardRoute[3].GameSquareNumber);
            GameBoard2DTranslation.Add("5.4", gameBoard.BoardRoute[4].GameSquareNumber);
            //White section NE
            GameBoard2DTranslation.Add("6.1", gameBoard.BoardRoute[6].GameSquareNumber);
            GameBoard2DTranslation.Add("6.2", gameBoard.BoardRoute[7].GameSquareNumber);
            GameBoard2DTranslation.Add("6.3", gameBoard.BoardRoute[8].GameSquareNumber);
            GameBoard2DTranslation.Add("6.4", gameBoard.BoardRoute[9].GameSquareNumber);
            GameBoard2DTranslation.Add("7.4", gameBoard.BoardRoute[10].GameSquareNumber);
            GameBoard2DTranslation.Add("8.4", gameBoard.BoardRoute[11].GameSquareNumber);
            GameBoard2DTranslation.Add("9.4", gameBoard.BoardRoute[12].GameSquareNumber);
            GameBoard2DTranslation.Add("10.4", gameBoard.BoardRoute[13].GameSquareNumber);
            GameBoard2DTranslation.Add("10.5", gameBoard.BoardRoute[14].GameSquareNumber);
            //Yellow squares
            GameBoard2DTranslation.Add("10.6", gameBoard.BoardRoute[15].GameSquareNumber);
            GameBoard2DTranslation.Add("9.5", gameBoard.BoardRoute[16].GameSquareNumber);
            GameBoard2DTranslation.Add("8.5", gameBoard.BoardRoute[17].GameSquareNumber);
            GameBoard2DTranslation.Add("7.5", gameBoard.BoardRoute[18].GameSquareNumber);
            GameBoard2DTranslation.Add("6.5", gameBoard.BoardRoute[19].GameSquareNumber);
            //White section SE
            GameBoard2DTranslation.Add("9.6", gameBoard.BoardRoute[21].GameSquareNumber);
            GameBoard2DTranslation.Add("8.6", gameBoard.BoardRoute[22].GameSquareNumber);
            GameBoard2DTranslation.Add("7.6", gameBoard.BoardRoute[23].GameSquareNumber);
            GameBoard2DTranslation.Add("6.6", gameBoard.BoardRoute[24].GameSquareNumber);
            GameBoard2DTranslation.Add("6.7", gameBoard.BoardRoute[25].GameSquareNumber);
            GameBoard2DTranslation.Add("6.8", gameBoard.BoardRoute[26].GameSquareNumber);
            GameBoard2DTranslation.Add("6.9", gameBoard.BoardRoute[27].GameSquareNumber);
            GameBoard2DTranslation.Add("6.10", gameBoard.BoardRoute[28].GameSquareNumber);
            GameBoard2DTranslation.Add("5.10", gameBoard.BoardRoute[29].GameSquareNumber);
            //Green section
            GameBoard2DTranslation.Add("4.10", gameBoard.BoardRoute[30].GameSquareNumber);
            GameBoard2DTranslation.Add("5.9", gameBoard.BoardRoute[31].GameSquareNumber);
            GameBoard2DTranslation.Add("5.8", gameBoard.BoardRoute[32].GameSquareNumber);
            GameBoard2DTranslation.Add("5.7", gameBoard.BoardRoute[33].GameSquareNumber);
            GameBoard2DTranslation.Add("5.6", gameBoard.BoardRoute[34].GameSquareNumber);
            //White section SW
            GameBoard2DTranslation.Add("4.9", gameBoard.BoardRoute[36].GameSquareNumber);
            GameBoard2DTranslation.Add("4.8", gameBoard.BoardRoute[37].GameSquareNumber);
            GameBoard2DTranslation.Add("4.7", gameBoard.BoardRoute[38].GameSquareNumber);
            GameBoard2DTranslation.Add("4.6", gameBoard.BoardRoute[39].GameSquareNumber);
            GameBoard2DTranslation.Add("3.6", gameBoard.BoardRoute[40].GameSquareNumber);
            GameBoard2DTranslation.Add("2.6", gameBoard.BoardRoute[41].GameSquareNumber);
            GameBoard2DTranslation.Add("1.6", gameBoard.BoardRoute[42].GameSquareNumber);
            GameBoard2DTranslation.Add("0.6", gameBoard.BoardRoute[43].GameSquareNumber);
            GameBoard2DTranslation.Add("0.5", gameBoard.BoardRoute[44].GameSquareNumber);
            //Blue section
            GameBoard2DTranslation.Add("0.4", gameBoard.BoardRoute[45].GameSquareNumber);
            GameBoard2DTranslation.Add("1.5", gameBoard.BoardRoute[46].GameSquareNumber);
            GameBoard2DTranslation.Add("2.5", gameBoard.BoardRoute[47].GameSquareNumber);
            GameBoard2DTranslation.Add("3.5", gameBoard.BoardRoute[48].GameSquareNumber);
            GameBoard2DTranslation.Add("4.5", gameBoard.BoardRoute[49].GameSquareNumber);
            //White section NW
            GameBoard2DTranslation.Add("1.4", gameBoard.BoardRoute[51].GameSquareNumber);
            GameBoard2DTranslation.Add("2.4", gameBoard.BoardRoute[52].GameSquareNumber);
            GameBoard2DTranslation.Add("3.4", gameBoard.BoardRoute[53].GameSquareNumber);
            GameBoard2DTranslation.Add("4.4", gameBoard.BoardRoute[54].GameSquareNumber);
            GameBoard2DTranslation.Add("4.3", gameBoard.BoardRoute[55].GameSquareNumber);
            GameBoard2DTranslation.Add("4.2", gameBoard.BoardRoute[56].GameSquareNumber);
            GameBoard2DTranslation.Add("4.1", gameBoard.BoardRoute[57].GameSquareNumber);
            GameBoard2DTranslation.Add("4.0", gameBoard.BoardRoute[58].GameSquareNumber);
            GameBoard2DTranslation.Add("5.0", gameBoard.BoardRoute[59].GameSquareNumber);
            //Goal
            GameBoard2DTranslation.Add("5.5", gameBoard.BoardRoute[50].GameSquareNumber);

            return GameBoard2DTranslation;
        }
    }
}
