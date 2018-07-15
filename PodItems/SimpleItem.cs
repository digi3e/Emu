using System;
using System.Xml;

using EmuController.Controls;
using EmuController.PodControllers;

namespace EmuController.PodItems
{
    public class SimpleItem : PodItem
    {
        private string _icon;
        private PodController _controller;

        public SimpleItem(string name, string icon, PodController controller)
            : base(name)
        {
            _icon = icon;
            _controller = controller;
        }

        public string Icon
        {
            get { return _icon; }
        }

        public override bool IsLeaf
        {
            get { return false; }
        }

        public override PodController GetChildController()
        {
            return _controller;
        }
    }
}
