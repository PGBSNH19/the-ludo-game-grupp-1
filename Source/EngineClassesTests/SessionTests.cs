using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngineClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EngineClasses.Tests
{
    [TestClass]
    public class SessionTests
    {
        [TestMethod()]
        public void CreatePlayerTest_CreateNewPlayer_OneNewPlayerWithFourGamePieces()
        {
            Session s1 = new Session();

            s1.CreatePlayer("Testman", "Green");

            Assert.AreEqual(4, s1.Players.First().GamePieces.Count());
        }

        [TestMethod()]
        public void CreatePlayerTest_CreateNewPlayer_OneNewPlayerWithCorrectUserName()
        {
            Session s1 = new Session();
            string userName = "Testman";
            string color = "Green";

            s1.CreatePlayer(userName, color);

            Assert.AreEqual(userName, s1.Players.First().UserName);
        }

        [TestMethod()]
        public void GetCurrentPlayerTest_WithFourPlayersCheckWhosTurnItIs_ReturnPlayerAtIndex0()
        {
            GameEngine engine = new GameEngine(new Session(), new GameBoard(), new GameLog(), new LudoContext());
            engine.Session.CreatePlayer("Testman1", "Red");
            engine.Session.CreatePlayer("Testman2", "Yellow");
            engine.Session.CreatePlayer("Testman3", "Green");
            engine.Session.CreatePlayer("Testman4", "Blue");

            var result = engine.Session.GetCurrentPlayer();

            Assert.AreEqual(engine.Session.Players[0], result);
            Assert.AreNotEqual(engine.Session.Players[1], result);
        }

        [TestMethod()]
        public void GetCurrentPlayerTest_WithFourPlayersCheckWhosTurnItIsAfterTurnHasIncreasedByFive_ReturnPlayerAtIndex1()
        {
            GameEngine engine = new GameEngine(new Session(), new GameBoard(), new GameLog(), new LudoContext());
            engine.Session.CreatePlayer("Testman1", "Red");
            engine.Session.CreatePlayer("Testman2", "Yellow");
            engine.Session.CreatePlayer("Testman3", "Green");
            engine.Session.CreatePlayer("Testman4", "Blue");

            engine.Session.Turns = engine.Session.Turns + 5;
            var result = engine.Session.GetCurrentPlayer();

            Assert.AreEqual(engine.Session.Players[1], result);
            Assert.AreNotEqual(engine.Session.Players[2], result);
        }

        [TestMethod()]
        public void GetCurrentPlayerTest_WithThreePlayersCheckWhosTurnItIsAfterTurnHasIncreasedByFour_ReturnPlayerAtIndex1()
        {
            GameEngine engine = new GameEngine(new Session(), new GameBoard(), new GameLog(), new LudoContext());
            engine.Session.CreatePlayer("Testman1", "Red");
            engine.Session.CreatePlayer("Testman2", "Yellow");
            engine.Session.CreatePlayer("Testman3", "Green");

            engine.Session.Turns = engine.Session.Turns + 4;
            var result = engine.Session.GetCurrentPlayer();

            Assert.AreEqual(engine.Session.Players[1], result);
            Assert.AreNotEqual(engine.Session.Players[2], result);
        }

        [TestMethod()]
        public void GetCurrentPlayerTest_WithTwoPlayersCheckWhosTurnItIsAfterTurnHasIncreasedByThree_ReturnPlayerAtIndex1()
        {
            GameEngine engine = new GameEngine(new Session(), new GameBoard(), new GameLog(), new LudoContext());
            engine.Session.CreatePlayer("Testman1", "Red");
            engine.Session.CreatePlayer("Testman2", "Yellow");

            engine.Session.Turns = engine.Session.Turns + 3;
            var result = engine.Session.GetCurrentPlayer();

            Assert.AreEqual(engine.Session.Players[1], result);
            Assert.AreNotEqual(engine.Session.Players[0], result);
        }
    }
}