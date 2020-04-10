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
        public Grid mainGrid;
        public Grid gameBoardGrid;
        public StackPanel menuGamePanel;
        public Button gameSquareButton;
        public TextBox enterNameBox;
        public Label diceResult;
        public Label playerColor;
        public Button startGame;
        public Button rollDice;
        Dictionary<string, int> gameBoard2D;

        public MainWindow()
        {
            InitializeComponent();
            Start();
        }
        private void Start()
        {
            //Set window Height and Width
            Height = 800;
            Width = 1200;

            //Set up new Game Engine
            gameEngine = new GameEngine(new Session(), new GameBoard(), new GameLog(), new LudoContext());

            //Set up game board 2D translation
            gameBoard2D = GetGameBoard2DRepresentation(gameEngine.GameBoard);

            //Create main window grid
            mainGrid = (Grid)Content;
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            //Build game board visual and start menu
            CreateGameBoardGUI(gameEngine.GameBoard);
            CreateStartGameGUI();

            //Add Game Board visual
            mainGrid.Children.Add(gameBoardGrid);
            Grid.SetColumn(gameBoardGrid, 0);

            //Add Start Game menu            
            mainGrid.Children.Add(menuGamePanel);
            Grid.SetColumn(menuGamePanel, 1);            

        }

        

        public void CreateStartGameGUI()
        {
            menuGamePanel = new StackPanel
            {
                Background = Brushes.AliceBlue
            };

            Label enterNameInstruction = new Label
            {
                Content = "Skriv in ditt användarnamn!",
                FontSize = 30,
            };
            menuGamePanel.Children.Add(enterNameInstruction);

            for (int i = 0; i < 4; i++)
            {
                enterNameBox = new TextBox();
                enterNameBox.Tag = i;
                menuGamePanel.Children.Add(enterNameBox);
            }

            startGame = new Button
            {
                Content = "Start Game!"
            };
            menuGamePanel.Children.Add(startGame);
            startGame.Click += StartGame_Click;

        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            int boxCount = menuGamePanel.Children.OfType<TextBox>().Where(b => b.Text != "").Count();
            string[] colors = { "Red", "Yellow", "Green", "Blue" };

            for (int i = 0; i < boxCount; i++)
            {
                gameEngine.CreatePlayer(menuGamePanel.Children.OfType<TextBox>().Where(b => b.Tag.ToString() == i.ToString()).First().Text, colors[i]);
            }

            menuGamePanel.Children.Clear();
            CreateGameNavGUI(boxCount);

            UpdateGameBoard();
        }

        public void CreateGameNavGUI(int players)
        {
            Dictionary<string, Brush> brushes = new Dictionary<string, Brush>();
            brushes.Add("Red", Brushes.Red);
            brushes.Add("Yellow", Brushes.Yellow);
            brushes.Add("Green", Brushes.Green);
            brushes.Add("Blue", Brushes.Blue);

            for (int i = 0; i < players; i++)
            {
                menuGamePanel.Children.Add
                (
                    new Label
                    {
                        Content = gameEngine.PlayerSelect(i).UserName,
                        Background = brushes[gameEngine.PlayerSelect(i).Color],
                        FontSize = 30,
                    }
                );
            }
            
            rollDice = new Button
            {
                Content = "Roll Dice!"
            };
            menuGamePanel.Children.Add(rollDice);
            rollDice.Click += RollDice_Click;

            diceResult = new Label
            {
                Content = "Dice Result",
                FontSize = 30,
            };
            menuGamePanel.Children.Add(diceResult);

        }

        private void RollDice_Click(object sender, RoutedEventArgs e)
        {
            int roll = gameEngine.RollDice();            
            diceResult.Content = roll.ToString();
            rollDice.IsEnabled = false;
        }

        public void CreateGameBoardGUI(GameBoard gameBoard)
        {
            gameBoardGrid = new Grid();

            for (int i = 0; i < 11; i++)
            {
                gameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                gameBoardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            for (int row = 0; row < 11; row++)
            {
                for (int column = 0; column < 11; column++)
                {
                    gameSquareButton = new Button
                    {
                        Height = 50,
                        Width = 50,
                        Margin = new Thickness(0),
                    };
                    gameSquareButton.Tag = $"{column}.{row}";
                    gameSquareButton.Click += gameSquareButton_Click;

                    if (row == 1 && column == 1 || (row == 2 && column == 2) || (row == 1 && column == 2) || (row == 2 && column == 1) || (row == 4 && column == 0) || (row == 5 && column >= 0 && column < 5))
                    {
                        gameSquareButton.Background = Brushes.Blue;
                    }
                    else if (row == 1 && column == 8 || (row == 1 && column == 9) || (row == 2 && column == 8) || (row == 2 && column == 9) || (row == 0 && column == 6) || (row < 5 && column == 5))
                    {
                        gameSquareButton.Background = Brushes.Red;
                    }
                    else if (row == 8 && column == 1 || (row == 8 && column == 2) || (row == 9 && column == 1) || (row == 9 && column == 2) || (row == 10 && column == 4) || (row < 11 && row > 5 && column == 5))
                    {
                        gameSquareButton.Background = Brushes.Green;
                    }
                    else if (row == 8 && column == 8 || (row == 8 && column == 9) || (row == 9 && column == 8) || (row == 9 && column == 9) || (row == 6 && column == 10) || (row == 5 && column > 5))
                    {
                        gameSquareButton.Background = Brushes.Yellow;
                    }
                    else if (row == 5 && column == 5)
                    {
                        gameSquareButton.Background = Brushes.Gold;
                        gameSquareButton.Content = "GOAL";
                    }
                    else if (row == 4 && column > 0 || (column < 5 && column > 5) || (row == 6 && column >= 0) || (column < 5 && column > 5) || (column == 4 && row >= 0 && row <= 10) || (column == 6 && row >= 0 && row <= 10))
                    {
                        gameSquareButton.Background = Brushes.White;
                    }
                    else
                    {
                        gameSquareButton.Background = Brushes.Black;
                    }
                    gameBoardGrid.Children.Add(gameSquareButton);
                    Grid.SetRow(gameSquareButton, row);
                    Grid.SetColumn(gameSquareButton, column);
                }
            }
        }

        private void gameSquareButton_Click(object sender, RoutedEventArgs e)
        {
            //Check if buttons tag is translateable according to gameBoard2D
            if (gameBoard2D.ContainsKey((sender as Button).Tag.ToString()))
            {
                int squareTranslation = gameBoard2D[(sender as Button).Tag.ToString()];

                //Get list of all pieces occupying square
                List<GamePiece> piecesOnSquare =
                gameEngine
                .CurrentPlayer()
                .GamePiece
                .Where(gp => gp.BoardSquareNumber != null && gp.BoardSquareNumber.Value == squareTranslation)
                .ToList();

                //Move first piece in list if there are any on square
                if (piecesOnSquare.Count > 0)
                {
                    gameEngine.MoveGamePiece(piecesOnSquare[0], int.Parse(diceResult.Content.ToString()));

                    //Enable rolling dice
                    rollDice.IsEnabled = true;

                    //Set to next turn
                    gameEngine.Session.Turns++;

                    UpdateGameBoard();
                }
            }
        }

        public Dictionary<string, int> GetGameBoard2DRepresentation(GameBoard gameBoard)
        {
            Dictionary<string, int> GameBoard2DTranslation = new Dictionary<string, int>();
            //Red squares
            GameBoard2DTranslation.Add("5.1", gameBoard.Board[0].BoardSquareNumber);
            GameBoard2DTranslation.Add("5.2", gameBoard.Board[1].BoardSquareNumber);
            GameBoard2DTranslation.Add("5.3", gameBoard.Board[2].BoardSquareNumber);
            GameBoard2DTranslation.Add("5.4", gameBoard.Board[3].BoardSquareNumber);
            GameBoard2DTranslation.Add("6.0", gameBoard.Board[4].BoardSquareNumber);
            //White section NE
            GameBoard2DTranslation.Add("6.1", gameBoard.Board[5].BoardSquareNumber);
            GameBoard2DTranslation.Add("6.2", gameBoard.Board[6].BoardSquareNumber);
            GameBoard2DTranslation.Add("6.3", gameBoard.Board[7].BoardSquareNumber);
            GameBoard2DTranslation.Add("6.4", gameBoard.Board[8].BoardSquareNumber);
            GameBoard2DTranslation.Add("7.4", gameBoard.Board[9].BoardSquareNumber);
            GameBoard2DTranslation.Add("8.4", gameBoard.Board[10].BoardSquareNumber);
            GameBoard2DTranslation.Add("9.4", gameBoard.Board[11].BoardSquareNumber);
            GameBoard2DTranslation.Add("10.4", gameBoard.Board[12].BoardSquareNumber);
            GameBoard2DTranslation.Add("10.5", gameBoard.Board[13].BoardSquareNumber);
            //Yellow squares
            GameBoard2DTranslation.Add("9.5", gameBoard.Board[14].BoardSquareNumber);
            GameBoard2DTranslation.Add("8.5", gameBoard.Board[15].BoardSquareNumber);
            GameBoard2DTranslation.Add("7.5", gameBoard.Board[16].BoardSquareNumber);
            GameBoard2DTranslation.Add("6.5", gameBoard.Board[17].BoardSquareNumber);
            GameBoard2DTranslation.Add("10.6", gameBoard.Board[18].BoardSquareNumber);
            //White section SE
            GameBoard2DTranslation.Add("9.6", gameBoard.Board[19].BoardSquareNumber);
            GameBoard2DTranslation.Add("8.6", gameBoard.Board[20].BoardSquareNumber);
            GameBoard2DTranslation.Add("7.6", gameBoard.Board[21].BoardSquareNumber);
            GameBoard2DTranslation.Add("6.6", gameBoard.Board[22].BoardSquareNumber);
            GameBoard2DTranslation.Add("6.7", gameBoard.Board[23].BoardSquareNumber);
            GameBoard2DTranslation.Add("6.8", gameBoard.Board[24].BoardSquareNumber);
            GameBoard2DTranslation.Add("6.9", gameBoard.Board[25].BoardSquareNumber);
            GameBoard2DTranslation.Add("6.10", gameBoard.Board[26].BoardSquareNumber);
            GameBoard2DTranslation.Add("5.10", gameBoard.Board[27].BoardSquareNumber);
            //Green section
            GameBoard2DTranslation.Add("5.9", gameBoard.Board[28].BoardSquareNumber);
            GameBoard2DTranslation.Add("5.8", gameBoard.Board[29].BoardSquareNumber);
            GameBoard2DTranslation.Add("5.7", gameBoard.Board[30].BoardSquareNumber);
            GameBoard2DTranslation.Add("5.6", gameBoard.Board[31].BoardSquareNumber);
            GameBoard2DTranslation.Add("4.10", gameBoard.Board[32].BoardSquareNumber);
            //White section SW
            GameBoard2DTranslation.Add("4.9", gameBoard.Board[33].BoardSquareNumber);
            GameBoard2DTranslation.Add("4.8", gameBoard.Board[34].BoardSquareNumber);
            GameBoard2DTranslation.Add("4.7", gameBoard.Board[35].BoardSquareNumber);
            GameBoard2DTranslation.Add("4.6", gameBoard.Board[36].BoardSquareNumber);
            GameBoard2DTranslation.Add("3.6", gameBoard.Board[37].BoardSquareNumber);
            GameBoard2DTranslation.Add("2.6", gameBoard.Board[38].BoardSquareNumber);
            GameBoard2DTranslation.Add("1.6", gameBoard.Board[39].BoardSquareNumber);
            GameBoard2DTranslation.Add("0.6", gameBoard.Board[40].BoardSquareNumber);
            GameBoard2DTranslation.Add("0.5", gameBoard.Board[41].BoardSquareNumber);
            //Blue section
            GameBoard2DTranslation.Add("1.5", gameBoard.Board[42].BoardSquareNumber);
            GameBoard2DTranslation.Add("2.5", gameBoard.Board[43].BoardSquareNumber);
            GameBoard2DTranslation.Add("3.5", gameBoard.Board[44].BoardSquareNumber);
            GameBoard2DTranslation.Add("4.5", gameBoard.Board[45].BoardSquareNumber);
            GameBoard2DTranslation.Add("0.4", gameBoard.Board[46].BoardSquareNumber);
            //White section NW
            GameBoard2DTranslation.Add("1.4", gameBoard.Board[47].BoardSquareNumber);
            GameBoard2DTranslation.Add("2.4", gameBoard.Board[48].BoardSquareNumber);
            GameBoard2DTranslation.Add("3.4", gameBoard.Board[49].BoardSquareNumber);
            GameBoard2DTranslation.Add("4.4", gameBoard.Board[50].BoardSquareNumber);
            GameBoard2DTranslation.Add("4.3", gameBoard.Board[51].BoardSquareNumber);
            GameBoard2DTranslation.Add("4.2", gameBoard.Board[52].BoardSquareNumber);
            GameBoard2DTranslation.Add("4.1", gameBoard.Board[53].BoardSquareNumber);
            GameBoard2DTranslation.Add("4.0", gameBoard.Board[54].BoardSquareNumber);
            GameBoard2DTranslation.Add("5.0", gameBoard.Board[55].BoardSquareNumber);
            //Goal
            GameBoard2DTranslation.Add("5.5", gameBoard.Board[56].BoardSquareNumber);

            return GameBoard2DTranslation;
        }

        private void UpdateGameBoard()
        {

            //Update square content
            foreach (KeyValuePair<string, int> square in gameBoard2D)
            {
                if (gameEngine.GameBoard.Board[square.Value].GamePieces.Any())
                {
                    gameBoardGrid.Children.OfType<Button>().Where(b => b.Tag.ToString() == square.Key).First().Content = gameEngine.GameBoard.Board[square.Value].GamePieces[0].Player.UserName;
                }
            }

            //Mark current player
            foreach (Label label in menuGamePanel.Children.OfType<Label>())
            {
                if (label.Content.ToString() == gameEngine.CurrentPlayer().UserName)
                {
                    label.BorderBrush = Brushes.Black;
                    label.BorderThickness = new Thickness(3);
                }
                else
                {
                    label.BorderBrush = Brushes.Transparent;
                    label.BorderThickness = new Thickness(0);
                }
            }
        }
    }
}
