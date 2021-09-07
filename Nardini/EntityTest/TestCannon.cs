using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Michele_Nardini_C_Sharp;

namespace EntityTest
{
    [TestClass]
    public class TestCannon
    {

        static float x = 1000;
        static float y = 10;
        static int height = 20;
        static int width = 20;
        static CollectBall collectBall = new CollectBall();
        Cannon cannon = new Cannon(x, y, width, height, collectBall);

        [TestMethod]
        public void testGetCollectBall()
        {
            Assert.AreEqual(collectBall, cannon.getCollectBall());
        }

        [TestMethod]
        public void testGetNumBall()
        {
            CollectBall collectBall = new CollectBall();
            cannon = new Cannon(x, y, width, height, collectBall);
            cannon.getCollectBall().addBall(new Ball(0, 0, 0, 0, 0, 0));
            Assert.AreEqual(2, cannon.getCollectBall().numBolle());
            cannon.getCollectBall().addBall(new Ball(1, 1, 1, 1, 1, 1));
            Assert.AreEqual(3, cannon.getCollectBall().numBolle());
        }

        [TestMethod]
        public void testSpeedCannon()
        {
            cannon.setSpeed(30);
            Assert.AreEqual(30, cannon.getSpeed());
        }

        [TestMethod]
        public void testCheckBounce()
        {
            cannon.checkBounce();
            Assert.AreEqual(false, cannon.bounce);
        }

    }
}
