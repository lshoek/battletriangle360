using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace MultiClientGame
{

    class Bullet
    {
        double X, Y;
        int i = 1;
        double stapx, stapy;
        internal Color color;
        int bulletsize = 12;

        public Bullet(Color c,double x, double y,Point p)
        {
            X = x;
            Y = y;
            color = c;
            stapx = (p.X-x  )/20;
            stapy = (p.Y-y  )/20;
        }

        public void update()
        {
            i++;
        }

        public void draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), (float)(X + stapx * i) - bulletsize / 2, (float)(Y + stapy * i) - bulletsize / 2, bulletsize, bulletsize);
        }

        public Point getloction()
        {
            return new Point(Convert.ToInt32(X + stapx * i),Convert.ToInt32( Y + stapy * i));
        }
    }
}
