using Homework.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Asteroid
    {
        protected Point Position;
        protected Point Direction;
        protected Size Size;

        public Asteroid(Point pos, Point dir, Size size)
        {
            Position = pos;
            Direction = dir;
            Size = size;
        }

        public virtual void Draw(Bitmap aster)
        {
            Game.Buffer.Graphics.DrawImage(aster, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public virtual void Update()
        {
            Position.X = Position.X + Direction.X;
            Position.Y = Position.Y + Direction.Y;

            if (Position.X < 0 || Position.X > Game.Width) Direction.X *= -1;
            if (Position.Y < 0 || Position.Y > Game.Height) Direction.Y *= -1;
        }
    }
}
