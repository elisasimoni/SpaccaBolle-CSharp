using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class TestScore 
    {
        Score score = new Score(0,0,0,0);
        
        [TestMethod]
        public void TestAddPoint() 
        {
            score.AddPoint(370, 5);
            Assert.AreEqual(3, score.GetNumber1());
            Assert.AreEqual(7, score.GetNumber2());
            Assert.AreEqual(3, score.GetNumber3());
            Assert.AreEqual(2, score.GetNumber4());
        }

        [TestMethod]
        public void TestPower() 
        {
            int score = Score.Power(2, 2);
            Assert.AreEqual(4, score);
        }

    }
}