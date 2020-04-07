using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngineClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EngineClasses.Tests
{
    [TestClass()]
    public class GameEngineTests
    {

        [TestMethod()]
        public void CreatePlayerTest_CreateNewPlayer_OneNewPlayerWithFourGamePieces()
        {
            GameEngine g1 = new GameEngine(new Session(), new GameBoard(), new GameLog());
            string userName = "Testman";
            string color = "Red";

            g1.CreatePlayer(userName, color);

            Assert.AreEqual(4, g1.Session.Player.ToList().First().GamePiece.Count());
        }

        [TestMethod()]
        public void CreatePlayerTest_CreateNewPlayer_OneNewPlayerWithCorrectUserName()
        {
            GameEngine g1 = new GameEngine(new Session(), new GameBoard(), new GameLog());
            string userName = "Testman";
            string color = "Red";

            g1.CreatePlayer(userName, color);

            Assert.AreEqual(userName, g1.Session.Player.ToList()[0].UserName);
        }
    }
}