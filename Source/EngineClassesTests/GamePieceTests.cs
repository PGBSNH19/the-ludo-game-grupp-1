using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            int gameSquareId = 4;
           
            string expectation = $"{gameSquareId}";

            gp.GameSquareId = gameSquareId;
           

            Assert.AreEqual(expectation, $"{gp.GameSquareId}");
        }
    }
}