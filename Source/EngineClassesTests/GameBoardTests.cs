﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            Assert.AreEqual(board.Board.Where(s => s.Color == player.Color && s.IsStartingSquare).First().Color, result.Color);
        }

        [TestMethod()]
        public void GetCurrentSquareTest_CreatePieceAndMoveThenGetCurrentSquare_CheckThatPositionIs3()
        {
            GameBoard board = new GameBoard();
            Player player = new Player("Testman", "Red");
            player.AddGamePieces();
            int boardNumber = 3;

            player.GamePieces[1].BoardSquareNumber = boardNumber;
            board.AddGamePiecesToBoard(player.GamePieces[1]);
            var result = board.GetCurrentSquare(player.GamePieces[1]);

            Assert.AreEqual(board.Board[boardNumber], result);
        }
    }
}