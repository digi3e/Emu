using System;
using System.Collections;

namespace EmuController.Controls
{
    public abstract class PodController
    {
        private ArrayList _items = new ArrayList();

        public PodController()
        {
        }

        public ArrayList Items
        {
            get { return _items; }
        }

        public virtual void Refresh()
        {
        }
    }
}
