using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngineClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace EngineClasses.Tests
{
    [TestClass()]
    public class GameBoardTests
    {
        [TestMethod()]
        public void GetStartingSquareTest_CreateGamePiecesAndGetTheirStartingSquares_CheckThatStartingSquareColorMatchesPlayerColor()
        {
            GameBoard board = new GameBoard();
            Player player = new Player("Testman", "Red");

            var result = board.GetStartingSquare(player);

            Assert.AreEqual(board.Board.Where(s => s.Color == player.Color && s.StartingSquare).First(), result);
        }

        [TestMethod()]
        public void GetCurrentSquareTest_CreatePieceAndMoveThenGetCurrentSquare_CheckThatPositionIs3()
        {
            GameBoard board = new GameBoard();
            Player player = new Player("Testman", "Red");

            player.GamePiece[1].BoardSquareNumber = 3;
            var result = board.GetCurrentSquare(player.GamePiece[1]);
            Assert.AreEqual(board.Board.Where(gp => gp.BoardSquareNumber == 3).First(), result);

        }

        [TestMethod()]
        public void GetNextSquareTest_CreateGamePieceAddPosition_CheckPositionIsPlusOne()
        {
            GameBoard board = new GameBoard();
            Player player = new Player("Testman", "Red");

            player.GamePiece[1].BoardSquareNumber = 3;
            var result = board.GetNextSquare(player.GamePiece[1]);
            Assert.AreEqual(board.Board.Where(gp => gp.BoardSquareNumber == 3 + 1).First(), result);
        }

        [TestMethod()]
        public void FindNextValidSquareTest_AddPositionToGamePieceAndFindNextValidSquare_()
        {
            GameBoard board = new GameBoard();
            Player player = new Player("Testman", "Red");

            player.GamePiece[1].BoardSquareNumber = 0;
            
            var result = board.FindNextValidSquare(player.GamePiece[1]);
            Debug.WriteLine(result);
            
            
        }
    }
}