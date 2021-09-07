using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michele_Nardini_C_Sharp
{
    /// <summary>
    /// The abstract class that init alla the entity
    /// </summary>
    public abstract class Entity
    {

        public float x;
        public float y;
        protected int width, height;

        /// <summary>
        /// The costructor of this class
        /// </summary>
        /// <param name="x"> The coordinate x</param>
        /// <param name="y"> The coordinate y</param>
        /// /// <param name="width"> The width of the entity</param>
        /// <param name="height"> The height of the entity</param>
        /// 
        public Entity(float x,float y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Gets the x
        /// </summary>
        public float getX()
        {
            return this.x;
        }

        /// <summary>
        /// Sets the x
        /// <param name="x"> The coordinate x</param> 
        /// </summary>
        public void setX(float x)
        {
            this.x = x; 
        }

        /// <summary>
        /// Gets the y
        /// </summary>
        public float getY()
        {
            return this.y;
        }

        /// <summary>
        /// <param name="y"> The coordinate y</param>
        /// </summary>
        public void setY(float y)
        {
            this.y = y;
        }

        /// <summary>
        /// Gets width
        /// </summary>
        public int getWidth()
        {
            return this.width;
        }

        /// <summary>
        /// Sets the width
        /// <param name="width"> The width</param>
        /// </summary>
        public void setWidth(int width)
        {
            this.width = width;
        }

        /// <summary>
        /// Gets the height
        /// </summary>
        public int getHeight()
        {
            return this.height;
        }

        /// <summary>
        /// Sets the height
        /// <param name="height"> The height</param>
        /// </summary>
        public void setHeight(int height)
        {
            this.height = height;
        }

        /// <summary>
        /// Tick
        /// </summary>
        public abstract void tick();

        /// <summary>
        /// Render the image
        ///<param name="g">Graphics to render</param>
        /// </summary>
        public abstract void render(Graphics g);

    }
}
