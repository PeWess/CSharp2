using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class Planet : BackgroundObject
    {
        Bitmap Skin;

        public Planet(Point position, Size size, Bitmap skin) : base(position, size)
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
            if (Position.X < -300)
            {
                Position.X = Game.Width;
                Position.Y = new Random().Next(0, Game.Height);
            }
        }
    }
}