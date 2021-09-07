using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The class that init the map
/// </summary>

namespace Michele_Nardini_C_Sharp
{
    public class Map
    {
        private static readonly int RADIUS = (int)(Ball.BOBBLE_SIZE / 1.25);

        /** The Constant NROW. */
        private static readonly int NROW = 13;

        /** The Constant NCOL. */
        private static readonly int NCOL = 15;

        /** The Constant RADIUS_NINE. */
        private static readonly int RADIUS_NINE = RADIUS + 506; //nine coulumn

        /** The Constant SCARTO_Y. */
        public static readonly double SCARTO_Y = RADIUS + 5;

        /** The ball map. */
        public Ball ballMap;

        /** The index. */
        public static int index = 0; //public

        /** The map matrix. */
        public static Ball[,] mapMatrix = new Ball[NROW,NCOL];

        /** The pos X. */
        public static float[] posX = new float[NROW];

        /** The collect ball map. */
        public static CollectBall collectBallMap;

        /** The line. */
        private String line;

        /** The line dimension X. */
        private int lineDimensionX = Launcher.GAME_WIDTH;

        /** The line dimension Y. */
        private int lineDimensionY = Launcher.GAME_HEIGHT;


        /// <summary>
		/// The costructor of this class
		/// </summary>
		/// <param name="gameYSize"> The height of the map</param>
		/// <param name="gameXSize"> The width of the map</param>
		/// /// <param name="collectBall">The collection of all ball</param>
		/// <param name="level"> The file level</param>
		///
        public Map(int gameYSize, int gameXSize, CollectBall collectBall, String level)
        {
            Map.collectBallMap = collectBall;
            StreamReader reader = null;
           
            reader = LoadLevel(reader, level);
            LoadMap(reader, gameYSize);
        }

        /// <summary>
		/// manage collectBallMap
		///<returns> the collectBallMap</returns>
		/// </summary>
        public static CollectBall GetCollectBallMap()
        {
            return collectBallMap;
        }

        /// <summary>
		/// manage collectBallMap
		/// /// <param name="collectBallMap">The collection of all ball of the map</param>
		/// </summary>
        public static void SetCollectBallMap(CollectBall collectBallMap)
        {
            Map.collectBallMap = collectBallMap;
        }

        /// <summary>
		/// manage mapMatrix
		///<returns> the matrix of the map</returns>
		/// </summary>
        public static Ball[,] GetMapmatrix()
        {
            return mapMatrix;
        }

        /// <summary>
		/// The costructor of this class
		/// </summary>
		/// <param name="xMap"> The x of the map</param>
		/// <param name="yMap"> The y of the map</param>
		/// /// <param name="row">The row in the map matrix</param>
		/// <param name="col"> The col in the map matrix</param>
        /// <param name="color"> The color of the ball</param>
        /// <param name="map"> The map matrix</param>
        /// <param name="index"> The index of the ball</param>
		///
        private void LoadCoordinate(int xMap, int yMap, int row, int col, int color, Ball[,] map, int index)
        {
            map[row,col] = new Ball(xMap, yMap, Ball.BOBBLE_SIZE, Ball.BOBBLE_SIZE, color, index); /*caricamento matrice*/
        }

        /// <summary>
        /// load level
        /// <param name="reader"> The reader</param>
		/// <param name="level"> The level of the map</param>
        /// </summary>
        private StreamReader LoadLevel(StreamReader reader, String level)
        {
            if (level != null)
            {

                //String filePath = new File("").getAbsolutePath(); 
                //System.out.println(filePath+level.getName());
                reader = File.OpenText(level);
                if (reader == null)
                {

                }
                //System.out.println("Creato livello");        	    
            }
            else
            {
                // String filePath = new File("").getAbsolutePath();
                reader = new System.IO.StreamReader("/res/map/level1.txt");


            }
            return reader;
        }

        /// <summary>
        /// load level
        /// <param name="reader"> The reader</param>
		/// <param name="gameYSize"> The height of the map</param>
        /// </summary>
        private void LoadMap(StreamReader reader, int gameYSize)
        {
            int posLine = 0;
            CollectBall.flyngPoint = 0;

            try
            {
                line = reader.ReadLine();
            }
            catch (System.IO.IOException e1)
            {
                // TODO Auto-generated catch block
                e1.ToString();
            }

            while (line != null)
            {
                for (int i = 0; i < line.Length; i++)
                {

                    char elem = line.ElementAt(i);
                    String checkElem = Convert.ToString(elem);
                    int posChar = i;
                    if (posChar == 13)
                    {
                        posChar = 0;
                    }

                    int readBobble = 0;

                    switch (checkElem)
                    {
                        case "0":
                            readBobble = 0;
                            break;
                        case "1":
                            readBobble = 1;
                            break;
                        case "2":
                            readBobble = 2;
                            break;
                        case "3":
                            readBobble = 3;
                            break;
                        case "4":
                            readBobble = 4;
                            break;
                        default:
                            break;

                    }

                    switch (posLine)
                    {
                        case 0:
                            lineDimensionY = gameYSize;
                            break;
                        case 1:
                            lineDimensionY = gameYSize + RADIUS;
                            break;
                        case 2:
                            lineDimensionY = gameYSize + (2 * RADIUS);
                            break;
                        case 3:
                            lineDimensionY = gameYSize + (3 * RADIUS);
                            break;
                        case 4:
                            lineDimensionY = gameYSize + (4 * RADIUS);
                            break;
                        case 5:
                            lineDimensionY = gameYSize + (5 * RADIUS);
                            break;
                        case 6:
                            lineDimensionY = gameYSize + (6 * RADIUS);
                            break;
                        case 7:
                            lineDimensionY = gameYSize + (7 * RADIUS);
                            break;
                        case 8:
                            lineDimensionY = gameYSize + (8 * RADIUS);
                            break;
                        default:
                            break;
                    }

                    switch (posChar)
                    {
                        case 0:
                            lineDimensionX = RADIUS;
                            break;
                        case 1:
                            lineDimensionX = 2 * RADIUS;
                            break;
                        case 2:
                            lineDimensionX = 3 * RADIUS;
                            break;
                        case 3:
                            lineDimensionX = 4 * RADIUS;
                            break;
                        case 4:
                            lineDimensionX = 5 * RADIUS;
                            break;
                        case 5:
                            lineDimensionX = 6 * RADIUS;
                            break;
                        case 6:
                            lineDimensionX = 7 * RADIUS;
                            break;
                        case 7:
                            lineDimensionX = 8 * RADIUS;
                            break;
                        case 8:
                            lineDimensionX = 9 * RADIUS;
                            break;
                        case 9:
                            lineDimensionX = RADIUS_NINE;
                            break;
                        case 10:
                            lineDimensionX = 11 * RADIUS + 2;
                            break;
                        case 11:
                            lineDimensionX = 12 * RADIUS + 2;
                            break;
                        case 12:
                            lineDimensionX = 13 * RADIUS + 2;
                            break;
                        default:
                            break;
                    }

                    ballMap = new Ball(lineDimensionX, lineDimensionY, Ball.BOBBLE_SIZE, Ball.BOBBLE_SIZE, readBobble, index);
                    LoadCoordinate(lineDimensionX, lineDimensionY, posLine, posChar, readBobble, mapMatrix, index++);

                    posX[posChar] = lineDimensionX;
                    collectBallMap.AddBall(ballMap);

                }

                try
                {
                    line = reader.ReadLine();
                }
                catch (System.IO.IOException e)
                {
                    e.ToString();
                }
                posLine++;
            }
        }
    }
}
