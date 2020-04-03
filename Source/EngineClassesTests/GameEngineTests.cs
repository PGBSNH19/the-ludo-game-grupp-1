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

            g1.CreatePlayer(userName);

            Assert.AreEqual(4, g1.Session.Player.ToList().First().GamePiece.Count());
        }

        [TestMethod()]
        public void CreatePlayerTest_CreateNewPlayer_OneNewPlayerWithCorrectUserName()
        {
            GameEngine g1 = new GameEngine(new Session(), new GameBoard(), new GameLog());
            string userName = "Testman";

            g1.CreatePlayer(userName);

            Assert.AreEqual(userName, g1.Session.Player.ToList()[0].UserName);
        }

        [TestMethod()]
        public void CurrentPlayerTurnTest_CheckIfNextPlayerTurn_NextPlayerInTurn()
        {
            GameEngine g1 = new GameEngine(new Session(), new GameBoard(), new GameLog());
            string p1 = "p1";
            string p2 = "p2";

            g1.CreatePlayer(p1);
            g1.CreatePlayer(p2);
            g1.NextTurn(1);
            Player result = g1.CurrentPlayerTurn();

            Assert.AreEqual(p2, result.UserName);
        }
    }
}