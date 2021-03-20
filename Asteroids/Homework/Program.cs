using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var game = new Form();
            game.MinimumSize = new System.Drawing.Size(800, 600);
            game.MaximumSize = new System.Drawing.Size(800, 600);
            game.MaximizeBox = false;
            game.MinimizeBox = false;
            game.StartPosition = FormStartPosition.CenterScreen;
            game.Text = "Asteroids";

            Game.Init(game);
            game.Show();
            Game.Draw();
            Application.Run(game);
        }
    }
}
