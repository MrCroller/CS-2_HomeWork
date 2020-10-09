using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_2_HomeWork.Object
{
    class Kit : BaseObject
    {
        public bool IsMissing { get; set; } = false;

        public Kit(Bitmap img, Point pos, Point dir, Size size) : base(img, pos, dir, size)
        {
        }

    }
}
