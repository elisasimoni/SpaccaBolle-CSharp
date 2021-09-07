using System;
using System.Drawing;
using System.IO;

namespace Michele_Nardini_C_Sharp
{

	/// <summary>
	/// The class that init the object ball
	/// </summary>

	public class Ball : DynamicObject
	{

		public static readonly int LEFT_BOUNCE = 840 / 2 - 200;
		public static readonly int RIGHT_BOUNCE = 740 / 2 + 200;
		public const int BOBBLE_SIZE = 70;
		public float directMove;
		public int color, index, topConnected;
		public bool isMove;


		/// <summary>
		/// The costructor of this class
		/// </summary>
		/// <param name="x"> The coordinate x</param>
		/// <param name="y"> The coordinate y</param>
		/// /// <param name="width"> The width of the entity</param>
		/// <param name="height"> The height of the entity</param>
		/// <param name="color"> The color of ball</param>
		/// /// <param name="index"> The index of ball</param>
		///
		public Ball(float x, float y, int width, int height, int color, int index) : base(x, y, width, height)
		{
			this.color = color;
			this.index = index;
			this.directMove = 0;
			this.isMove = false;
		}

		/// <summary>
		/// manage ball
		///<returns> the color of the ball</returns>
		/// </summary>
		public int Color
		{
			get
			{
				return this.color;
			}
		}

		/// <summary>
		/// manage direct ball
		/// </summary>
		public void Direct()
		{
			this.SetxMove(Math.Cos((Math.PI/180)*(directMove)) * this.speed);
			this.SetyMove(Math.Sin((Math.PI/180)*(directMove)) * this.speed);
		}


		/// <summary>
		/// manage move ball
		/// </summary>
		private void Destroy()
		{
			if (this.y < 0)
			{
				this.isMove = false;
			}
		}

		/// <summary>
		/// manage elimination ball
		/// </summary>
		public void Eliminate()
		{
			this.SetHeight(0);
			this.SetWidth(0);
		}

		/// <summary>
		/// manage status ball
		/// </summary>
		public void BallStatus()
		{
			Ball b = new Ball(this.x, this.y, this.height, this.width, this.color, Map.index);
			this.index = Map.index;
			Map.collectBallMap.AddBall(b);
		}

		/// <summary>
		/// manage tick
		/// </summary>
		public override void Tick()
		{
			if (isMove)
			{
				if (this.x < 0 || this.x > 840)
				{
					this.xMove = this.xMove * -1;
				}
				try
				{
					if (Map.collectBallMap.roof(this.x, this.y, this))
					{
						this.isMove = false;
						BallStatus();
						if (Map.collectBallMap.Tris())
						{
							Eliminate();
						}
					}
					else
					{
						if (Map.collectBallMap.Check(this.x, this.y, this))
						{
							this.isMove = false;
							BallStatus();
							Eliminate();
							if (Map.collectBallMap.Tris())
							{
								Eliminate();
							}
						}
					}
				}
				catch (IOException e)
				{
					Console.WriteLine(e.ToString());
					Console.Write(e.StackTrace);
				}
				Move();
			}
		}

		/// <summary>
		/// manage render ball
		/// </summary>
		public override void Render(Graphics g)
		{
			g.DrawImage(Assets.ballGroup[color], (int)x, (int)y, width, height, null);
		}

		/// <summary>
		/// get ball
		///<returns> the ball</returns>
		/// </summary>
		public Ball GetBall => this;

	}
}