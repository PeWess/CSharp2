using Homework.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Asteroid : ActiveObject
    {
        Bitmap Skin;

        public Asteroid(Point position, Point direction, Size size, Bitmap skin) : base(position, direction, size)
        {
            Skin = skin;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Skin, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Position.X = Position.X + Direction.X;
            Position.Y = Position.Y + Direction.Y;

            if (Position.X < 0 || Position.X > Game.Width) Direction.X *= -1;
            if (Position.Y < 0 || Position.Y > Game.Height) Direction.Y *= -1;
        }
    }
}
