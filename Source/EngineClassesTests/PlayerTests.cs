using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngineClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineClasses.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void SelectGamePieceTest_PickOutTheGamePieceYouWant_ReturnGamePieceBasedOnIndex()
        {
            string userName = "test";
            Player p1 = new Player(userName);
            p1.GamePiece.Add(new GamePiece(true, false));
            p1.GamePiece.Add(new GamePiece(true, false));
            p1.GamePiece.Add(new GamePiece(false, false));
            p1.GamePiece.Add(new GamePiece(true, false));


            Assert.IsFalse(p1.SelectGamePiece(2).IsAtBase);
        }
    }
}