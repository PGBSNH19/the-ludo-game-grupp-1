﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngineClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineClasses.Tests
{
    [TestClass()]
    public class GamePieceTests
    {
        [TestMethod()]
        public void UpdateCoordsTest_ChangeGamePiecesCoords_CoordsChangedToSpecification()
        {
            GamePiece gp = new GamePiece(true, false);
            int x = 4;
            int y = 8;
            string expectation = $"{x}{y}";

            gp.XCoord = x;
            gp.YCoord = y;

            Assert.AreEqual(expectation, $"{gp.XCoord}{gp.YCoord}");
        }
    }
}