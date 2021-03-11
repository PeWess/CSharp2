using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public abstract class BackgroundObject
    {
        protected Point Position;
        protected Point Direction;
        protected Size Size;

        protected BackgroundObject(Point position, Size size)
        {
            if (position.X > Game.Width + 400 || position.X < -400 || position.Y > Game.Height + 400 || position.Y < -400)
            {
                throw new GameObjectException("Фоновый объект сгенерирован слишком далеко от игрового поля");
            }
            else if (size.Width > 300 || size.Width <= 0 || size.Height > 300 || size.Height <= 0)
            {
                throw new GameObjectException("Некорректные размеры фонового объекта");
            }
            Position = position;
            Direction = new Point(-1, 0);
            Size = size;
        }

        public abstract void Draw();

        public abstract void Update();
    }
}
