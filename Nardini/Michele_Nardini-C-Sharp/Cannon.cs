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

        public static Ball[,] mapColor = CollectBall.GetMapCollect();

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
            this.SetSpeed(50);
            this.ballPos = true;
            this.bounce = true;
            this.ball = new Ball(this.x + width / 2 - SCARTO_X_BOLLA, this.y + SCARTO_Y_BOLLA - 250, Ball.BOBBLE_SIZE, Ball.BOBBLE_SIZE, GetColor(), Map.index++);
            while (this.ball.color == 0)
            {
                this.ball = new Ball(this.x + width / 2 - SCARTO_X_BOLLA, this.y + SCARTO_Y_BOLLA - 250, Ball.BOBBLE_SIZE, Ball.BOBBLE_SIZE, GetColor(), Map.index++);
            }
            collectBall.AddBall(this.ball);
        }

        /// <summary>
        /// Manage collectBall
        ///<returns> the current array of the balls</returns>
        /// </summary>
        public CollectBall GetCollectBall()
        {
            return collectBall;
        }

        /// <summary>
        /// manage creation new ball
        ///<returns> the color of the new ball</returns>
        /// </summary>
        private int GetColor()
        {
            return CollectBall.randomColorCannon;
        }

        /// <summary>
        /// create the new ball
        /// </summary>
        private void NewBall()
        {
            if (!this.ball.isMove && !this.ballPos)
            {
                this.ball = new Ball(this.x + width / 2 - SCARTO_X_BOLLA, this.y + SCARTO_Y_BOLLA - 250, Ball.BOBBLE_SIZE, Ball.BOBBLE_SIZE, GetColor(), Map.index++);
                while (this.ball.color == 0)
                {
                    this.ball = new Ball(this.x + width / 2 - SCARTO_X_BOLLA, this.y + SCARTO_Y_BOLLA - 250, Ball.BOBBLE_SIZE, Ball.BOBBLE_SIZE, GetColor(), Map.index++);
                }
                collectBall.AddBall(this.ball);
                this.ballPos = true;
            }
        }

        /// <summary>
        /// shot the ball
        /// </summary>
        private void Shot()
        {
            if (this.ballPos && /*KeyManager.space && !StateGame.pause &&*/ !CollectBall.gameOver && !CollectBall.victoryGame)
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
                this.ball.Direct();
                this.ball.isMove = true;
                this.ballPos = false;
                firstShot = 1; //first shot now i can activate save
            }
        }

        /// <summary>
        /// check if the cannon need to change direction of the movement
        /// </summary>
        public void CheckBounce()
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
        private void BallSetX()
        {
            if (!this.ball.isMove)
            {
                this.ball.SetX(this.x + (float)92);
            }
        }

        /// <summary>
        /// set the speed of the cannon 
        /// </summary>
        private void SpeedCannon(float x)
        {
            if (this.bounce)
            {
                this.SetX(this.x + (float)x);
                BallSetX();
            }
            else
            {
                this.SetX(this.x - (float)x);
                BallSetX();
            }
        }

        /// <summary>
        /// Set the difficult of the game
        /// </summary>
        private void Difficults()
        {
            if (/*!StateGame.pause && */!CollectBall.gameOver && !CollectBall.victoryGame)
            {

                switch (difficult)
                {
                    case 1:
                        SpeedCannon(EASY);
                        break;
                    case 2:
                        SpeedCannon(NORMAL);
                        break;
                    case 3:
                        SpeedCannon(HARD);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Move the cannon
        /// </summary>
        public void CannonMove()
        {
            CheckBounce();
            Difficults();
        }

        /// <summary>
        /// update cannon action
        /// </summary>
        public override void Tick()
        {
            CannonMove();
            Shot();
            NewBall();
        }

        /// <summary>
        /// render cannon
        /// </summary>
        public override void Render(Graphics g)
        {
            //g.DrawImage(Assets.cannon, (int)this.getX() - 50, (int)this.getY() - 280, this.getWidth(), this.getHeight(), null);
        }
    }
}
