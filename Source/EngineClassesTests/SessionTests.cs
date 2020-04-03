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


            s1.CreatePlayer("Testman","Green");

            Assert.AreEqual(4, s1.Player.ToList().First().GamePiece.Count());
        }

        [TestMethod()]
        public void CreatePlayerTest_CreateNewPlayer_OneNewPlayerWithCorrectUserName()
        {
            Session s1 = new Session();
            string userName = "Testman";
            string color = "Green";

            s1.CreatePlayer(userName,color);

            Assert.AreEqual(userName, s1.Player.ToList().First().UserName);
        }
    }
}
