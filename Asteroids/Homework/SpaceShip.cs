using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class SpaceShip : ActiveObject
    {
        public static event EventHandler GameEndEvent;
        private int health = 100;
        Bitmap Skin;

        public int Health
        {
            get { return health; }
        }

        public SpaceShip(Point position, Point direction, Size size, Bitmap skin) : base(position, direction, size)
        {
            Skin = skin;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Skin, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            //base.Update();
        }

        public void DMGTaken(int dmg)
        {
            health -= dmg;
        }

        public void HealTaken(int heal)
        {
            health += heal;
        }

        public void Up()
        {
            if(Position.Y > 0) Position.Y -= Direction.Y;
        }

        public void Down()
        {
            if (Position.Y < Game.Height) Position.Y += Direction.Y;
        }

        public void Left()
        {
            if (Position.X > 0) Position.X -= Direction.X;
        }

        public void Right()
        {
            if (Position.X < Game.Width) Position.X += Direction.X;
        }

        public void End()
        {
            if (GameEndEvent != null) GameEndEvent.Invoke(this, new EventArgs());
        }
    }
}
