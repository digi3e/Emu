using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Xml;

using EmuController.PodItems;
using EmuController.Controls;
using EmuController.Utility;

namespace EmuController.PodControllers
{
    public class GameboyController : PodController
    {
        private XmlDocument _doc = new XmlDocument();
        private ArrayList _games = new ArrayList();

        public GameboyController()
        {
            _doc.Load(Config.GameboyDirectory + "games.xml");

            PodController c;

            c = new AlphabetController(GameSystem.Gameboy, _games, "description");
            Items.Add(new SimpleItem("All Games", null, c));

            Items.Add(new RandomLeafItem(_games));
        }

        public void Reload()
        {
            _games.Clear();

            XmlNodeList games = _doc.SelectNodes("/gameboy/game");

            foreach (XmlNode game in games)
            {
                string name = XmlBinder.Eval(game, "@name");

                if (File.Exists(Config.GameboyDirectory + "roms\\" + name + ".zip"))
                {
                    _games.Add(game);
                }
            }
        }

        public static void RunGame(XmlNode game)
        {
            MainForm.ShowWaiting("Launching Game...");

            string name = XmlBinder.Eval(game, "@name");
            string args = "\"roms\\" + name + ".zip\"";

            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = Config.GameboyDirectory;
            proc.StartInfo.FileName = Config.GameboyDirectory + "VisualBoyAdvance.exe";
            proc.StartInfo.Arguments = args;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;

            proc.Start();

            string output = proc.StandardOutput.ReadToEnd();

            proc.WaitForExit();

            MainForm.HideWaiting();
        }
    }
}
