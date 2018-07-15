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
    public class Atari2600Controller : PodController
    {
        private XmlDocument _doc = new XmlDocument();
        private ArrayList _games = new ArrayList();

        public Atari2600Controller()
        {
            _doc.Load(Config.Atari2600Directory + "games.xml");

            PodController c;

            c = new AlphabetController(GameSystem.Atari2600, _games, "description");
            Items.Add(new SimpleItem("All Games", null, c));

            Items.Add(new RandomLeafItem(_games));
        }

        public void Reload()
        {
            _games.Clear();

            XmlNodeList games = _doc.SelectNodes("/atari2600/game");

            foreach (XmlNode game in games)
            {
                string name = XmlBinder.Eval(game, "@name");

                if (File.Exists(Config.Atari2600Directory + "roms\\" + name + ".bin"))
                {
                    _games.Add(game);
                }
            }
        }

        public static void RunGame(XmlNode game)
        {
            MainForm.ShowWaiting("Launching Game...");

            string name = XmlBinder.Eval(game, "@name");
            string args = "\"roms\\" + name + ".bin\"";

            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = Config.Atari2600Directory;
            proc.StartInfo.FileName = Config.Atari2600Directory + "stella.exe";
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
