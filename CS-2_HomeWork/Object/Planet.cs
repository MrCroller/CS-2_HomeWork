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
        /// Свойства планеты
        /// </summary>
        /// <param name="img">Картинка</param>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Planet(Bitmap img, Point pos, Point dir, Size size) : base(img, pos, dir, size)
        {
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
