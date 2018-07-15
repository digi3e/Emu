using System;
using System.Collections;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace EmuController.Controls
{
    public class PodList : UserControl
    {
        private ArrayList _stack = new ArrayList();
        private Font _font;
        private KeyEventArgs _lastKey = null;
        private System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        public PodList()
        {
            int fontSize = (Screen.PrimaryScreen.WorkingArea.Width >= 1920) ? 24 : 12;
            _font = new Font("Arial Black", fontSize);

            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.Opaque, true);

            _timer.Tick += new EventHandler(this.OnKeyTick);
        }

        public event EventHandler SelectedIndexChanged;

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }

        public object SelectedItem
        {
            get
            {
                StackWrapper current = CurrentController;

                if (current == null)
                    return null;

                if (current.Controller.Items.Count == 0)
                    return null;

                return current.Controller.Items[current.Index];
            }
        }

        private StackWrapper CurrentController
        {
            get
            {
                if (_stack.Count == 0)
                    return null;

                return (StackWrapper)_stack[_stack.Count - 1];
            }
        }

        public PodController RootController
        {
            get
            {
                if (_stack.Count == 0)
                    return null;

                StackWrapper current = (StackWrapper)_stack[0];

                return current.Controller;
            }
            set
            {
                _stack.Clear();

                if (value != null)
                    _stack.Add(new StackWrapper(value));

                Refresh();
            }
        }

        private int Rows
        {
            get
            {
                return (base.Height / this._font.Height);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Bitmap bmp = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics bmpG = Graphics.FromImage(bmp);

            GoPaint(bmpG, e.ClipRectangle);

            g.DrawImage(bmp, 0, 0);

            bmpG.Dispose();
            bmp.Dispose();

            g.Dispose();
        }

        private void GoPaint(Graphics g, Rectangle clipRect)
        {
            g.Clear(this.BackColor);

            Pen pen = new Pen(Color.White);

            StackWrapper current = CurrentController;

            if (current == null)
            {
                g.DrawString("No controller set", _font, pen.Brush, 5, 5);
            }
            else
            {
                int rows = this.Height / _font.Height;

                for (int i = 0; i < rows; i++)
                {
                    Pen rowPen = pen;
                    int index = i + current.Scroll;

                    if (index >= current.Controller.Items.Count)
                        break;

                    PodItem item = (PodItem)current.Controller.Items[index];
                    int y = i * _font.Height;

                    // Selected item background
                    if (index == current.Index)
                    {
                        Rectangle bgRect = new Rectangle(0, y, this.Width, _font.Height);
                        g.FillRectangle(pen.Brush, bgRect);

                        rowPen = new Pen(Color.Black);
                    }

                    // Text
                    Rectangle textRect = new Rectangle(5, y, this.Width - 25, _font.Height);
                    g.DrawString(item.Name, _font, rowPen.Brush, textRect);

                    // Arrow
                    if (!item.IsLeaf)
                    {
                        Point[] points = new Point[3];

                        int xoffset = this.Width - 15;
                        int yoffset = (_font.Height / 2) - 5;

                        points[0] = new Point(xoffset, y + yoffset);
                        points[1] = new Point(xoffset, y + yoffset + 10);
                        points[2] = new Point(xoffset + 10, y + yoffset + 5);

                        g.FillPolygon(rowPen.Brush, points);
                    }

                    if (rowPen != pen)
                        rowPen.Dispose();
                }
            }

            pen.Dispose();
        }

        private void AdjustScroll()
        {
            StackWrapper current = CurrentController;

            int rows = this.Height / _font.Height;
            int max = current.Controller.Items.Count - rows;

            if (max <= 0)
            {
                // List is too small for scrolling
                current.Scroll = 0;
                return;
            }

            if (current.Index < current.Scroll)
            {
                current.Scroll = current.Index;
            }
            else if (current.Index >= current.Scroll + rows)
            {
                current.Scroll = current.Index - rows + 1;
            }
        }

        private void MoveUp(int count)
        {
            StackWrapper current = CurrentController;
            if (current.Index > 0)
            {
                current.Index -= count;
                if (current.Index < 0)
                    current.Index = 0;

                AdjustScroll();
                Refresh();

                OnSelectedIndexChanged(EventArgs.Empty);
            }
        }

        private void MoveDown(int count)
        {
            // Decide if we can move
            StackWrapper current = CurrentController;
            int max = current.Controller.Items.Count - 1;

            if (current.Index < max)
            {
                current.Index += count;
                if (current.Index > max)
                    current.Index = max;

                AdjustScroll();
                Refresh();

                OnSelectedIndexChanged(EventArgs.Empty);
            }
        }

        private void MoveLeft()
        {
            // Decide if we can move
            StackWrapper current = CurrentController;

            if (_stack.Count <= 1)
                return;

            // Create graphics port
            Graphics g = this.CreateGraphics();

            // Get image of the current view
            Bitmap bmp1 = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics bmp1G = Graphics.FromImage(bmp1);

            GoPaint(bmp1G, ClientRectangle);

            // Remove controller from stack
            _stack.RemoveAt(_stack.Count - 1);

            // Get image of new view
            Bitmap bmp2 = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics bmp2G = Graphics.FromImage(bmp2);

            GoPaint(bmp2G, ClientRectangle);

            // Animate transition between views
            Pen pen = new Pen(Color.Gray, 3);

            for (int i = 1; i < 4; i++)
            {
                int x = ClientRectangle.Width * i / 5;

                g.DrawImage(bmp1, x, 0);
                g.DrawImage(bmp2, x - ClientRectangle.Width, 0);
                g.DrawLine(pen, x, 0, x, ClientRectangle.Height);

                Thread.Sleep(25);
            }

            pen.Dispose();

            // Clean up images
            bmp1G.Dispose();
            bmp1.Dispose();

            bmp2G.Dispose();
            bmp2.Dispose();

            g.Dispose();

            // Finally, draw new view
            Refresh();

            OnSelectedIndexChanged(EventArgs.Empty);
        }

        private void MoveRight()
        {
            // Decide if we can move
            StackWrapper current = CurrentController;

            if (current.Controller.Items.Count == 0)
                return;

            PodItem item = (PodItem)current.Controller.Items[current.Index];

            if (item.IsLeaf)
                return;

            PodController child = item.GetChildController();

            if (child == null)
                return;

            child.Refresh();

            // Create graphics port
            Graphics g = this.CreateGraphics();

            // Get image of the current view
            Bitmap bmp1 = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics bmp1G = Graphics.FromImage(bmp1);

            GoPaint(bmp1G, ClientRectangle);

            // Add new controller to stack
            _stack.Add(new StackWrapper(child));

            // Get image of new view
            Bitmap bmp2 = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics bmp2G = Graphics.FromImage(bmp2);

            GoPaint(bmp2G, ClientRectangle);

            // Animate transition between views
            Pen pen = new Pen(Color.Gray, 3);

            for (int i = 1; i < 4; i++)
            {
                int x = ClientRectangle.Width * (5 - i) / 5;

                g.DrawImage(bmp1, x - ClientRectangle.Width, 0);
                g.DrawImage(bmp2, x, 0);
                g.DrawLine(pen, x, 0, x, ClientRectangle.Height);

                Thread.Sleep(25);
            }

            pen.Dispose();

            // Clean up images
            bmp1G.Dispose();
            bmp1.Dispose();

            bmp2G.Dispose();
            bmp2.Dispose();

            g.Dispose();

            // Finally, draw new view
            Refresh();

            OnSelectedIndexChanged(EventArgs.Empty);
        }

        private void RunAction()
        {
            StackWrapper current = CurrentController;

            if (current.Controller.Items.Count == 0)
                return;

            PodItem item = (PodItem)current.Controller.Items[current.Index];

            //if (!item.IsLeaf)
            //    return;

            item.RunAction();
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            _lastKey = e;
            _timer.Interval = 512;

            e.Handled = true;
            OnKeyTick(this, EventArgs.Empty);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _timer.Stop();

            e.Handled = true;
        }

        protected void OnKeyTick(object sender, EventArgs e)
        {
            bool repeat = false;

            switch (this._lastKey.KeyCode)
            {
                case Keys.M:
                case Keys.Prior:
                    repeat = true;
                    MoveUp(this.Rows - 1);
                    break;

                case Keys.Oemcomma:
                case Keys.Next:
                    repeat = true;
                    MoveDown(this.Rows - 1);
                    break;

                case Keys.Home:
                case Keys.Return:
                    RunAction();
                    break;

                case Keys.Left:
                    MoveLeft();
                    break;

                case Keys.Up:
                    repeat = true;
                    MoveUp(1);
                    break;

                case Keys.Right:
                    MoveRight();
                    break;

                case Keys.Down:
                    repeat = true;
                    MoveDown(1);
                    break;
            }

            if (repeat)
            {
                if (_timer.Interval > 1)
                {
                    _timer.Interval /= 2;
                }
                _timer.Start();
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
        }

        protected override void OnLostFocus(EventArgs e)
        {
        }

        private class StackWrapper
        {
            private PodController _controller;
            private int _index = 0;
            private int _scroll = 0;

            public StackWrapper(PodController controller)
            {
                _controller = controller;
            }

            public PodController Controller
            {
                get { return _controller; }
            }

            public int Index
            {
                get { return _index; }
                set { _index = value; }
            }

            public int Scroll
            {
                get { return _scroll; }
                set { _scroll = value; }
            }
        }
    }
}
