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
    public static class Game
    {
        public static Dictionary<Asteroid, System.Drawing.Bitmap> asteroidsAndSkins= new Dictionary<Asteroid, System.Drawing.Bitmap>();
        public static List<System.Drawing.Bitmap> skins = new List<System.Drawing.Bitmap> { Resources.Asteroid1, Resources.Asteroid2, Resources.Asteroid3, Resources.Asteroid4, Resources.Asteroid5 };
        public static Dictionary<Planet, System.Drawing.Bitmap> planetsAndSkins = new Dictionary<Planet, System.Drawing.Bitmap>();
        public static List<System.Drawing.Bitmap> skinsForPlanets = new List<System.Drawing.Bitmap> { Resources.Planet1, Resources.Planet2, Resources.Planet3 };

        #region Private Fields

        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;
        private static Asteroid[] _asteroids;
        private static Planet[] _planets;
        private static Star[] _stars;
        private static Comet[] _comets;

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

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            _buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            load();

            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;

            Timer timerForPlanets = new Timer();
            timerForPlanets.Interval = 500;
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
                planet.Draw(planetsAndSkins[planet]);

            foreach (var asteroid in _asteroids)
                asteroid.Draw(asteroidsAndSkins[asteroid]);

            _buffer.Render();
        }

        public static void Update()
        {
            foreach (var asteroid in _asteroids)
                asteroid.Update();
        }

        public static void load()
        {
            var rnd = new Random();

            _stars = new Star[300];
            for (int i = 0; i < _stars.Length; i++)
            {
                var size = 1;
                _stars[i] = new Star(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-1, 0), new Size(size, size));
            }

            _comets = new Comet[5];
            for (int i = 0; i < _comets.Length; i++)
            {
                var size = rnd.Next(10, 40);
                _comets[i] = new Comet(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-2, 3), new Size(size, size));
            }

            _planets = new Planet[3];
            for (int i = 0; i < _planets.Length; i++)
            {
                var size = rnd.Next(50, 300);
                _planets[i] = new Planet(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-1, 0), new Size(size, size));
                planetsAndSkins[_planets[i]] = skinsForPlanets[i];
            }

            _asteroids = new Asteroid[15];
            for(int i = 0; i < _asteroids.Length; i++)
            {
                var size = rnd.Next(15, 50);
                _asteroids[i] = new Asteroid(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(rnd.Next(-5, 5), rnd.Next(-5, 5)), new Size(size, size));
                asteroidsAndSkins[_asteroids[i]] = skins[rnd.Next(0, 5)];
            }
        }

            #endregion
    }
}
