using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_2_HomeWork.Object
{
    /// <summary>
    /// Класс пуль
    /// </summary>
    class Bullet : BaseObject
    {
        /// <summary>
        /// Параметры пули
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        /// <summary>
        /// Отрисовка пули
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Движение пули
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + 3;
        }
    }
}
