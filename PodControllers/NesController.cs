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
    public class NesController : PodController
    {
        private XmlDocument _doc = new XmlDocument();
        private ArrayList _games = new ArrayList();

        public NesController()
        {
            _doc.Load(Config.NesDirectory + "games.xml");

            PodController c;

            c = new AlphabetController(GameSystem.Nes, _games, "description");
            Items.Add(new SimpleItem("All Games", null, c));

            Items.Add(new RandomLeafItem(_games));
        }

        public void Reload()
        {
            _games.Clear();

            XmlNodeList games = _doc.SelectNodes("/nes/game");

            foreach (XmlNode game in games)
            {
                string name = XmlBinder.Eval(game, "@name");

                if (File.Exists(Config.NesDirectory + "roms\\" + name + ".nes"))
                {
                    _games.Add(game);
                }
            }
        }

        public static void RunGame(XmlNode game)
        {
            MainForm.ShowWaiting("Launching Game...");

            string name = XmlBinder.Eval(game, "@name");
            string args = "\"roms\\" + name + ".nes\"";

            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = Config.NesDirectory;
            proc.StartInfo.FileName = Config.NesDirectory + "nessie.exe";
            proc.StartInfo.Arguments = args;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            //proc.StartInfo.UseShellExecute = false;
            //proc.StartInfo.RedirectStandardOutput = true;

            proc.Start();

            //string output = proc.StandardOutput.ReadToEnd();

            proc.WaitForExit();

            MainForm.HideWaiting();
        }
    }
}
