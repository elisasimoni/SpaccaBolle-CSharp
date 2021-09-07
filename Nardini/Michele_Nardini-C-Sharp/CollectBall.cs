using java.io;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michele_Nardini_C_Sharp
{

	/// <summary>
	/// The class that init the map
	/// </summary>
	public class CollectBall
	{
		private static int NUMROW = 8;

		/** The numrow1. */
		private static int NUMROW1 = 9;

		/** The numcol. */
		private static int NUMCOL = 13;

		/** The numcol1. */
		private static int NUMCOL1 = 14;

		/** The roff limit. */
		private static int ROOF_LIMIT = 40;


		/** The collection ball. */
		private List<Ball> collectionBall;

		/** The map collect. */
		public static Ball[,] mapCollect = Map.getMapmatrix();

		/** The game over. */
		public static bool gameOver = false;

		/** The victory. */
		public static bool victoryGame = false;

		/** The score. */
		public static Score score = new Score(0, 0, 0, 0);

		/** The add point. */
		public static int addPoint = 0;

		/** The point. */
		public static int point = 0;

		/** The rand. */
		private static Random rand = new Random();

		/** The random color cannon. */
		public static int randomColorCannon = 1;

		/** The flyng point. */
		public static int flyngPoint = 0;

		/** The check tris. */
		private bool checkTris = false;



		/// <summary>
		/// The costructor of this class
		/// </summary>
		///
		public CollectBall()
		{
			collectionBall = new List<Ball>();
		}

		/// <summary>
		/// manage MapCollect
		///<returns> the matrix of ball in the map</returns>
		/// </summary>
		public static Ball[,] getMapCollect()
		{
			return mapCollect;
		}

		/// <summary>
		/// tick
		/// </summary>
		public void tick()
		{
			foreach (Ball b in collectionBall)
			{
				b.tick();
			}
		}

		/// <summary>
		/// manage MapCollect
		///<returns> the color random</returns>
		/// </summary>
		public static int getColorInMap()
		{
			int blue = 0, yellow = 0, green = 0, red = 0;
			int random = 0;
			int right = 0;

			for (int r = 0; r < NUMROW1; r++)
			{
				for (int c = 0; c < NUMCOL; c++)
				{

					int readColor = mapCollect[r, c].color;

					switch (readColor)
					{
						case 1:
							red++;
							break;
						case 2:
							blue++;
							break;
						case 3:
							green++;
							break;
						case 4:
							yellow++;
							break;
						default:
							break;
					}
				}
			}

			while (right < 1)
			{
				random = rand.Next(5);
				right = 1;
				if ((blue == 0 && random == 2) || (red == 0 && random == 1) || (green == 0 && random == 3) || (yellow == 0 && random == 4) || (random == 0))
				{
					right = 0;
				}
			}

			return random;
		}

		/// <summary>
		/// find num of ball
		///<returns> the number of ball</returns>
		/// </summary>
		public int numBolle()
		{
			return collectionBall.Count;
		}

		/// <summary>
		/// manage cord
		///<returns> the cordX of ball</returns>
		/// </summary>
		public float cordX(Ball b)
		{
			return b.x;
		}

		/// <summary>
		/// manage cord
		///<returns> the cordY of ball</returns>
		/// </summary>
		public float cordY(Ball b)
		{
			return b.y;
		}

		/// <summary>
		/// manage color
		///<returns> the color of ball</returns>
		/// </summary>
		public int color(Ball b)
		{
			return b.color;
		}

		/// <summary>
		/// manage gameOver
		/// <param name="coordinateY"> The coordY of ball</param>
		/// </summary>
		private void gameOverCheck(float coordinateY)
		{
			if (coordinateY > 350)
			{
				System.Console.WriteLine("Hai perso");
				saveGame(mapCollect);
				gameOver = true;
			}
		}

		/// <summary>
		/// manage roof
		/// <param name="coordinateY"> The coordY of ball</param>
		/// <param name="coordinateX"> The coordX of ball</param>
		/// <param name="b"> The ball</param>
		/// </summary>
		public bool roof(float coordinateX, float coordinateY, Ball b)
		{
			int saveCol = 0;
			bool check = false;

			foreach (Ball bobble in collectionBall)
			{
				if (coordinateX >= bobble.getX() && coordinateX <= bobble.getX() + bobble.getWidth())
				{

					if (coordinateY < ROOF_LIMIT && bobble.getY() < ROOF_LIMIT && bobble.color == 0)
					{


						for (int r = 0; r < NUMROW1; r++)
						{
							for (int c = 0; c < NUMCOL; c++)
							{
								if (mapCollect[r,c].index == bobble.index)
								{

									b.x = mapCollect[r,c].x;
									b.y = mapCollect[r,c].y;
									mapCollect[r,c].color = b.color;
								}
							}
						}

						randomColorCannon = getColorInMap();



						check = true;
					}
				}
			}

			return check;
		}


		/// <summary>
		/// manage check
		/// <param name="coordinateY"> The coordY of ball</param>
		/// <param name="coordinateX"> The coordX of ball</param>
		/// <param name="b"> The ball</param>
		/// </summary>
		public bool check(float coordinateX, float coordinateY, Ball b)
		{

			bool check = false;
			int saveCol = 0;

			foreach (Ball bobble in collectionBall)
			{
				if ((coordinateY < (cordY(bobble) + Map.SCARTO_Y) && bobble.color != 0))
				{
					if (coordinateX >= cordX(bobble) && coordinateX < (cordX(bobble)) + bobble.getWidth() - 15)
					{

						float saveX = cordX(bobble);
						b.x = cordX(bobble);
						bool isEqual = true;
						int tempSaveCol = 0;

						while (isEqual)
						{
							if (coordinateX != mapCollect[0,tempSaveCol].x)
							{
								if (coordinateX <= mapCollect[0,tempSaveCol].x + 55)
								{
									isEqual = false;
									saveCol = tempSaveCol;
								}
								else
								{
									tempSaveCol++;
									if (tempSaveCol == NUMCOL)
									{
										isEqual = false;
									}
								}
							}
							else
							{
								isEqual = false;
								saveCol = tempSaveCol;
							}
						}

						for (int col = 1; col < NUMCOL; col++)
						{
							if (b.x == mapCollect[0,col].x)
							{
								saveCol = col;
							}
						}

						for (int row = 0; row < NUMROW; row++)
						{
							if (b.y >= mapCollect[row,saveCol].y - 30 && b.y <= mapCollect[row,saveCol].y + 30)
							{
								mapCollect[row,saveCol] = b;
								saveGame(mapCollect);
								randomColorCannon = getColorInMap();
							}
						}

						/*
						* Controllo Game Over
						*/
						this.gameOverCheck(coordinateY);
						check = true;
					}
				}
			}
			return check;

		}

		private void fiveCheck()
		{

			int count = 0;

			/* 
			 * Controllo Orizzontale a 5
			 */
			for (int r = 0; r < NUMROW; r++)
			{
				for (int c = 0; c < NUMCOL; c++)
				{
					int c2 = c + 1;
					int c3 = c + 2;
					int c4 = c - 1;
					int c5 = c - 2;

					if (c4 < 0)
					{
						c4 = 0;
					}

					if (c5 < 0)
					{
						c5 = 0;
					}

					if (mapCollect[r,c] != null && mapCollect[r,c2] != null && mapCollect[r,c3] != null && mapCollect[r,c4] != null && mapCollect[r,c5] != null
					   && mapCollect[r,c].color != 0 && mapCollect[r,c2].color != 0 && mapCollect[r,c3].color != 0 && mapCollect[r,c4].color != 0 && mapCollect[r,c5].color != 0)
					{
						if (mapCollect[r,c].color == mapCollect[r,c2].color && mapCollect[r,c2].color == mapCollect[r,c3].color && mapCollect[r,c3].color == mapCollect[r,c4].color
						   && mapCollect[r,c4].color == mapCollect[r,c5].color)
						{

							mapCollect[r,c].color = 0;
							mapCollect[r,c2].color = 0;
							mapCollect[r,c3].color = 0;
							mapCollect[r,c4].color = 0;
							mapCollect[r,c5].color = 0;
							mapCollect[r,c].eliminate();
							mapCollect[r,c2].eliminate();
							mapCollect[r,c3].eliminate();
							mapCollect[r,c4].eliminate();
							mapCollect[r,c5].eliminate();
							this.checkTris = true;

							if (addPoint == 0)
							{
								addPoint = 1;
								point = point + 5;
							}
						}
					}

					victory(mapCollect);
				}
			}
		}

		/**
		 * Four and three check.
		 */
		private void fourAndThreeCheck()
		{

			int count = 0;

			/* Controllo Orizzontale 4 e 3 */
			for (int r = 0; r < NUMROW1; r++)
			{
				for (int c = 0; c < NUMCOL; c++)
				{
					int c2 = c + 1;
					int c3 = c + 2;
					int c4 = c + 3;
					int c5 = c + 5;

					if (mapCollect[r,c] != null && mapCollect[r,c2] != null && mapCollect[r,c3] != null && mapCollect[r,c4] != null && mapCollect[r,c].color != 0
					   && mapCollect[r,c2].color != 0 && mapCollect[r,c3].color != 0 && mapCollect[r,c4].color != 0)
					{
						if (mapCollect[r,c].color == mapCollect[r,c2].color && mapCollect[r,c2].color == mapCollect[r,c3].color)
						{
							if (mapCollect[r,c3].color == mapCollect[r,c4].color)
							{

								checkTris = true;
								mapCollect[r,c].color = 0;
								mapCollect[r,c2].color = 0;
								mapCollect[r,c3].color = 0;
								mapCollect[r,c4].color = 0;
								mapCollect[r,c].eliminate();
								mapCollect[r,c2].eliminate();
								mapCollect[r,c3].eliminate();
								mapCollect[r,c4].eliminate();

								if (addPoint == 0)
								{
									addPoint = 1;
									point = point + 4;
								}
							}
							else
							{

								checkTris = true;
								mapCollect[r,c].color = 0;
								mapCollect[r,c2].color = 0;
								mapCollect[r,c3].color = 0;
								mapCollect[r,c].eliminate();
								mapCollect[r,c2].eliminate();
								mapCollect[r,c3].eliminate();

								if (addPoint == 0)
								{
									addPoint = 1;
									point = point + 3;
								}
							}

							victory(mapCollect);

						}
					}
				}
			}
		}

		/**
		 * Three vertical check.
		 */
		private void threeVerticalCheck()
		{

			int count = 0;

			/*Controllo Tris Verticale a 3 */
			for (int r = 0; r < NUMROW1; r++)
			{
				for (int c = 0; c < NUMCOL; c++)
				{
					int r2 = r + 1;
					int r3 = r + 2;

					if (mapCollect[r,c] != null && mapCollect[r2,c] != null && mapCollect[r3,c] != null && mapCollect[r,c].color != 0 && mapCollect[r2,c].color != 0
					   && mapCollect[r3,c].color != 0)
					{
						if (mapCollect[r,c].color == mapCollect[r2,c].color && mapCollect[r2,c].color == mapCollect[r3,c].color)
						{

							checkTris = true;
							mapCollect[r,c].color = 0;
							mapCollect[r2,c].color = 0;
							mapCollect[r3,c].color = 0;
							mapCollect[r,c].eliminate();
							mapCollect[r2,c].eliminate();
							mapCollect[r3,c].eliminate();

							if (addPoint == 0)
							{
								addPoint = 1;
								point = point + 3;
							}

							victory(mapCollect);
						}
					}
				}
			}
		}

		/**
		 * Ball attached one check.
		 */
		private void ballAttachedOneCheck()
		{

			int count = 0;

			/*controllo 1 pallina attaccate al vuoto*/

			for (int r = 0; r < 9; r++)
			{
				for (int c = 0; c < 13; c++)
				{

					int r2 = r - 1;

					if (r2 == -1)
					{
						r2 = 0;
					}

					int c3 = c + 2;

					if (mapCollect[r,c] != null && mapCollect[r2,c] != null)
					{
						if (mapCollect[r2,c].color == 0)
						{

							checkTris = true;
							mapCollect[r,c].color = 0;
							mapCollect[r,c].eliminate();

							if (addPoint == 0)
							{
								addPoint = 1;
								flyngPoint = 0;
							}

							victory(mapCollect);
						}
					}
				}
			}
		}

		/**
		 * Ball attached plus check.
		 */
		private void ballAttachedPlusCheck()
		{


			/* Controllo + Palline Attaccate Al Vuoto*/

			for (int r = 0; r < NUMROW1; r++)
			{
				for (int c = 0; c < NUMCOL1; c++)
				{

					bool stop = false;
					int i = 0;
					int r2 = r - i;
					int r3 = r2 - 1;

					if (r2 < 0)
					{
						r2 = 0;
					}

					if (r3 < 0)
					{
						r3 = 0;
					}

					i++;
					if (mapCollect[0,c] != null && mapCollect[0,c].color == 0)
					{
						for (int row = 0; row < 8; row++)
						{

							mapCollect[row,c].color = 0;
							mapCollect[row,c].eliminate();

							if (addPoint == 0)
							{
								addPoint = 1;
								flyngPoint = r;
								if (flyngPoint == 1)
								{
									flyngPoint = 0;
								}
							}
						}
					}

					if (mapCollect[r2,c] != null)
					{
						if (mapCollect[r2,c].color == 0)
						{

							checkTris = true;
							mapCollect[r,c].color = 0;
							mapCollect[r,c].eliminate();

							if (addPoint == 0)
							{
								addPoint = 1;
								flyngPoint = r;
								if (flyngPoint == 1)
								{
									flyngPoint = 0;
								}
							}

							victory(mapCollect);

							stop = true;
						}
					}
				}
			}
		}

		/**
		 * Ball horizontal check.
		 */
		private void ballHorizontalCheck()
		{

			/*Controllo Orizzontale Particolare*/

			for (int r = 0; r < NUMROW1; r++)
			{
				for (int c = 0; c < NUMCOL1; c++)
				{

					int c2 = c - 1;

					if (c2 < 0)
					{
						c2 = 0;
					}

					int c3 = c - 2;

					if (c3 < 0)
					{
						c3 = 0;
					}

					if (mapCollect[r,c] != null && mapCollect[r,c2] != null && mapCollect[r,c3] != null && mapCollect[r,c].color != 0 && mapCollect[r,c2].color != 0
					   && mapCollect[r,c3].color != 0)
					{
						if (mapCollect[r,c].color == mapCollect[r,c2].color && mapCollect[r,c2].color == mapCollect[r,c3].color)
						{

							mapCollect[r,c].color = 0;
							mapCollect[r,c2].color = 0;
							mapCollect[r,c3].color = 0;
							mapCollect[r,c].eliminate();
							mapCollect[r,c2].eliminate();
							mapCollect[r,c3].eliminate();

							checkTris = true;

							if (addPoint == 0)
							{
								addPoint = 1;
								point = point + 3;
							}

							victory(mapCollect);
						}
					}
				}
			}

		}

		/// <summary>
		/// manage tris
		/// <return>if there is a tris of ball of the same color</return>
		/// </summary>
		public bool tris()
		{


			addPoint = 0;


			this.fiveCheck();

			this.fourAndThreeCheck();

			this.threeVerticalCheck();

			this.ballHorizontalCheck();

			this.ballAttachedOneCheck();

			this.ballAttachedPlusCheck();


			for (int r = 0; r < NUMROW; r++)
			{
				for (int c = 0; c < NUMCOL; c++)
				{

					if (mapCollect[r,c].color == 0)
					{
						foreach (Ball b in collectionBall)
						{

							if (mapCollect[r,c].getX() == b.getX() && mapCollect[r,c].getY() == b.getY())
							{

								b.color = 0;
								b.eliminate();
							}
						}
					}
				}
			}

			//confronto mappa e collection Ball
			for (int r = 0; r < NUMROW1; r++)
			{
				for (int c = 0; c < NUMCOL; c++)
				{

					if (mapCollect[r,c].color == 0)
					{
						foreach (Ball b in collectionBall)
						{

							if (mapCollect[r,c].index == b.index)
							{
								b.color = 0;
								b.eliminate();

							}
						}
					}
				}
			}

			score.addPoint(point, flyngPoint);

			return checkTris;
		}

		/// <summary>
		/// saveGame
		/// <param name="matrix"> The actual matrix map</param>
		/// </summary>
		public void saveGame(Ball[,] matrix)
		{
			string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "save.txt")))
			{
				for (int r = 0; r < NUMROW1; r++)
				{
					for (int c = 0; c < NUMCOL; c++)
					{
						outputFile.Write(matrix[r, c].color);
					}
					outputFile.WriteLine();
				}
				outputFile.Close();
			}
		}

		/// <summary>
		/// manage victory
		/// <param name="map"> The actual matrix map</param>
		/// </summary>
		private void victory(Ball[,] map)
		{
			int count = 0;
			for (int r1 = 0; r1 < NUMROW; r1++)
			{
				for (int c1 = 0; c1 < NUMCOL; c1++)
				{
					if (map[r1,c1].color != 0)
					{
						count++;
					}
				}
			}
			if (count == 0)
			{
				victoryGame = true;
			}
		}

		/// <summary>
		/// manage balls
		///<return>all the ball</return>
		/// </summary>
		public List<Ball> getBolle()
		{
			return collectionBall;
		}

		/// <summary>
		/// manage render
		/// <param name="g"> The graphics to render</param>
		/// </summary>
		public void render(Graphics g)
		{

			foreach (Ball b in collectionBall)
			{
				b.render(g);
			}


			score.render(g);
		}

		/// <summary>
		/// manage addBall
		/// <param name="b"> The ball to add</param>

		/// </summary>
		public void addBall(Ball b)
		{
			collectionBall.Add(b);
		}
	}
}
