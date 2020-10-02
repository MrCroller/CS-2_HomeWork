using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_2_HomeWork.Object
{
    /// <summary>
    /// Класс планет
    /// </summary>
    class Planet : BaseObject
    {
        /// <summary>
        /// Свойство для картинки
        /// </summary>
        public Bitmap img { get; private set; }

        /// <summary>
        /// Свойства планет
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        public Planet(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        /// <summary>
        /// Конструктор для параметров планеты
        /// </summary>
        /// <param name="img">Картинка</param>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Planet(Bitmap img, Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            this.img = img;
        }

        /// <summary>
        /// Отрисовка планет
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Движение планет (статично)
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X;
            Pos.Y = Pos.Y;
        }
    }
}
