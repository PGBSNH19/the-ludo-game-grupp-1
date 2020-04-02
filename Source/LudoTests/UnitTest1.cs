using EngineClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace LudoTests
{
    [TestClass]
    public class GameEngineTests
    {
        [TestMethod]
        public void RollDiceTest_RollDiceRandomNumber_ReturnOneToSix()
        {
            Player player = new Player("TestMan");
            int result = 0;

            result = player.RollDice();

            Assert.IsTrue(result > 0 && result < 7);
            Debug.Print(result.ToString());
        }

        [TestMethod]
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
