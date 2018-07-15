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
    public class MameController : PodController
    {
        private XmlDocument _doc = new XmlDocument();
        private ArrayList _games = new ArrayList();

        public MameController()
        {
            _doc.Load(Config.MameDirectory + "games.xml");

            PodController c;

            c = new AlphabetController(GameSystem.Mame, _games, "description");
            Items.Add(new SimpleItem("All Games", null, c));

            c = new FilterController(GameSystem.Mame, _games, "year");
            Items.Add(new SimpleItem("By Year", null, c));

            c = new FilterController(GameSystem.Mame, _games, "manufacturer");
            Items.Add(new SimpleItem("By Manufacturer", null, c));

            c = new FilterController(GameSystem.Mame, _games, "input/@players");
            Items.Add(new SimpleItem("By Players", null, c));

            Items.Add(new RandomLeafItem(_games));
        }

        public void Reload()
        {
            _games.Clear();

            XmlNodeList games = _doc.SelectNodes("/mame/game[not(@runnable='no' or @cloneof)]");
            //XmlNodeList games = _doc.SelectNodes("/mame/game[not(@runnable='no')]");

            foreach (XmlNode game in games)
            {
                string name = XmlBinder.Eval(game, "@name");

                if (File.Exists(Config.MameDirectory + "roms\\" + name + ".zip"))
                {
                    _games.Add(game);
                }
            }
        }

        public static void RunGame(XmlNode game)
        {
            MainForm.ShowWaiting("Launching Game...");

            string name = XmlBinder.Eval(game, "@name");

            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = Config.MameDirectory;
            proc.StartInfo.FileName = Config.MameDirectory + "mame.exe";
            proc.StartInfo.Arguments = name;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            //proc.StartInfo.RedirectStandardError = true;

            proc.Start();

            string output = proc.StandardOutput.ReadToEnd();

            proc.WaitForExit();

            //string error = proc.StandardError.ReadToEnd();
            //Console.WriteLine(error);

            //return output;

            MainForm.HideWaiting();
        }
    }
}
