using Homework.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Comet : Planet
    {
        public Comet(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Resources.Comet1, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Position.X = Position.X + Direction.X;
            Position.Y = Position.Y + Direction.Y;
            if (Position.X < -40 || Position.Y > Game.Height)
            {
                Position.Y = -40;
                Position.X = new Random().Next(0, Game.Width);
            }
        }
    }
}
