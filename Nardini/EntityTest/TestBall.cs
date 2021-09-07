using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Michele_Nardini_C_Sharp;

namespace EntityTest
{
	[TestClass]
	public class TestBall
	{
		static int x = 10;
		static int y = 10;
		static int height = 20;
		static int width = 20;
		static int color = 1;
		static int index = 1;
		Ball ball = new Ball(x, y, height, width, color, index);


		[TestMethod]
		public void testGetColor()
		{
			Assert.AreEqual(color, ball.Color);
		}

		[TestMethod]
		public void testGetBall()
		{
			Ball ball1 = ball.getBall;
			Assert.AreEqual(ball, ball1);
		}

		[TestMethod]
		public void testCoordinate()
		{
			Ball ball1 = ball.getBall;
			ball1.y = ball.getY();
			ball1.x = ball.getX();
			Assert.AreEqual(x, ball1.x);
			Assert.AreEqual(y, ball1.y);
		}

		[TestMethod]
		public void testEliminate()
		{
			ball.eliminate();
			Assert.AreNotEqual(height, ball.getHeight());
			Assert.AreNotEqual(width, ball.getWidth());
		}
	}
}
