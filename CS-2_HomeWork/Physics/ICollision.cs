using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CS_2_HomeWork.Physics
{
    /// <summary>
    /// Интерфейс определения столкнавений
    /// </summary>
    interface ICollision
    {
        bool Collision(ICollision obj);

        Rectangle Rect { get; }
    }
}
