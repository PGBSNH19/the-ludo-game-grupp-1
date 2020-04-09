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

        [TestMethod()]
        public void PlayerSelectTest_SelectFirstPlayerOfTwo_ReturnPlayerAtIndexZero()
        {
            GameEngine g1 = new GameEngine(new Session(), new GameBoard(), new GameLog());
            string p1 = "Testman";
            string p1Color = "Red";
            string p2 = "Humberto";
            string p2Color = "Yellow";

            g1.CreatePlayer(p1, p1Color);
            g1.CreatePlayer(p2, p2Color);

            var result = g1.PlayerSelect(0);

            Assert.AreEqual("Testman", result.UserName);
        }

        [TestMethod()]
        public void PlayerSelectTest_SelectFirstPlayerOfTwo_ReturnPlayerAtIndexOne()
        {
            GameEngine g1 = new GameEngine(new Session(), new GameBoard(), new GameLog());
            string p1 = "Testman";
            string p1Color = "Red";
            string p2 = "Humberto";
            string p2Color = "Yellow";

            g1.CreatePlayer(p1, p1Color);
            g1.CreatePlayer(p2, p2Color);

            var result = g1.PlayerSelect(1);

            Assert.AreEqual("Humberto", result.UserName);
        }

        [TestMethod()]
        public void CurrentPlayerTest_ReturnPlayerWhosTurnItIs_ResultFirstPlayer()
        {
            GameEngine g1 = new GameEngine(new Session(), new GameBoard(), new GameLog());
            string p1 = "Testman";
            string p1Color = "Red";
            string p2 = "Humberto";
            string p2Color = "Yellow";

            g1.CreatePlayer(p1, p1Color);
            g1.CreatePlayer(p2, p2Color);

            var result = g1.CurrentPlayer();

            Assert.AreEqual("Testman", result.UserName);
        }

        [TestMethod()]
        public void MovableGamePiecesTest_ReturnAListContainingAllPiecesAtBase_ReturnFourGamePieces()
        {
            GameEngine g1 = new GameEngine(new Session(), new GameBoard(), new GameLog());
            string p1 = "Testman";
            string p1Color = "Red";
            string p2 = "Humberto";
            string p2Color = "Yellow";

            g1.CreatePlayer(p1, p1Color);
            g1.CreatePlayer(p2, p2Color);

            int result = g1.MovableGamePieces(g1.PlayerSelect(1), 6).Count;

            Assert.AreEqual(4, result);
        }
    }
}