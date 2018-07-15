using System;
using System.Collections;
using System.Xml;

using EmuController.PodItems;
using EmuController.Controls;
using EmuController.Utility;

namespace EmuController.PodControllers
{
    public class AlphabetController : PodController
    {
        private GameSystem _system;
        private ArrayList _master;
        private string _field;

        public AlphabetController(GameSystem system, ArrayList master, string field)
        {
            _system = system;
            _master = master;
            _field = field;
        }

        public override void Refresh()
        {
            ArrayList[] groups = new ArrayList[27];

            for (int i = 0; i < groups.Length; i++)
                groups[i] = new ArrayList();

            foreach (XmlNode game in _master)
            {
                string val = XmlBinder.Eval(game, _field);

                if (val.Length == 0)
                    val = "(unnamed)";

                int i = 0;
                char first = val.ToUpper()[0];

                if (first >= 'a' && first <= 'z')
                    i = first - 'a' + 1;
                else if (first >= 'A' && first <= 'Z')
                    i = first - 'A' + 1;

                groups[i].Add(game);
            }

            Items.Clear();

            for (int i = 0; i < groups.Length; i++)
            {
                ArrayList group = groups[i];

                if (group.Count == 0)
                    continue;

                string name;

                if (i == 0)
                    name = "#";
                else
                    name = ((char)(i - 1 + 'A')).ToString();

                PodController c = new GameLeafController(_system, group, "description");
                Items.Add(new SimpleItem(name, null, c));
            }
        }
    }
}
