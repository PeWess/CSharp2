using Homework.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{

    public class GameObjectException : Exception
    {
        public GameObjectException(string message) : base(message)
        {

        }
    }

    public delegate string Logger<T>(T arg);

    public static class LogMessages
    {
        private static StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "GameLog.txt", false);

        public static void GameStart()
        {
            sw.WriteLine("Начало игры.");
        }

        public static void GameEnd()
        {
            sw.Close();
        }

        public static string DMGMessage(int dmg)
        {
            return $"Корабль получил {dmg} урона.";
        }

        public static string HealMessage(int heal)
        {
            return $"Корабль восстановил {heal} здоровья.";
        }

        public static string AsteroidMessage(int asteroidsLeft)
        {
            return $"Астероид уничтожен. Осталось {asteroidsLeft} астероидов.";
        }

        public static string EndMessage(string result)
        {
            return $"Раунд закончен. Пользователь {result}.";
        }

        public static void PrintMessage(string msg)
        {
            Debug.WriteLine(msg);
            sw.WriteLine(msg);
        }
    }

    public static class Game
    {
        public static Timer timer;
        public static Timer timerForPlanets;
        public static Timer reload;
        public static Timer nextRnd = new Timer { Interval = 5000 };
        public static Random rnd = new Random();

        public static List<Bitmap> skins = new List<Bitmap> { Resources.Asteroid1, Resources.Asteroid2, Resources.Asteroid3, Resources.Asteroid4, Resources.Asteroid5 };
        public static List<Bitmap> skinsForPlanets = new List<Bitmap> { Resources.Planet1, Resources.Planet2, Resources.Planet3 };

        #region Private Fields

        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;
        private static List<ActiveObject> _asteroids = new List<ActiveObject>();
        private static List<ActiveObject> _bullets = new List<ActiveObject>();
        private static ActiveObject _medkit;
        private static SpaceShip _spaceShip;
        private static BackgroundObject[] _planets;
        private static BackgroundObject[] _stars;
        private static BackgroundObject[] _comets;
        private static int points = 0;
        private static int asteroidsLeft = 10;
        private static int round = 1;
        private static bool reloaded = true;

        #endregion

        #region Properties

        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BufferedGraphics Buffer
        {
            get { return _buffer; }
        }

        #endregion

        #region Constructors

        static Game() { }

        #endregion

        #region Public Methods

        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            try
            {
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;

                if (Width < 0 || Width > 1000 || Height < 0 || Height > 1000) throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Некорректные длина и ширина окна.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.GetCurrentProcess().Kill();
            }

            _buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            load();

            timer = new Timer { Interval = 5 };
            timer.Start();
            timer.Tick += Timer_Tick;

            timerForPlanets = new Timer { Interval = 50 };
            timerForPlanets.Start();
            timerForPlanets.Tick += TimerForPlanets_Tick;

            reload = new Timer { Interval = 500 };
            reload.Start();
            reload.Tick += Reload_Tick;

            form.KeyDown += Form_KeyDown;

            LogMessages.GameStart();
        }

        public static void load()
        {
            try
            {
                _stars = new BackgroundObject[300];
                for (int i = 0; i < _stars.Length; i++)
                {
                    var size = 1;
                    _stars[i] = new Star(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Size(size, size));
                }

                _comets = new BackgroundObject[5];
                for (int i = 0; i < _comets.Length; i++)
                {
                    var size = rnd.Next(10, 40);
                    _comets[i] = new Comet(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Size(size, size), Resources.Comet1);
                }

                _planets = new BackgroundObject[3];
                for (int i = 0; i < _planets.Length; i++)
                {
                    var size = rnd.Next(50, 300);
                    _planets[i] = new Planet(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Size(size, size), skinsForPlanets[rnd.Next(0, 3)]);
                }

                for (int i = 0; i < asteroidsLeft; i++)
                {
                    var size = rnd.Next(15, 50);
                    _asteroids.Add(new Asteroid(new Point(rnd.Next(200, Width), rnd.Next(0, Height)), new Point(rnd.Next(-5, 5), rnd.Next(-5, 5)), new Size(size, size), skins[rnd.Next(0, 5)]));
                }

                _medkit = new Medkit(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(0, 0), new Size(30, 30), Resources.medkit);

                _spaceShip = new SpaceShip(new Point(10, 400), new Point(10, 10), new Size(90, 100), Resources.SpaceShip);

            }
            catch (GameObjectException ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.GetCurrentProcess().Kill();
            }
        }

        public static void Draw()
        {
            _buffer.Graphics.Clear(Color.Black);

            foreach (var star in _stars)
                star.Draw();

            foreach (var comet in _comets)
                comet.Draw();

            foreach (var planet in _planets)
                planet.Draw();

            foreach (var asteroid in _asteroids)
                asteroid.Draw();

            foreach (var bullet in _bullets)
                bullet.Draw();

            _medkit.Draw();

            if (_spaceShip != null)
            {
                _spaceShip.Draw();
                Buffer.Graphics.DrawString($"HP: { _spaceShip.Health}", SystemFonts.DefaultFont, Brushes.White, 10, 10);
            }

            Buffer.Graphics.DrawString($"Points: {points}", SystemFonts.DefaultFont, Brushes.White, 10, 20);

            _buffer.Render();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey && reloaded == true)
            {
                _bullets.Add(new Bullet(new Point(_spaceShip.Rect.X + 50, _spaceShip.Rect.Y + 46), new Point(5, 0), new Size(50, 10)));
                reloaded = false;
            }

            if (e.KeyCode == Keys.Up) _spaceShip.Up();
            if (e.KeyCode == Keys.Down) _spaceShip.Down();
            if (e.KeyCode == Keys.Left) _spaceShip.Left();
            if (e.KeyCode == Keys.Right) _spaceShip.Right();
        }

        private static void TimerForPlanets_Tick(object sender, EventArgs e)
        {
            foreach (var star in _stars)
                star.Update();

            foreach (var comet in _comets)
                comet.Update();

            foreach (var planet in _planets)
                planet.Update();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        private static void Reload_Tick(object sender, EventArgs e)
        {
            reloaded = true;
        }

        public static void Update()
        {
            _medkit.Update();

            foreach (var bullet in _bullets)
            {
                bullet.Update();
            }

            if (_spaceShip.Collision(_medkit))
            {
                int heal = rnd.Next(1, 16);
                _spaceShip.HealTaken(heal);
                _medkit = new Medkit(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(0, 0), new Size(30, 30), Resources.medkit);
                Logger<int> msg = LogMessages.HealMessage;
                LogMessages.PrintMessage(msg(heal));
            }

            for (int i = 0; i < _asteroids.Count; i++)
            {

                _asteroids[i].Update();


                if (_asteroids[i].Collision(_spaceShip))
                {
                    _asteroids[i] = new Asteroid(new Point(Game.Width, rnd.Next(0, Height)), new Point(rnd.Next(-5, 5), rnd.Next(-5, 5)), new Size(rnd.Next(15, 50), rnd.Next(15, 50)), skins[rnd.Next(0, 5)]);
                    _spaceShip.DMGTaken(10 + round);
                    points -= 5;
                    Logger<int> msg = LogMessages.DMGMessage;
                    LogMessages.PrintMessage(msg(10 + round));

                    if (_spaceShip.Health <= 0)
                    {
                        string result = "проиграл";
                        Logger<string> msgResult = LogMessages.EndMessage;
                        LogMessages.PrintMessage(msgResult(result));
                        SpaceShip.GameEndEvent += Fail;
                        _spaceShip.End();
                    }

                    continue;
                }

                for (int j = 0; j < _bullets.Count; j++)
                {
                    if (_asteroids.Count != 0 && _bullets[j].Collision(_asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        _bullets.RemoveAt(j);
                        if(j != 0) j--;
                        _asteroids.RemoveAt(i);
                        if(i != 0) i--;
                        points += 10;
                        Logger<int> msg = LogMessages.AsteroidMessage;
                        LogMessages.PrintMessage(msg(_asteroids.Count));

                        if (_asteroids.Count == 0)
                        {
                            string result = "выиграл";
                            Logger<string> msgResult = LogMessages.EndMessage;
                            LogMessages.PrintMessage(msgResult(result));
                            SpaceShip.GameEndEvent += Win;
                            _spaceShip.End();
                        }

                        continue;
                    }
                }
            }
        }

        private static void Fail(object sender, EventArgs e)
        {
            timer.Stop();
            timerForPlanets.Stop();
            Buffer.Graphics.DrawString($"Game Over", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            LogMessages.GameEnd();
            _buffer.Render();
        }

        private static void Win(object sender, EventArgs e)
        {
            round++;
            asteroidsLeft += round - 1;
            timer.Stop();
            timerForPlanets.Stop();
            reload.Stop();
            Buffer.Graphics.DrawString($"You Win", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Graphics.DrawString($"Prepare for round {round}", new Font(FontFamily.GenericSansSerif, 20, FontStyle.Underline), Brushes.White, 240, 180);
            _buffer.Render();

            nextRnd.Start();
            nextRnd.Tick += NextRnd_Tick;
        }

        private static void NextRnd_Tick(object sender, EventArgs e)
        {
            SpaceShip.GameEndEvent -= Win;
            nextRnd.Stop();
            _asteroids = new List<ActiveObject>();
            _bullets = new List<ActiveObject>();
            load();
            timer.Start();
            timerForPlanets.Start();
            reload.Start();
        }

        #endregion
    }
}
