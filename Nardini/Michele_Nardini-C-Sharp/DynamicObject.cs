using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michele_Nardini_C_Sharp
{

    /// <summary>
    /// The abstract class that init alla the dynamic entity
    /// </summary>
    public abstract class DynamicObject : Entity
    {
        public static readonly int DEFAULT_SPEED = 10;

        protected float speed;

        protected double xMove, yMove;

        /// <summary>
        /// The costructor of this class
        /// </summary>
        /// <param name="x"> The coordinate x</param>
        /// <param name="y"> The coordinate y</param>
        /// /// <param name="width"> The width of the entity</param>
        /// <param name="height"> The height of the entity</param>
        ///
        public DynamicObject(float x, float y, int width, int height) : base(x,y,width,height)
        {
            this.speed = DynamicObject.DEFAULT_SPEED;
            this.xMove = 0;
            this.yMove = 0;
        }


        /// <summary>
        /// Move the entity
        /// </summary>
        public void Move()
        {
            x += (float)this.xMove;
            y += (float)this.yMove;
        }

        /// <summary>
        /// Gets speed
        /// </summary>
        public float GetSpeed()
        {
            return this.speed;
        }

        /// <summary>
        /// Sets the speed
        /// <param name="speed"> The speed</param>
        /// </summary>
        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        /// <summary>
        /// Get the x Move
        /// </summary>
        public double GetxMove()
        {
            return this.xMove;
        }

        /// <summary>
        /// Sets the xMove
        /// <param name="xMove"> The xMove</param>
        /// </summary>
        public void SetxMove(double xMove)
        {
            this.xMove = xMove;
        }

        /// <summary>
        /// Gets the yMove
        /// </summary>
        public double GetyMove()
        {
            return this.yMove;
        }

        /// <summary>
        /// Sets the yMove
        /// <param name="yMove"> The yMove</param>
        /// </summary>
        public void SetyMove(double yMove)
        {
            this.yMove = yMove;
        }


    }
}
