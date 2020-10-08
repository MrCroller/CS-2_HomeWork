using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;
using CS_2_HomeWork.Object;

namespace CS_2_HomeWork
{
    /// <summary>
    /// Делегат для логера
    /// </summary>
    /// <param name="msg"></param>
    public delegate void ToLog(string msg);
    class Game
    {
        private static Timer timer = new Timer();
        public static Random rnd = new Random();

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        /// <summary>
        /// Событие для логера
        /// </summary>
        public static event ToLog Write;

        /// <summary>
        /// Корабль
        /// </summary>
        private static Ship ship;

        /// <summary>
        /// Установка ширины игрового поля
        /// </summary>
        public static int Width
        {
            get => width;
            set
            {
                if (value > 1001 || value < 0) throw new ArgumentOutOfRangeException("Недопустимая ширина игрового поля");
                width = value;
            }
        }

        /// <summary>
        /// Ширина игрового поля
        /// </summary>
        private static int width;

        /// <summary>
        /// Установка высоты игрового поля
        /// </summary>
        public static int Height 
        {
            get => height;
            set
            {
                if (value > 1001 || value < 0) throw new ArgumentOutOfRangeException("Недопустимая высота игрового поля");
                height = value;
            } 
        }

        /// <summary>
        /// Выоста игрового поля
        /// </summary>
        private static int height;

        static Game()
        {
        }

        /// <summary>
        /// Метод для нажатия клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) bullets.Add(new Bullet(new Point(ship.Rect.X + 85, ship.Rect.Y + 82), new Point(20, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up) ship.Up();
            if (e.KeyCode == Keys.Down) ship.Down();
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

            // Обработчики событий
            form.KeyDown += Form_KeyDown;

            // Событие конца игры
            Ship.MessageDie += Finish;

            // Создание объекта, связывание его с формой
            // Сохранение размера формы
            form.Width = width;
            form.Height = height;

            // Связывание буфера в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            Timer timer = new Timer { Interval = 60 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Коллеция объектов
        /// </summary>
        public static List<BaseObject> objs;

        /// <summary>
        /// Коллекция астероидов
        /// </summary>
        private static List<Asteroid> asteroids;

        /// <summary>
        /// Объект пули
        /// </summary>
        private static List<Bullet> bullets = new List<Bullet> { };

        /// <summary>
        /// Фоновая музыка
        /// </summary>
        private static MediaPlayer foneM = new MediaPlayer 
        {
        };

        /// <summary>
        /// Музыка проигрыша
        /// </summary>
        private static MediaPlayer loseM = new MediaPlayer
        {
        };

        /// <summary>
        /// Метод для воспроизведения фоновой музыки
        /// </summary>
        private static void PlayLoseMusic()
        {
            foneM.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../Resources/Sanctuary_Guardian.wav")));
            foneM.Volume = 0.1;
            foneM.Play();
        }

        /// <summary>
        /// Метод для воспроизведения фоновой музыки
        /// </summary>
        private static void PlayFoneMusic()
        {
            foneM.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../Resources/PigStep.wav")));
            foneM.Volume = 0.2;
            foneM.Play();
        }

        /// <summary>
        /// Метод создания объектов
        /// </summary>
        public static void Load()
        {
            Write?.Invoke($"Начало игры");

            objs = new List<BaseObject>();
            asteroids = new List<Asteroid>();
            Random rnd = new Random();

            // Создание фонового рисунка
            Bitmap img_background = new Bitmap("../../Resources/background.png"); // Да... это не красиво. Как сделать лучше?
            objs.Add(new Planet(img_background, new Point(0,0), new Point(0), new Size(Width, Height)));

            // Старт фоновой музыки
            PlayFoneMusic();

            // Создание звёзд
            for (int i = 1; i < 40; i++)    // Звезды побольше (ближние)
                objs.Add(new Star(new Point(rnd.Next(1, Width), rnd.Next(1, Height)), new Point(3), new Size(3, 3)));
            for (int i = 1; i < 200; i++)   // Средние
                objs.Add(new Star(new Point(rnd.Next(1, Width), rnd.Next(1, Height)), new Point(2), new Size(2, 2)));
            for (int i = 1; i < 600; i++)   // Маленькие (дальние)
                objs.Add(new Star(new Point(rnd.Next(1, Width), rnd.Next(1, Height)), new Point(1), new Size(1, 1)));

            // Создание пуль
            bullets.Add(new Bullet(new Point(0, 0), new Point(0,0), new Size(4, 1)));

            // Создание корабля
            Bitmap img_ship = new Bitmap("../../Resources/bunny_ship.png");
            ship = new Ship(img_ship, new Point(10, 400), new Point(8, 8), new Size(100, 100));

            // Создание астероидов
            Bitmap img_ast = new Bitmap("../../Resources/ceres.png");
                asteroids.Add(new Asteroid(img_ast, new Point(Game.Width + rnd.Next(-50,50), rnd.Next(0, Game.Height)), new Point(3), new Size(50, 50)));
                Timer NewAster = new Timer
                {
                    Interval = 9
                };

            // Создание планет
            Bitmap img_earth = new Bitmap("../../Resources/earth.png");
            objs.Add(new Planet(img_earth, new Point(550, 500), new Point(0), new Size(800, 700)));
            Bitmap img_saturn = new Bitmap("../../Resources/saturn.png");
            objs.Add(new Planet(img_saturn, new Point(200, 150), new Point(0), new Size(70, 70)));
        }


        /// <summary>
        /// Отрисовка графики
        /// </summary>
        public static void Draw()
        {
            // Проверяем вывод графики
            Buffer.Graphics.Clear(System.Drawing.Color.Black);

            // Звезды и планеты
            foreach (BaseObject obj in objs)
                obj.Draw();

            // Астероиды
            foreach (Asteroid ast in asteroids)
                ast.Draw();

            // Пули
            foreach(Bullet bul in bullets)
                bul.Draw();

            // Корабль
            ship?.Draw();

            // Энергия корабля
            if (ship != null)
                Buffer.Graphics.DrawString("Energy:" + ship.Energy, SystemFonts.DefaultFont, System.Drawing.Brushes.White, 0, 0);

            Buffer.Render();
        }

        /// <summary>
        /// Метод обновляющий состояние объектов
        /// </summary>
        public static void Update()
        {
            // Звезды и планеты
            foreach (BaseObject obj in objs)
                obj.Update();

            // Астероиды
            foreach (Asteroid ast in asteroids)
                ast.Update();

            // Пули
            foreach (Bullet bul in bullets)
                bul.Update();

            // Ох, господь... что же это
            for (var i = 0; i < asteroids.Count; i++)
            {
                if (asteroids[i] == null) continue;
                asteroids[i].Update();
                if (bullets != null && bullets[i].Collision(asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    asteroids[i] = null;
                    bullets = null;
                    continue;
                }
                if (!ship.Collision(asteroids[i])) continue;
                var rnd = new Random();
                ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (ship.Energy <= 0) ship?.Die();
            }
        }

        /// <summary>
        /// Обработчик таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Конец игры
        /// </summary>
        public static void Finish()
        {

            timer.Stop();
            Buffer.Graphics.DrawString("Это конец?", new Font(System.Drawing.FontFamily.GenericMonospace, 70, FontStyle.Underline), System.Drawing.Brushes.White, 200, 100);
            Buffer.Render();
        }


    }
}

