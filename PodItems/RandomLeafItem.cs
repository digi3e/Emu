using System;
using System.Collections;
using System.Xml;

using EmuController.Controls;
using EmuController.PodControllers;

namespace EmuController.PodItems
{
    public class RandomLeafItem : PodItem
    {
        private ArrayList _group;

        public RandomLeafItem(ArrayList master)
            : base("Random Game")
        {
            _group = master;
        }

        public override bool IsLeaf
        {
            get { return true; }
        }

        public override void RunAction()
        {
            if (_group.Count == 0)
                return;

            Random rand = new Random();
            int i = rand.Next(_group.Count);
            XmlNode game = (XmlNode)_group[i];

            GameItem.RunGame(game);
        }
    }
}
