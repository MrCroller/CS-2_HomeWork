using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CS_2_HomeWork.Object
{
    abstract class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        /// <summary>
        /// Параметры базового объекта
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Абстрактный метод отрисовки
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Абстрактный метод описывающий движение.
        /// </summary>
        public virtual void Update()    // Оставил его виртуальным, т.к. есть общее поведение для Star и Asteroid (пока что)
        {
            Pos.X -= Dir.X;
            Pos.Y = Pos.Y;
            if (Pos.X < 0) Pos.X = Game.Width;
        }
    }
}
