using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_2_HomeWork.Object
{
    class Ship : BaseObject
    {
        private int energy = 100;

        /// <summary>
        /// Свойства с картинкой
        /// </summary>
        /// <param name="img">Картинка</param>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Ship(Bitmap img, Point pos, Point dir, Size size) : base(img, pos, dir, size)
        {
        }

        public static event Message MessageDie;

        public int Energy => energy;

        public void EnergyLow(int n)
        {
            energy -= n;
        }

        public void Up()
        {
            if (this.Pos.Y > -80) this.Pos.Y -= this.Dir.Y;
            if (this.Pos.Y <= -80) this.Pos.Y = Game.Height - this.Size.Height;
        }
        public void Down()
        {
            if (this.Pos.Y < Game.Height - this.Size.Height) this.Pos.Y += this.Dir.Y;
            if (this.Pos.Y + this.Size.Height >= Game.Height) this.Pos.Y = 0;
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }

    }
}
