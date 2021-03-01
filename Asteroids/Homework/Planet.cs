using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Planet
    {
        protected Point Position;
        protected Point Direction;
        protected Size Size;

        public Planet(Point pos, Point dir, Size size)
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
            if (Position.X < -300)
            {
                Position.X = Game.Width;
                Position.Y = new Random().Next(0, Game.Height);
            }
        }
    }
}