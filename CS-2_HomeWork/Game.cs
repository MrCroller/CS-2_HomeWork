using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CS_2_HomeWork.Object;

namespace CS_2_HomeWork
{
    class Game
    {

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
        }

        /// <summary>
        /// Графическое устройство для вывода графики
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            Graphics g;

            // Предоставляет доступ к главному буферу гарфического контекста 
            // для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            // Создание объекта, связывание его с формой
            // Сохранение размера формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            // Связывание буфера в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            Timer timer = new Timer { Interval = 50 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Коллеция объектов
        /// </summary>
        public static List<BaseObject> objs;

        /// <summary>
        /// Метод создания объектов
        /// </summary>
        public static void Load()
        {
            objs = new List<BaseObject>();
            Random r = new Random();

            for (int i = 1; i < 40; i++)
                objs.Add(new Star(new Point(r.Next(1, Width), r.Next(1, Height)), new Point(3), new Size(3, 3)));
            for (int i = 1; i < 200; i++)
                objs.Add(new Star(new Point(r.Next(1, Width), r.Next(1, Game.Height)), new Point(2), new Size(2, 2)));
            for (int i = 1; i < 600; i++)
                objs.Add(new Star(new Point(r.Next(1, Width), r.Next(1, Game.Height)), new Point(1), new Size(1, 1)));

            Bitmap img_earth = new Bitmap("Resources/earth.png");
            objs.Add(new Planet(img_earth, new Point(550, 500), new Point(0), new Size(800, 700)));
            Bitmap img_saturn = new Bitmap("Resources/saturn.png");
            objs.Add(new Planet(img_saturn, new Point(200, 150), new Point(70), new Size(70, 70)));
        }

        /// <summary>
        /// Отрисовка графики
        /// </summary>
        public static void Draw()
        {
            // Проверяем вывод графики
            Buffer.Graphics.Clear(Color.Black);
            //Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(450, 330, 100, 100));

            foreach (BaseObject obj in objs)
                obj.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// Метод обновляющий состояние объектов
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
        }

        /// <summary>
        /// Обработчик таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e) //Видимо какое то событие
        {
            Draw();
            Update();
        }
    }
}

