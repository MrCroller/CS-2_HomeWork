using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_2_HomeWork.Object
{
    /// <summary>
    /// Класс Астероидов
    /// </summary>
    class Asteroid : BaseObject
    {
        public int Power { get; set; }

        /// <summary>
        /// Параметры Астерода
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        /// <summary>
        /// Отрисовка Астероида
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
    }
}
