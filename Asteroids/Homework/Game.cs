using Homework.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{
    public class GameObjectException : Exception
    {
        public GameObjectException(string message) : base (message)
        {

        }
    }

    public static class Game
    {
        public static List<Bitmap> skins = new List<Bitmap> { Resources.Asteroid1, Resources.Asteroid2, Resources.Asteroid3, Resources.Asteroid4, Resources.Asteroid5 };
        public static List<Bitmap> skinsForPlanets = new List<Bitmap> { Resources.Planet1, Resources.Planet2, Resources.Planet3 };
        public static Random rnd = new Random();

        #region Private Fields

        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;
        private static ActiveObject[] _asteroids;
        private static ActiveObject _bullet;
        private static BackgroundObject[] _planets;
        private static BackgroundObject[] _stars;
        private static BackgroundObject[] _comets;

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
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }

            _buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            load();

            Timer timer = new Timer();
            timer.Interval = 5;
            timer.Start();
            timer.Tick += Timer_Tick;

            Timer timerForPlanets = new Timer();
            timerForPlanets.Interval = 50;
            timerForPlanets.Start();
            timerForPlanets.Tick += TimerForPlanets_Tick;
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

            _bullet.Draw();

            _buffer.Render();
        }

        public static void Update()
        {
            foreach (var asteroid in _asteroids)
            {
                asteroid.Update();
                if (asteroid.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _bullet.Pos = new Point(0, rnd.Next(50, 600));
                    asteroid.Pos = new Point(Width, rnd.Next(0, Height));
                }
            }

            _bullet.Update();
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

                _asteroids = new ActiveObject[15];
                for (int i = 0; i < _asteroids.Length; i++)
                {
                    var size = rnd.Next(15, 50);
                    _asteroids[i] = new Asteroid(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(rnd.Next(-5, 5), rnd.Next(-5, 5)), new Size(size, size), skins[rnd.Next(0, 5)]);
                }

                _bullet = new Bullet(new Point(0, rnd.Next(50, 600)), new Point(5, 0), new Size(50, 10));

            }
            catch (GameObjectException ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

            #endregion
    }
}
