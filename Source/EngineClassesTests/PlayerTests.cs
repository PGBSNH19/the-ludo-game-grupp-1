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
        [TestMethod]
        //Under development, will allways fail for now!
        public void TestAddToDb_InsertNewRowToDb_AddedTrue()
        {
            LudoContext context = new LudoContext();
            Player player = new Player("Hampus", "Red");
            //player.AddToDb();
            Player playerFromDb = context.Player.Where(p => p.UserName == player.UserName).FirstOrDefault();

            Assert.AreEqual(player.UserName, playerFromDb.UserName);
        }
    }
}