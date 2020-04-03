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
        public void ValidateStartingSquareTest_CreateGamePiecesAndGetTheirStartingSquares_CheckThatStartingSquareColorMatchesGamePieceColor()
        {
            GameBoard board = new GameBoard();
            Player player = new Player("Testman", "Red");
            player.GamePiece.Add(new GamePiece(true, false));
            player.GamePiece.Add(new GamePiece(true, false));
            player.GamePiece.Add(new GamePiece(true, false));
            player.GamePiece.Add(new GamePiece(true, false));
            int gamePieceIndex = 0;

            var result = board.ValidateStartingSquare(player.GamePiece.ToList()[gamePieceIndex]);

            Assert.Fail();
        }
    }
}