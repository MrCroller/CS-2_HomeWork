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

        /// <summary>
        /// Параметры Астерода
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        public Asteroid(Bitmap img, Point pos, Point dir, Size size) : base(img, pos, dir, size)
        {

        }

    }
}
