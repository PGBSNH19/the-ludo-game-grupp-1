using System;
using EngineClasses;

namespace Ludo
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard gameBoard = new GameBoard();
            int horizontal = gameBoard.Placements.GetUpperBound(0);
            int vertical = gameBoard.Placements.GetUpperBound(0);

            for (int i = 0; i <= horizontal; i++)
            {
                for (int j = 0; j <= vertical; j++)
                {

                    string res = gameBoard.Placements[i, j];
                    Console.Write(res + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
