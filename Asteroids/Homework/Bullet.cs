using Homework.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Bullet : ActiveObject
    {
        public Bullet(Point position, Point direction, Size size) : base(position, direction, size) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Resources.Bullet, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Position.X += Direction.X;
        }
    }
}
