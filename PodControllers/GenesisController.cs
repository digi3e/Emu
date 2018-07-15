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
    public class GenesisController : PodController
    {
        private XmlDocument _doc = new XmlDocument();
        private ArrayList _games = new ArrayList();

        public GenesisController()
        {
            _doc.Load(Config.GenesisDirectory + "games.xml");

            PodController c;

            c = new AlphabetController(GameSystem.Genesis, _games, "description");
            Items.Add(new SimpleItem("All Games", null, c));

            Items.Add(new RandomLeafItem(_games));
        }

        public void Reload()
        {
            _games.Clear();

            XmlNodeList games = _doc.SelectNodes("/genesis/game");

            foreach (XmlNode game in games)
            {
                string name = XmlBinder.Eval(game, "@name");

                if (File.Exists(Config.GenesisDirectory + "roms\\" + name + ".zip"))
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
            proc.StartInfo.WorkingDirectory = Config.GenesisDirectory;
            proc.StartInfo.FileName = Config.GenesisDirectory + "gens.exe";
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
