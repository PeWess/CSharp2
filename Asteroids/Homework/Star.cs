using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Star : Planet
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.White, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Position.X = Position.X + Direction.X;
            if (Position.X < 0)
            {
                Position.X = Game.Width;
                Position.Y = new Random().Next(0, Game.Height);
            }
        }
    }
}
