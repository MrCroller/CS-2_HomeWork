﻿using System;
using CS_2_HomeWork.Physics;
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

        /// <summary>
        /// Энергия корабля
        /// </summary>
        public int Energy { get; internal set; } = 100;

        /// <summary>
        /// Увеличение энергии
        /// </summary>
        /// <param name="n">Ед. энергии</param>
        public void EnergyAdd(int n)
        {
            this.Energy += n;
            Logger.LogWriter($"Энергия корабля увеличена на {n} единиц, и равна {this.Energy}");
        }

        /// <summary>
        /// Уменьшение энергии
        /// </summary>
        /// <param name="n">Ед. энергии</param>
        public void EnergyLow(int n)
        {
            energy -= n;
            Logger.LogWriter($"Энергия корабля уменьшена на {n} единиц, и равна {this.Energy}");
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
        public void Shoot()
        {
            Game.bullets.Add(new Bullet(new Point(this.Rect.X + 85, this.Rect.Y + 82), new Point(20, 0), new Size(4, 1)));
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }

        /// <summary>
        /// Обновление корабля
        /// </summary>
        public override void Update()
        {
            if (Game.IsShipUpKeyPress) this.Up();
            if (Game.IsShipDownKeyPress) this.Down();
            if (Game.IsShootKeyPress) this.Shoot();
        }
    }
}
