using Homework.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Medkit : ActiveObject
    {
        Bitmap Skin;

        public Medkit(Point position, Point direction, Size size, Bitmap skin) : base(position, direction, size)
        {
            Skin = skin;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Resources.medkit, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            //base.Update();
        }
    }
}
