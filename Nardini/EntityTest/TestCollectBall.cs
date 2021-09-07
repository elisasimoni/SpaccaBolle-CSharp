using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Michele_Nardini_C_Sharp;

namespace EntityTest
{
    [TestClass]
    public class TestCollectBall
    {
		CollectBall collectBall = new CollectBall();
		static int x = 10;
		static int y = 10;
		static int height = 20;
		static int width = 20;
		static int color = 1;
		static int index = 1;
		Ball ball = new Ball(x, y, height, width, color, index);
		Ball ball1 = new Ball(x, y, height, width, color, index);
		Ball ball2 = new Ball(x, y, height, width, color, index);
		int size = 3;

		public void init()
		{
			collectBall.addBall(ball);
			collectBall.addBall(ball1);
			collectBall.addBall(ball2);
		}

		[TestMethod]
		public void testNumBall()
		{
			init();
			Assert.AreEqual(size, collectBall.numBolle());
		}

		[TestMethod]
		public void testCordX()
		{
			Assert.AreEqual(ball.getX(), collectBall.cordX(ball));
		}

		[TestMethod]
		public void testCordY()
		{
			Assert.AreEqual(ball.getY(), collectBall.cordY(ball));
		}

		[TestMethod]
		public void testColor()
		{
			Assert.AreEqual(ball.Color, collectBall.color(ball));
		}

		[TestMethod]
		public void testGetBall()
		{
			Assert.AreNotEqual(collectBall, collectBall.getBolle());
		}

		[TestMethod]
		public void testAddBall()
		{
			init();
			Ball ball3 = new Ball(x, y, height, width, color, index);
			collectBall.addBall(ball3);
			Assert.AreNotEqual(size, collectBall.numBolle());
		}
	}
}
