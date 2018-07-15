using System;
using System.Collections;
using System.Xml;

using EmuController.PodItems;
using EmuController.Controls;
using EmuController.Utility;

namespace EmuController.PodControllers
{
    public class FilterController : PodController
    {
        private GameSystem _system;
        private ArrayList _master;
        private string _field;

        public FilterController(GameSystem system, ArrayList master, string field)
        {
            _system = system;
            _master = master;
            _field = field;
        }

        public override void Refresh()
        {
            Hashtable hash = new Hashtable();

            foreach (XmlNode game in _master)
            {
                string val = XmlBinder.Eval(game, _field);

                if (val.Length == 0)
                    val = "(n/a)";

                ArrayList group = (ArrayList)hash[val];

                if (group == null)
                {
                    group = new ArrayList();
                    hash[val] = group;
                }

                group.Add(game);
            }

            Items.Clear();

            foreach (string key in hash.Keys)
            {
                ArrayList group = (ArrayList)hash[key];

                if (group.Count > 20)
                {
                    PodController c = new AlphabetController(_system, group, "description");
                    Items.Add(new SimpleItem(key, null, c));
                }
                else
                {
                    PodController c = new GameLeafController(_system, group, "description");
                    Items.Add(new SimpleItem(key, null, c));
                }
            }

            Items.Sort(new PodItemComparer());
        }
    }
}
