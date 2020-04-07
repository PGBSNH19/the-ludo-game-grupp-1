using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngineClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

            Assert.AreEqual(board.BoardRoute.Where(s => s.Color == player.Color && s.StartingSquare).First(), result);
        }
    }
}