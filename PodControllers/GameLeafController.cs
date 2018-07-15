using System;
using System.Collections;
using System.Xml;

using EmuController.PodItems;
using EmuController.Controls;
using EmuController.Utility;

namespace EmuController.PodControllers
{
    public class GameLeafController : PodController
    {
        private GameSystem _system;
        private ArrayList _master;
        private string _field;
        private bool _findClones;

        public GameLeafController(GameSystem system, ArrayList master, string field, bool findClones)
        {
            _system = system;
            _master = master;
            _field = field;
            _findClones = findClones;
        }

        public GameLeafController(GameSystem system, ArrayList master, string field)
            : this(system, master, field, true)
        {
        }

        public override void Refresh()
        {
            Items.Clear();

            foreach (XmlNode game in _master)
            {
                string val = XmlBinder.Eval(game, _field);

                if (val.Length == 0)
                    val = "(unnamed)";

                if (_findClones)
                {
                    XmlNodeList clones = game.SelectNodes("clones/game");
                    if (clones.Count > 0)
                    {
                        ArrayList group = new ArrayList();

                        group.Add(game);
                        foreach (XmlNode clone in clones)
                            group.Add(clone);

                        PodController c = new GameLeafController(_system, group, "description", false);
                        Items.Add(new GameItem(_system, game, val, c));

                        continue;
                    }
                }

                Items.Add(new GameItem(_system, game, val));
            }

            Items.Sort(new PodItemComparer());
        }
    }
}
