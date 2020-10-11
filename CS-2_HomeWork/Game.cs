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

    /// <summary>
    /// Делегат для пуль
    /// </summary>
    /// <returns></returns>
    public delegate bool DelBull();

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
        /// Обработчик нажатия клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) Game.IsShipUpKeyPress = true;
            if (e.KeyCode == Keys.Down) Game.IsShipDownKeyPress = true;
            if (e.KeyCode == Keys.Space) Game.IsShootKeyPress = true;
        }

        /// <summary>
        /// Обработчик отпускания клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) Game.IsShipUpKeyPress = false;
            if (e.KeyCode == Keys.Down) Game.IsShipDownKeyPress = false;
            if (e.KeyCode == Keys.Space) Game.IsShootKeyPress = false;
        }


        /// <summary>
        /// Флаг клавиши выстрела
        /// </summary>
        public static bool IsShootKeyPress { get; set; } = false;

        /// <summary>
        /// Флаг клавиши вверх
        /// </summary>
        public static bool IsShipUpKeyPress { get; set; } = false;

        /// <summary>
        /// Флаг клавиши вниз
        /// </summary>
        public static bool IsShipDownKeyPress { get; set; } = false;


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
            form.KeyDown += GameForm_KeyDown;
            form.KeyUp += GameForm_KeyUp;

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
        /// Максимальное кол-во астероидов
        /// </summary>
        public static int MaxAsteroids { get; set; } = 8;

        /// <summary>
        /// Коллекция аптечек
        /// </summary>
        private static List<Kit> kit;

        /// <summary>
        /// Объект пули
        /// </summary>
        public static List<Bullet> bullets;

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
        /// Создание астероидов
        /// </summary>
        public static void AsterCrt()
        {
            asteroids = new List<Asteroid>(MaxAsteroids);
            Bitmap img_ast = new Bitmap("../../Resources/ceres.png");
            for (int i = 0; i < MaxAsteroids; i++)
            {
                int r = rnd.Next(50, 80);
                asteroids.Add(new Asteroid(img_ast, new Point((Game.Width + rnd.Next(700)), rnd.Next(81, Game.Height)), new Point(5), new Size(r, r)));
            }
        }

        /// <summary>
        /// Метод создания объектов
        /// </summary>
        public static void Load()
        {
            Write?.Invoke($"Начало игры");

            objs = new List<BaseObject>();
            kit = new List<Kit>();
            bullets = new List<Bullet>();
            Random rnd = new Random();

            // Создание фонового рисунка
            Bitmap img_background = new Bitmap("../../Resources/background.png"); // Да... это не красиво. Как сделать лучше?
            objs.Add(new Planet(img_background, new Point(0, 0), new Point(0), new Size(Width, Height)));

            // Старт фоновой музыки
            PlayFoneMusic();

            // Создание звёзд
            for (int i = 1; i < 40; i++)    // Звезды побольше (ближние)
                objs.Add(new Star(new Point(rnd.Next(1, Width), rnd.Next(1, Height)), new Point(3), new Size(3, 3)));
            for (int i = 1; i < 200; i++)   // Средние
                objs.Add(new Star(new Point(rnd.Next(1, Width), rnd.Next(1, Height)), new Point(2), new Size(2, 2)));
            for (int i = 1; i < 600; i++)   // Маленькие (дальние)
                objs.Add(new Star(new Point(rnd.Next(1, Width), rnd.Next(1, Height)), new Point(1), new Size(1, 1)));

            // Создание корабля
            Bitmap img_ship = new Bitmap("../../Resources/bunny_ship.png");
            ship = new Ship(img_ship, new Point(10, 400), new Point(10, 10), new Size(100, 100));

            // Создание астероидов
            AsterCrt();

            // Создание аптечек
            Bitmap img_kit = new Bitmap("../../Resources/kit.png");
            for (int i = 0; i < 50; i++)
            {
                kit.Add(new Kit(img_kit, new Point(Game.Width + (i * rnd.Next(800, 1000)), rnd.Next(10, (Game.Height - 10))), new Point(2), new Size(50, 50)));
            }


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
            foreach (Bullet bul in bullets)
                bul.Draw();

            // Корабль
            ship?.Draw();

            //Аптечки
            foreach (Kit k in kit)
                k.Draw();

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
            // Энергия корабля
            if (ship.Energy > 0) Buffer.Graphics.DrawString("Energy:" + ship.Energy, SystemFonts.DefaultFont, System.Drawing.Brushes.White, 0, 0);
            else Buffer.Graphics.Clear(System.Drawing.Color.Black);

            // Звезды и планеты
            foreach (BaseObject obj in objs)
                obj.Update();

            // Увелечение коллекции астероидов
            if (asteroids.Count == 0)
            {
                MaxAsteroids++;
                AsterCrt();
            }

            // Астероиды
            foreach (Asteroid ast in asteroids)
                ast.Update();

            // Пули
            foreach (Bullet bul in bullets)
                bul.Update();

            // Аптечки
            foreach (Kit k in kit)
                k.Update();

            // Корабль
            ship.Update();

            Random rnd = new Random();
            // Ох, господь... что же это
            for (int j = 0; j < bullets.Count; j++)
            {
                // Столкновение пули с астероидом
                for (var i = 0; i < asteroids.Count; i++)
                {
                    if (bullets[j].Collision(asteroids[i]))
                    {
                        asteroids.RemoveAt(i);
                        bullets.RemoveAt(j);
                        ship.EnergyAdd(5);
                    }
                }
                // Удаление пуль вылетевших за пределы поля
                if (bullets[j].DelBull()) bullets.RemoveAt(j);
            }

            // Столкновение астероида с кораблем
            for(var i = 0; i < asteroids.Count; i++)
            {
                if (ship.Collision(asteroids[i]))
                {
                    ship.EnergyLow(20);
                    asteroids.RemoveAt(i);
                }

                // Удаление астероидов вылетевших за пределы поля
                if (asteroids[i].DelAst())
                { 
                    asteroids.RemoveAt(i);
                    ship.EnergyLow(20);
                }
            }

            // Столкновение пули с аптечкой
            if (kit.Count > 0)
            {
                for (int i = 0; i < kit.Count; i++)
                {
                    for (int j = 0; j < bullets.Count; j++)
                    {
                        if (bullets[j].Collision(kit[i]))
                        {
                            ship.EnergyAdd(50);
                            kit.RemoveAt(i);
                            bullets.RemoveAt(j);
                        }
                    }
                }
            }

            // Здесь прокидываеться событие конца игры
            if (ship.Energy < 0) 
            {
                ship.EventDie += Finish;
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
            Buffer.Graphics.DrawString("Конец игры", new Font(System.Drawing.FontFamily.GenericMonospace, 70, FontStyle.Underline), System.Drawing.Brushes.White, 200, 100);
            Buffer.Render();
            System.Threading.Thread.Sleep(3000);
            Application.Exit();
        }
    }
}

