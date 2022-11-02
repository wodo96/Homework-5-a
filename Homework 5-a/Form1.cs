namespace Homework_5_a
{
    public partial class Form1 : Form
    {
        Bitmap b;
        Graphics g;
        Rectangle rect;

        Dictionary<double, long> d = new Dictionary<double, long>();
        bool v= false;


        Pen penRectangle = new Pen(Color.Black, 0.2f);

        public Form1()
        {
            InitializeComponent();
            d.Add(0, 13);
            d.Add(1, 45);
            d.Add(2, 18);
            d.Add(7, 34);
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            rect = new Rectangle(20, 20, b.Width - 40, b.Height - 40);
            generateHistogram(rect, d, v);
            pictureBox1.Image = b;

        }

    

       
        private void generateHistogram(Rectangle r, Dictionary<double, long> d, bool v = false)
        {
            int n = d.Count;

            double maxValue = d.Values.Max();
            for (int i = 0; i < n; i++)
            {
                Rectangle rr;
                int left, top, right, bottom;
                if (v)
                {
                    left = fromXRealToXVirtual(i, 0, n, r.Left, r.Width);
                    top = fromYRealToYVirtual(d.ElementAt(i).Value, 0, maxValue, r.Top, r.Height);
                    right = fromXRealToXVirtual(i + 1, 0, n, r.Left, r.Width);
                    bottom = fromYRealToYVirtual(0, 0, maxValue, r.Top, r.Height);
                }
                else
                {
                    left = fromXRealToXVirtual(0, 0, maxValue, r.Left, r.Width);
                    top = fromYRealToYVirtual(i + 1, 0, n, r.Top, r.Height);
                    right = fromXRealToXVirtual(d.ElementAt(i).Value, 0, maxValue, r.Left, r.Width);
                    bottom = fromYRealToYVirtual(i, 0, n, r.Top, r.Height);
                }
                rr = Rectangle.FromLTRB(left, top, right, bottom);

                
                g.FillRectangle(new SolidBrush(Color.LightBlue), rr);
                g.DrawRectangle(penRectangle, rr);

            }
        }

        private void redraw()
        {

            g.Clear(BackColor);
            generateHistogram(rect, d, v);
            g.DrawRectangle(penRectangle, rect);

            pictureBox1.Image = b;
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            v = checkBox1.Checked;
            redraw();

        }

        private int fromXRealToXVirtual(double x, double minX, double maxX, int left, int w)
        {
            return left + (int)(w * (x - minX) / (maxX - minX));
        }

        private int fromYRealToYVirtual(double y, double minY, double maxY, int top, int h)
        {
            return top + (int)(h - h * (y - minY) / (maxY - minY));
        }

      
    }
}