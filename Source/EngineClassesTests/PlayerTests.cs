using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngineClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

        [TestMethod]
        public void RollDiceTest_RollDiceRandomNumber_ReturnOneToSix()
        {
            Player player = new Player("TestMan");
            List<int> results = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                results.Add(player.RollDice());
            }

            Assert.IsFalse(results.Any(r => r > 6 || r < 1));
        }

        [TestMethod]
        //Under development, will allways fail for now!
        public void TestAddToDb_InsertNewRowToDb_AddedTrue()
        {
            LudoContext context = new LudoContext();
            Player player = new Player("Hampus");
            //player.AddToDb();
            Player playerFromDb = context.Player.Where(p => p.UserName == player.UserName).FirstOrDefault();

            Assert.AreEqual(player.UserName, playerFromDb.UserName);
        }
    }
}