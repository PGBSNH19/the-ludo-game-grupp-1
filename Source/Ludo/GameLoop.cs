using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EngineClasses;

namespace Ludo
{
    public class GameLoop
    {
        private GameEngine gameEngine;
        private int diceResult;
        private Player currentPlayer;
        public bool IsRunning { get; private set; }
        private string[] colors = { "Red", "Blue", "Green", "Yellow" };
        public GameLoop(GameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
            this.IsRunning = false;
        }

        public void StartLoopThread()
        {
            IsRunning = true;
            Thread gameLoop = new Thread(StartLoop);
            gameLoop.Start();
        }

        public void StopLoopThread()
        {
            this.IsRunning = false;
        }

        private void StartLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                //Find player whos turn it is
                currentPlayer = gameEngine.CurrentPlayer();


                diceResult = gameEngine.RollDice();
                //Get moveable gamepieces and move first piece in list
                if (gameEngine.MovableGamePieces(currentPlayer, diceResult).Any())
                {
                    gameEngine.MoveGamePiece(gameEngine.MovableGamePieces(currentPlayer, diceResult)[0], diceResult);
                }

                UpdateConsole(gameEngine, diceResult);
                if(gameEngine.IsWinner(currentPlayer))
                {
                    Console.WriteLine(currentPlayer.UserName + " is winner!!");
                    IsRunning = false;
                    gameEngine.CreateGameLog(currentPlayer.UserName); 
                }
                gameEngine.Session.Turns++;
                Thread.Sleep(100);
                SaveGame();
            }
        }

        public void PlayerSelect(int players)
        {
            int i = 0;
            while(i < players)
            {
                Console.WriteLine("Add username to player" + (i + 1));
                gameEngine.CreatePlayer(Console.ReadLine(), colors[i]);
                i++;
            }
            
        }
        private static void UpdateConsole(GameEngine gameEngine, int diceResult)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n");

            Console.WriteLine($"Current Player: {gameEngine.CurrentPlayer().UserName}");
            Console.WriteLine($"Dice Roll: {diceResult}");
            Console.WriteLine();

            foreach (BoardSquare square in gameEngine.GameBoard.Board)
            {
                if (square.GamePieces.Any())
                {
                    Console.Write(square.GamePieces[0].Player.UserName[0]);
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine();

            foreach (BoardSquare square in gameEngine.GameBoard.Board)
            {
                Console.ForegroundColor = ConsoleColor.Black;

                if (square.Color == "Red")
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
                else if (square.Color == "Yellow")
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                else if (square.Color == "Green")
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
                else if (square.Color == "Blue")
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                }

                if (square.StartingSquare)
                {
                    Console.Write("S");
                }
                else if (square.EndSquare)
                {
                    Console.Write("E");
                }
                else if (true)
                {
                    Console.Write("-");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
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

        public void MainMenu()
        {
            string[] menuOptions = { "new game", "load game", "save game", "exit" };
            string[] numberOfPlayers = { "2", "3", "4" };
            string menuChoice = "";
            bool menuIsRunning = true;
            while (menuIsRunning)
            {
                menuChoice = ShowMenu("LudoGame", menuOptions);

                switch (menuChoice)
                {
                    case "new game":
                        int players = int.Parse(ShowMenu("PlayerSelect", numberOfPlayers));
                        PlayerSelect(players);
                        StartLoopThread();
                        menuIsRunning = false;
                        break;
                    case "load game":
                        LoadGame();
                        StartLoopThread();
                        menuIsRunning = false;
                        break;
                    case "save game":
                        break;
                    case "exit":
                        break;
                }
            }
            ConsoleKeyInfo keyPress = new ConsoleKeyInfo();

            while (keyPress.Key != ConsoleKey.Escape)
            {
                keyPress = Console.ReadKey();

                if (keyPress.Key == ConsoleKey.Spacebar)
                {
                    if (IsRunning)
                    {
                        StopLoopThread();
                    }
                    else
                    {
                        StartLoopThread();
                    }
                }
            }
        }
    
        public void SaveGame()
        {
            gameEngine.SaveSession();
        }

        public void LoadGame()
        {
            gameEngine.PlayCurrentSession();
        }

        private static string ShowMenu(string prompt, string[] options)
        {
            Console.Clear();
            Console.WriteLine(prompt);

            int selected = 0;

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                // If this is not the first iteration, move the cursor to the first line of the menu.
                if (key != null)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = Console.CursorTop - options.Length;
                }

                // Print all the options, highlighting the selected one.
                for (int i = 0; i < options.Length; i++)
                {
                    var option = options[i];
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("- " + option);
                    Console.ResetColor();
                }

                // Read another key and adjust the selected value before looping to repeat all of this.
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Length - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }
            }

            // Reset the cursor and return the selected option.
            Console.CursorVisible = true;
            return options[selected];
        }
    }
}
