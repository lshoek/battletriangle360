using System.Drawing;
using System.Drawing.Drawing2D;
using System;
using System.Collections.Generic;

namespace MultiClientGame
{
    public class Ship
    {
        public string ColorStr;
        public Color playerColor = Color.HotPink; 
        public double Angle = 0;
        public double currentSpeed = 0;
        public double maxSpeed = 4;
        public double speedVal = 0.14;
        public int bulletsize = 6;
        public bool isActive = true;
        public List<Tuple<string, Color>> colorList = new List<Tuple<string, Color>>();
        Point center;
        int radius;
        GameForm gameform;
        Rectangle rec;

        public Ship(string color, Point center, int radius, GameForm g)
        {
            this.radius = radius;
            this.center = center;
            this.ColorStr = color;
            fillColorList();
            setPlayerColor(ColorStr);
            Angle = new Random().NextDouble() * 360;
            gameform = g;
        }

        public void draw(Graphics g)
        {
            GraphicsState state = g.Save();
            SolidBrush b = new SolidBrush(playerColor);       
            Point[] points = { new Point(center.X, center.Y + radius), new Point(center.X - 10, center.Y + 20 + radius), new Point(center.X + 10, center.Y + 20 + radius) };
            Pen pen = new Pen(new SolidBrush(Color.Blue));
            Matrix myMatrix = new Matrix();
            myMatrix.RotateAt((float)Angle, center, MatrixOrder.Append);
            g.Transform = myMatrix;
            g.FillPolygon(b, points, FillMode.Alternate);
            
            g.Restore(state);
            //g.DrawRectangle(pen, rec);
        }

        public Point getLocation()
        {
            int x = (int)(center.X+(radius) * Math.Cos((Angle + 90) / 180 * Math.PI));
            int y = (int)(center.Y+(radius) * Math.Sin((Angle + 90) / 180 * Math.PI));
            return new Point(x,y);
        }

        public void checkhit()
        {
            rec = new Rectangle();
            rec.Location = getLocation();
            rec.Width = 1;
            rec.Height = 1;
            
            Rectangle r = new Rectangle();
            r.Width = bulletsize + 5;
            r.Height = bulletsize + 5;
            foreach (Bullet b in gameform.bullets.getList())
            {
                r.Location = b.getloction();
                if (r.Contains(rec) && playerColor != b.color)
                {
                    gameform.writer.WriteLine("hit#" + b.color.ToKnownColor() + "#" + ColorStr);
                }
            }
        }

        public void Move(char dir)
        {
            if (dir == 'R')
                currentSpeed -= speedVal;
            else //dir == 'L'
                currentSpeed += speedVal;

            if (currentSpeed >= maxSpeed)
                currentSpeed = maxSpeed;
            else if (currentSpeed <= maxSpeed*-1)
                currentSpeed = maxSpeed*-1;

            Angle += currentSpeed;
        }

        public void speedToZero()
        {
            if (currentSpeed > speedVal && currentSpeed < speedVal*-1)
                currentSpeed = 0;

            if (currentSpeed != 0)
            {
                if (currentSpeed > 0)
                    currentSpeed -= speedVal;
                else
                    currentSpeed += speedVal;
            }
            Angle += currentSpeed;
        }

        public void Shoot()
        {
            int x = (int)((radius-bulletsize) * Math.Cos((Angle+90)/180*Math.PI));
            int y = (int)((radius-bulletsize) * Math.Sin((Angle+90)/180*Math.PI));
            
            Point p = new Point(center.X+ x , center.Y+y);
            
            Bullet b = new Bullet(playerColor,p.X,p.Y,center);
            gameform.bullets.Add(b);
            gameform.writer.WriteLine("shoot#" + ColorStr + "#" + p.X + "#" + p.Y);
        }

        public void setPlayerColor(string cStr)
        {
            foreach (Tuple<string, Color> t in colorList)
            {
                if (cStr == t.Item1)
                {
                    playerColor = t.Item2;
                    break;
                }
            }
        }

        public Color getPlayerColor(string cStr)
        {
            foreach (Tuple<string, Color> t in colorList)
            {
                if (cStr == t.Item1)
                {
                    return t.Item2;
                }
            }
            return Color.Black;
        }

        public void setActivity(bool b)
        {
            isActive = b;
        }

        public double getAngle()
        {
            return Angle;
        }

        public void fillColorList()
        {
            colorList.Add(new Tuple<string, Color>("Red", Color.Red));
            colorList.Add(new Tuple<string, Color>("Blue", Color.Blue));
            colorList.Add(new Tuple<string, Color>("Green", Color.Green));
            colorList.Add(new Tuple<string, Color>("Yellow", Color.Yellow));
            colorList.Add(new Tuple<string, Color>("Purple", Color.Purple));
            colorList.Add(new Tuple<string, Color>("Orange", Color.Orange));
            colorList.Add(new Tuple<string, Color>("Grey", Color.Gray));
            colorList.Add(new Tuple<string, Color>("Black", Color.Black));
        }
    }
}
