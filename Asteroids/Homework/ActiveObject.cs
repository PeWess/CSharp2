using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public abstract class ActiveObject : ICollision
    {
        protected Point Position;
        protected Point Direction;
        protected Size Size;

        protected ActiveObject(Point position, Point direction, Size size)
        {
            if (position.X > Game.Width + 100 || position.X < -100 || position.Y > Game.Height + 100 || position.Y < -100)
            {
                throw new GameObjectException("Объект сгенерирован слишком далеко от игрового поля");
            }
            else if(direction.X > 20 || direction.X < -20 || direction.Y > 20 || direction.Y < -20)
            {
                throw new GameObjectException("Объект двигается слишком быстро");
            }
            else if(size.Width > 100 || size.Width <= 0 || size.Height > 100 || size.Height <= 0)
            {
                throw new GameObjectException("Некорректные размеры объекта");
            }
            else
            {
                Position = position;
                Direction = direction;
                Size = size;
            }
        }

        public Point Pos 
        {
            get { return Position; }
            set { Position = value; }
        }

        public Rectangle Rect => new Rectangle(Position, Size);

        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(Rect);
        }

        public abstract void Draw();

        public abstract void Update();
    }
}
