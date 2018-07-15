using System;
using System.Collections;

namespace EmuController.Controls
{
    public class PodItem
    {
        private string _name;

        public PodItem(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public virtual bool IsLeaf
        {
            get { return true; }
        }

        public virtual PodController GetChildController()
        {
            return null;
        }

        public virtual void RunAction()
        {
            return;
        }
    }

    public class PodItemComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            PodItem a = (PodItem)x;
            PodItem b = (PodItem)y;

            return a.Name.CompareTo(b.Name);
        }
    }
}
