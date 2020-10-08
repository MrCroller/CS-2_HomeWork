using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CS_2_HomeWork.Physics;

namespace CS_2_HomeWork.Object
{

    abstract class BaseObject : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        /// <summary>
        /// Сообщение (о гибели?)
        /// </summary>
        public delegate void Message();

        /// <summary>
        /// Свойство для картинки
        /// </summary>
        public Bitmap img { get; private set; }

        /// <summary>
        /// Параметры базового объекта
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public BaseObject(Point pos, Point dir, Size size)
        {
            if(size.Width < 0 || size.Height < 0) throw new GameObjectException(this.GetType().ToString() + "\nРазмер элемента не может быть меньше 0");
            if(dir.X > 30 || dir.Y > 30) throw new GameObjectException(this.GetType().ToString() + "\nНедопустимая скорость");

            Pos = pos;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Конструктор для параметров с картинкой
        /// </summary>
        /// <param name="img">Картинка</param>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public BaseObject(Bitmap img, Point pos, Point dir, Size size) : this(pos, dir, size)
        {
            this.img = img;
        }

        /// <summary>
        /// Абстрактный метод отрисовки
        /// </summary>
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Виртуальный метод описывающий движение.
        /// </summary>
        public virtual void Update()    // Оставил его виртуальным, т.к. есть общее поведение для Star и Asteroid
        {
            Pos.X -= Dir.X;
            Pos.Y = Pos.Y;
            if (Pos.X < 0) Pos.X = Game.Width;
        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(Pos, Size);

    }
}
