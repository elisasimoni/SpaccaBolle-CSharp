using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michele_Nardini_C_Sharp
{

    /// <summary>
    /// The class cannon
    /// </summary>
    public class Cannon : DynamicObject
    {

        private static readonly int SCARTO_X_BOLLA = 30;

        private static readonly int SCARTO_Y_BOLLA = 15;

        private static readonly float EASY = (float)3.5;

        private static readonly float NORMAL = (float)6.5;

        private static readonly float HARD = (float)11;

        private bool ballPos;

        public bool bounce;

        private Ball ball;

        public CollectBall collectBall;

        public static Ball[,] mapColor = CollectBall.getMapCollect();

        private int angle = 0;

        public int difficult = 1, index;
        public static int firstShot = 0;

        /// <summary>
        /// The costructor of this class
        /// </summary>
        /// <param name="x"> The coordinate x</param>
        /// <param name="y"> The coordinate y</param>
        /// /// <param name="width"> The width of the entity</param>
        /// <param name="height"> The height of the entity</param>
        /// <param name="collectBall"> The collection of ball</param>
        ///
        public Cannon(float x, float y, int width, int height, CollectBall collectBall) : base(x,y,width,height)
        {
            this.collectBall = collectBall;
            this.setSpeed(50);
            this.ballPos = true;
            this.bounce = true;
            this.ball = new Ball(this.x + width / 2 - SCARTO_X_BOLLA, this.y + SCARTO_Y_BOLLA - 250, Ball.BOBBLE_SIZE, Ball.BOBBLE_SIZE, getColor(), Map.index++);
            while (this.ball.color == 0)
            {
                this.ball = new Ball(this.x + width / 2 - SCARTO_X_BOLLA, this.y + SCARTO_Y_BOLLA - 250, Ball.BOBBLE_SIZE, Ball.BOBBLE_SIZE, getColor(), Map.index++);
            }
            collectBall.addBall(this.ball);
        }

        /// <summary>
        /// Manage collectBall
        ///<returns> the current array of the balls</returns>
        /// </summary>
        public CollectBall getCollectBall()
        {
            return collectBall;
        }

        /// <summary>
        /// manage creation new ball
        ///<returns> the color of the new ball</returns>
        /// </summary>
        private int getColor()
        {
            return CollectBall.randomColorCannon;
        }

        /// <summary>
        /// create the new ball
        /// </summary>
        private void newBall()
        {
            if (!this.ball.isMove && !this.ballPos)
            {
                this.ball = new Ball(this.x + width / 2 - SCARTO_X_BOLLA, this.y + SCARTO_Y_BOLLA - 250, Ball.BOBBLE_SIZE, Ball.BOBBLE_SIZE, getColor(), Map.index++);
                while (this.ball.color == 0)
                {
                    this.ball = new Ball(this.x + width / 2 - SCARTO_X_BOLLA, this.y + SCARTO_Y_BOLLA - 250, Ball.BOBBLE_SIZE, Ball.BOBBLE_SIZE, getColor(), Map.index++);
                }
                collectBall.addBall(this.ball);
                this.ballPos = true;
            }
        }

        /// <summary>
        /// shot the ball
        /// </summary>
        private void shot()
        {
            if (this.ballPos && KeyManager.space && !StateGame.pause && !CollectBall.gameOver && !CollectBall.victoryGame)
            {
                bool iter = true;
                int i = 0;

                while (iter)
                {
                    if (this.ball.x >= Map.posX[i] && this.ball.x < Map.posX[i + 1])
                    {
                        this.ball.x = Map.posX[i];
                        iter = false;
                    }

                    i++;
                }

                this.ball.directMove = (float)((this.angle - 90)*(Math.PI/180));
                this.ball.direct();
                this.ball.isMove = true;
                this.ballPos = false;
                firstShot = 1; //first shot now i can activate save
            }
        }

        /// <summary>
        /// check if the cannon need to change direction of the movement
        /// </summary>
        public void checkBounce()
        {
            if (this.x > Ball.RIGHT_BOUNCE + 5)
            {
                this.bounce = false;
            }
            else if (this.x < (-Ball.LEFT_BOUNCE + 250))
            {
                this.bounce = true;
            }
        }

        /// <summary>
        /// set the ball coordinate
        /// </summary>
        private void ballSetX()
        {
            if (!this.ball.isMove)
            {
                this.ball.setX(this.x + (float)92);
            }
        }

        /// <summary>
        /// set the speed of the cannon 
        /// </summary>
        private void speedCannon(float x)
        {
            if (this.bounce)
            {
                this.setX(this.x + (float)x);
                ballSetX();
            }
            else
            {
                this.setX(this.x - (float)x);
                ballSetX();
            }
        }

        /// <summary>
        /// Set the difficult of the game
        /// </summary>
        private void difficults()
        {
            if (!StateGame.pause && !CollectBall.gameOver && !CollectBall.victoryGame)
            {

                switch (difficult)
                {
                    case 1:
                        speedCannon(EASY);
                        break;
                    case 2:
                        speedCannon(NORMAL);
                        break;
                    case 3:
                        speedCannon(HARD);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Move the cannon
        /// </summary>
        public void cannonMove()
        {
            checkBounce();
            difficults();
        }

        /// <summary>
        /// update cannon action
        /// </summary>
        public override void tick()
        {
            cannonMove();
            shot();
            newBall();
        }

        /// <summary>
        /// render cannon
        /// </summary>
        public override void render(Graphics g)
        {
            g.DrawImage(Assets.cannon, (int)this.getX() - 50, (int)this.getY() - 280, this.getWidth(), this.getHeight(), null);
        }
    }
}
