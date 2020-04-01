using EngineClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace LudoTests
{
    [TestClass]
    public class GameEngineTests
    {
        [TestMethod]
        public void RollDiceTest_RollDiceRandomNumber_ReturnOneToSix()
        {
            GameEngine engine = new GameEngine(new Session(), new GameBoard(), new GameLog());
            int result = 0;

            result = engine.RollDice();

            Assert.IsTrue(result > 0 && result < 7);
            Debug.Print(result.ToString());
        }
    }
}
