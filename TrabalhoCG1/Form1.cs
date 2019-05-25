using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoCG1
{
    public partial class Form1 : Form
    {
        Point inicial;
        Point final;
        Bitmap bitmap;
        bool drag;
        int segment;
        int segment_rectangle;
        int segment_triangle;
        int segment_circle;
        private int OffsetX, OffsetY;
        bool MovingStartEndPoint;
        bool MovingStartEndPointTriangle;
        private List<Linha> _lines = new List<Linha>();
        private List<Retangulo> _retangulos = new List<Retangulo>();
        private List<Triangulo> _triangulos = new List<Triangulo>();
        private List<Circulo> _circulos = new List<Circulo>();

        const int circle_radius = 3;
        const int over_dist_squared = circle_radius * circle_radius;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(Canvas.Width, Canvas.Height);
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, Canvas, new object[] { true });
            treeView1.Nodes[0].ExpandAll();
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            segment = 0;
            rotateLine(40);
            //double angle = 30 * (Math.PI / 180);
            //var a = _lines[0].Item1;
            //var b = _lines[0].Item2;

            //var midpoint = ((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            //var a_mid = ((a.X - midpoint.Item1), (a.Y - midpoint.Item2));
            //var b_mid = ((b.X - midpoint.Item1), (b.Y - midpoint.Item2));

            //var a_rotated = ((Math.Cos(angle) * a_mid.Item1 - Math.Sin(angle) * a_mid.Item2), (Math.Sin(angle) * a_mid.Item1 + Math.Cos(angle) * a_mid.Item2));
            //var b_rotated = ((Math.Cos(angle) * b_mid.Item1 - Math.Sin(angle) * b_mid.Item2), (Math.Sin(angle) * b_mid.Item1 + Math.Cos(angle) * b_mid.Item2));

            //_lines[0] = new Tuple<Point, Point>(new Point((int)a_rotated.Item1 + midpoint.Item1, (int)a_rotated.Item2 + midpoint.Item2), new Point((int)b_rotated.Item1 + midpoint.Item1, (int)b_rotated.Item2 + midpoint.Item2));
            //Canvas.Refresh();
        }
        private double Radio(Point inicial, Point final)
        {
            int Dx = Math.Abs(final.X - inicial.X);
            int Dy = Math.Abs(final.Y - inicial.Y);
            return Math.Sqrt(Math.Pow(Dx, 2) + Math.Pow(Dy, 2));
        }
        private void DrawCircleDDA(Point inicial, double r, Graphics g)
        {
            double x, y;
            x = y = default;
            for (x = 0; x <= y; x++)
            {
                y = Math.Sqrt(Math.Abs(Math.Pow(r, 2) - Math.Pow(x, 2)));
                DesenhaOctante((int)x, (int)y, inicial, g);
            }
        }
        private void DrawCircleDDA(Point inicial, double r, Graphics g, Color color)
        {
            double x, y;
            x = y = default;
            for (x = 0; x <= y; x++)
            {
                y = Math.Sqrt(Math.Abs(Math.Pow(r, 2) - Math.Pow(x, 2)));
                DesenhaOctante((int)x, (int)y, inicial, g, color);
            }
        }

        private void DrawCircleBreseham(Point inicial, double r, Graphics g)
        {
            double x, y;
            double d;

            x = 0;
            y = r;
            d = 3 - 2 - r;

            DesenhaOctante((int)x, (int)y, inicial, g);
            while (y >= x)
            {
                if (d < 0)
                {
                    //d += 2 * x + 5;
                    d += 4 * x + 6;

                }
                else
                {
                    // d += 2 * (x - y) + 5;
                    d += 4 * (x - y) + 10;

                    y--;
                }
                x++;
                DesenhaOctante((int)x, (int)y, inicial, g);
            }
        }
        private void DrawCircleBreseham(Point inicial, double r, Graphics g, Color color)
        {
            double x, y;
            double d;

            x = 0;
            y = r;
            d = 3 - 2 - r;

            DesenhaOctante((int)x, (int)y, inicial, g, color);
            while (y >= x)
            {
                if (d < 0)
                {
                    //d += 2 * x + 5;
                    d += 4 * x + 6;

                }
                else
                {
                    // d += 2 * (x - y) + 5;
                    d += 4 * (x - y) + 10;

                    y--;
                }
                x++;
                DesenhaOctante((int)x, (int)y, inicial, g, color);
            }
        }
        private void DesenhaOctante(int x, int y, Point inicial, Graphics g)
        {
            g.FillRectangle(new SolidBrush(colorDialog1.Color), new Rectangle(inicial.X + x, inicial.Y + y, 4, 4));
            g.FillRectangle(new SolidBrush(colorDialog1.Color), new Rectangle(inicial.X - y, inicial.Y - x, 4, 4));
            g.FillRectangle(new SolidBrush(colorDialog1.Color), new Rectangle(inicial.X + y, inicial.Y - x, 4, 4));
            g.FillRectangle(new SolidBrush(colorDialog1.Color), new Rectangle(inicial.X - y, inicial.Y + x, 4, 4));

            g.FillRectangle(new SolidBrush(colorDialog1.Color), new Rectangle(inicial.X + y, inicial.Y + x, 4, 4));
            g.FillRectangle(new SolidBrush(colorDialog1.Color), new Rectangle(inicial.X - x, inicial.Y - y, 4, 4));
            g.FillRectangle(new SolidBrush(colorDialog1.Color), new Rectangle(inicial.X + x, inicial.Y - y, 4, 4));
            g.FillRectangle(new SolidBrush(colorDialog1.Color), new Rectangle(inicial.X - x, inicial.Y + y, 4, 4));
        }
        private void DesenhaOctante(int x, int y, Point inicial, Graphics g, Color color)
        {
            g.FillRectangle(new SolidBrush(color), new Rectangle(inicial.X + x, inicial.Y + y, 4, 4));
            g.FillRectangle(new SolidBrush(color), new Rectangle(inicial.X - y, inicial.Y - x, 4, 4));
            g.FillRectangle(new SolidBrush(color), new Rectangle(inicial.X + y, inicial.Y - x, 4, 4));
            g.FillRectangle(new SolidBrush(color), new Rectangle(inicial.X - y, inicial.Y + x, 4, 4));

            g.FillRectangle(new SolidBrush(color), new Rectangle(inicial.X + y, inicial.Y + x, 4, 4));
            g.FillRectangle(new SolidBrush(color), new Rectangle(inicial.X - x, inicial.Y - y, 4, 4));
            g.FillRectangle(new SolidBrush(color), new Rectangle(inicial.X + x, inicial.Y - y, 4, 4));
            g.FillRectangle(new SolidBrush(color), new Rectangle(inicial.X - x, inicial.Y + y, 4, 4));
        }

        private float Sign(Point p1, Point p2, Point p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }
        private bool PointInTriangle(Point mouse, Point v1, Point v2, Point v3)
        {
            float d1, d2, d3;
            bool has_neg, has_pos;

            d1 = Sign(mouse, v1, v2);
            d2 = Sign(mouse, v2, v3);
            d3 = Sign(mouse, v3, v1);

            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }
        private bool PointInCircle(Point mouse, Point center, double r)
        {
            double d = Math.Sqrt(Math.Pow(center.X - mouse.X, 2) + Math.Pow(center.Y - mouse.Y, 2));
            if (d < r)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Point MidLine(Linha linha)
        {
            var a = linha.Ponto_Inicial;
            var b = linha.Ponto_Final;

            var midpoint = ((a.X + b.X) / 2, (a.Y + b.Y) / 2);

            return new Point(midpoint.Item1, midpoint.Item2);
        }
        private void rotateLine(double angle)
        {
            angle = angle * (Math.PI / 180);
            var a = _lines[segment].Ponto_Inicial;
            var b = _lines[segment].Ponto_Final;
            var algh = _lines[segment].Algorithm;
            var cor = _lines[segment].Color;

            var midpoint = ((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            var a_mid = ((a.X - midpoint.Item1), (a.Y - midpoint.Item2));
            var b_mid = ((b.X - midpoint.Item1), (b.Y - midpoint.Item2));

            var a_rotated = ((Math.Cos(angle) * a_mid.Item1 - Math.Sin(angle) * a_mid.Item2), (Math.Sin(angle) * a_mid.Item1 + Math.Cos(angle) * a_mid.Item2));
            var b_rotated = ((Math.Cos(angle) * b_mid.Item1 - Math.Sin(angle) * b_mid.Item2), (Math.Sin(angle) * b_mid.Item1 + Math.Cos(angle) * b_mid.Item2));

            _lines[segment] = new Linha(new Point((int)a_rotated.Item1 + midpoint.Item1, (int)a_rotated.Item2 + midpoint.Item2), new Point((int)b_rotated.Item1 + midpoint.Item1, (int)b_rotated.Item2 + midpoint.Item2), algh, cor);
        }
        private double FindDistanceToSegmentSquared(Point pt, Point p1, Point p2, out PointF closest)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return dx * dx + dy * dy;
            }

            // Calculate the t that minimizes the distance.
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                closest = new PointF(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new PointF(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }

            return dx * dx + dy * dy;
        }
        private bool MouseisOverRectangle(Point mouse, out int segment_rectangle)
        {
            for (int i = 0; i < _retangulos.Count; i++)
            {
                var x1 = _retangulos[i].Ponto_TopLeft.X;
                var y1 = _retangulos[i].Ponto_TopLeft.Y;
                var x2 = _retangulos[i].Ponto_BottomRight.X;
                var y2 = _retangulos[i].Ponto_BottomRight.Y;

                //if(FindDistanceToPointSquared(mouse,new Point((x1+x2)/2,(y1+y2)/2)) < over_dist_squared)
                //{
                //    segment_rectangle = i;
                //    return true;
                //}
                if ((mouse.X > x1 && mouse.X < x2) && (mouse.Y > y1 && mouse.Y < y2))
                {
                    segment_rectangle = i;
                    return true;
                }
            }
            segment_rectangle = -1;
            return false;
        }

        private bool MouseIsOverCircle(Point mouse, out int segment_circle)
        {
            for (int i = 0; i < _circulos.Count; i++)
            {
                if (PointInCircle(mouse, _circulos[i].Center, _circulos[i].Raio))
                {
                    segment_circle = i;
                    return true;
                }
            }
            segment_circle = -1;
            return false;
        }

        private bool MouseIsOverTriangle(Point mouse, out int segment_triangle)
        {
            for (int i = 0; i < _triangulos.Count; i++)
            {
                if (PointInTriangle(mouse, _triangulos[i].Ponto_A, _triangulos[i].Ponto_B, _triangulos[i].Ponto_C))
                {
                    segment_triangle = i;
                    return true;
                }
            }
            segment_triangle = -1;
            return false;
        }
        private bool MouseIsOverLine(Point mouse, out int segment)
        {
            for (int i = 0; i < _lines.Count; i++)
            {
                PointF closest;
                if (FindDistanceToSegmentSquared(mouse, _lines[i].Ponto_Inicial, _lines[i].Ponto_Final, out closest) < over_dist_squared)
                {
                    segment = i;
                    return true;
                }
            }
            segment = -1;
            return false;
        }

        private bool MouseisOverMidLine(Point mouse, out int segment)
        {

            for (int i = 0; i < _lines.Count; i++)
            {
                if (FindDistanceToPointSquared(mouse, MidLine(_lines[i])) < over_dist_squared)
                {
                    segment = i;
                    return true;
                }
            }
            segment = -1;
            return false;
        }
        //Algorithms
        private void drawLineWithBresenham(Point de, Point para, Graphics g, Color color)
        {
            Point point1 = de;
            Point point2 = para;
            //for 360 works:
            bool rotation = Math.Abs(point2.Y - point1.Y) > Math.Abs(point2.X - point1.X);
            if (rotation == true)
            {
                //turn rotate of line - change x and y
                Point tmp = new Point(point1.X, point1.Y);
                point1 = new Point(tmp.Y, tmp.X);

                tmp = point2;
                point2 = new Point(tmp.Y, tmp.X);
            }
            int deltaX = Math.Abs(point2.X - point1.X);
            int deltaY = Math.Abs(point2.Y - point1.Y);
            int mistake = 0;
            int deltaMistake = deltaY;
            int stepY = 0;
            int stepX = 0;
            int y = point1.Y;
            int x = point1.X;

            //Determines the direction of movement Y
            if (point1.Y < point2.Y)
            {
                stepY = 1;
            }
            else
            {
                stepY = -1;
            }

            //Determines the direction of movement X
            if (point1.X < point2.X)
            {
                stepX = 1;
            }
            else
            {
                stepX = -1;
            }

            int tmpX = 0;
            int tmpY = 0;
            while (x != point2.X) //moving on x coordinate
            {
                x += stepX;
                mistake += deltaMistake;
                if ((2 * mistake) > deltaX) //mistake - moving y coordinate
                {
                    y += stepY;
                    mistake -= deltaX;
                }
                //rotate coordinate if I change earlier
                if (rotation == true)
                {
                    tmpX = y;
                    tmpY = x;
                }
                else
                {
                    tmpX = x;
                    tmpY = y;
                }
                g.FillRectangle(new SolidBrush(color), new Rectangle(tmpX, tmpY, 4, 4));
            }
        }
        private void drawLineWithBresenham(Point de, Point para, Graphics g)
        {
            Point point1 = de;
            Point point2 = para;
            //for 360 works:
            bool rotation = Math.Abs(point2.Y - point1.Y) > Math.Abs(point2.X - point1.X);
            if (rotation == true)
            {
                //turn rotate of line - change x and y
                Point tmp = new Point(point1.X, point1.Y);
                point1 = new Point(tmp.Y, tmp.X);

                tmp = point2;
                point2 = new Point(tmp.Y, tmp.X);
            }
            int deltaX = Math.Abs(point2.X - point1.X);
            int deltaY = Math.Abs(point2.Y - point1.Y);
            int mistake = 0;
            int deltaMistake = deltaY;
            int stepY = 0;
            int stepX = 0;
            int y = point1.Y;
            int x = point1.X;

            //Determines the direction of movement Y
            if (point1.Y < point2.Y)
            {
                stepY = 1;
            }
            else
            {
                stepY = -1;
            }

            //Determines the direction of movement X
            if (point1.X < point2.X)
            {
                stepX = 1;
            }
            else
            {
                stepX = -1;
            }

            int tmpX = 0;
            int tmpY = 0;
            while (x != point2.X) //moving on x coordinate
            {
                x += stepX;
                mistake += deltaMistake;
                if ((2 * mistake) > deltaX) //mistake - moving y coordinate
                {
                    y += stepY;
                    mistake -= deltaX;
                }
                //rotate coordinate if I change earlier
                if (rotation == true)
                {
                    tmpX = y;
                    tmpY = x;
                }
                else
                {
                    tmpX = x;
                    tmpY = y;
                }
                g.FillRectangle(new SolidBrush(colorDialog1.Color), new Rectangle(tmpX, tmpY, 4, 4));
            }
        }
        private void drawLineWithDDA(Point de, Point para, Graphics g, Color color)
        {
            int dx = para.X - de.X;
            int dy = para.Y - de.Y;

            int steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);

            float Xinc = dx / (float)steps;
            float Yinc = dy / (float)steps;

            float X = de.X;
            float Y = de.Y;

            for (int i = 0; i <= steps; i++)
            {
                g.FillRectangle(new SolidBrush(color), new Rectangle((int)X, (int)Y, 4, 4));
                X += Xinc;
                Y += Yinc;
            }
        }
        private void drawLineWithDDA(Point de, Point para, Graphics g)
        {
            int dx = para.X - de.X;
            int dy = para.Y - de.Y;

            int steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);

            float Xinc = dx / (float)steps;
            float Yinc = dy / (float)steps;

            float X = de.X;
            float Y = de.Y;

            for (int i = 0; i <= steps; i++)
            {
                g.FillRectangle(new SolidBrush(colorDialog1.Color), new Rectangle((int)X, (int)Y, 4, 4));
                X += Xinc;
                Y += Yinc;
            }

        }

        private bool MouseIsOverEndPoint(Point mouse, out int segment, out Point hit_pt)
        {
            for (int i = 0; i < _lines.Count; i++)
            {
                if (FindDistanceToPointSquared(mouse, _lines[i].Ponto_Inicial) < over_dist_squared)
                {
                    segment = i;
                    hit_pt = _lines[i].Ponto_Inicial;
                    return true;
                }
                if (FindDistanceToPointSquared(mouse, _lines[i].Ponto_Final) < over_dist_squared)
                {
                    segment = i;
                    hit_pt = _lines[i].Ponto_Final;
                    return true;
                }
            }
            segment = -1;
            hit_pt = Point.Empty;
            return false;
        }
        private bool MouseIsOverEndPointRet(Point mouse, out int segment_rectangle, out Point hit_pt)
        {
            for (int i = 0; i < _retangulos.Count; i++)
            {
                if (FindDistanceToPointSquared(mouse, _retangulos[i].Ponto_TopLeft) < over_dist_squared)
                {
                    segment_rectangle = i;
                    hit_pt = _retangulos[i].Ponto_TopLeft;
                    return true;
                }
                if (FindDistanceToPointSquared(mouse, _retangulos[i].Ponto_BottomRight) < over_dist_squared)
                {
                    segment_rectangle = i;
                    hit_pt = _retangulos[i].Ponto_BottomRight;
                    return true;
                }
            }
            segment_rectangle = -1;
            hit_pt = Point.Empty;
            return false;
        }
        private bool MouseIsOverEndPointTriangle(Point mouse, out int segment_triangle, out Point hit_pt)
        {
            for (int i = 0; i < _triangulos.Count; i++)
            {
                if (FindDistanceToPointSquared(mouse, _triangulos[i].Ponto_A) < over_dist_squared)
                {
                    segment_triangle = i;
                    hit_pt = _triangulos[i].Ponto_A;
                    return true;
                }
                if (FindDistanceToPointSquared(mouse, _triangulos[i].Ponto_B) < over_dist_squared)
                {
                    segment_triangle = i;
                    hit_pt = _triangulos[i].Ponto_B;
                    return true;
                }
                if (FindDistanceToPointSquared(mouse, _triangulos[i].Ponto_C) < over_dist_squared)
                {
                    segment_triangle = i;
                    hit_pt = _triangulos[i].Ponto_C;
                    return true;
                }
            }
            segment_triangle = -1;
            hit_pt = Point.Empty;
            return false;
        }
        private bool MouseIsOverEndPointCircle(Point mouse, out int segment_circle, out Point hit_pt)
        {
            for (int i = 0; i < _circulos.Count; i++)
            {
                if (FindDistanceToPointSquared(mouse, _circulos[i].PRaio) < over_dist_squared)
                {
                    segment_circle = i;
                    hit_pt = _circulos[i].PRaio;
                    return true;
                }
            }
            segment_circle = -1;
            hit_pt = Point.Empty;
            return false;
        }

        private int FindDistanceToPointSquared(Point p1, Point p2)
        {
            int dx = p1.X - p2.X;
            int dy = p1.Y - p2.Y;
            return dx * dx + dy * dy;
        }



        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor cursor = Cursors.Cross;
            Point hit_Point;

            if (e.Button == MouseButtons.Left)
            {
                final = new Point(e.X, e.Y);
                Canvas.Invalidate();
            }

            //Linha
            if (MouseisOverMidLine(e.Location, out segment))
                cursor = Cursors.WaitCursor;
            else if (MouseIsOverEndPoint(e.Location, out segment, out hit_Point))
                cursor = Cursors.Arrow;
            else if (MouseIsOverLine(e.Location, out segment))
                cursor = Cursors.Hand;
            else
                segment = -1;

            //Retangulo
            if (MouseIsOverEndPointRet(e.Location, out segment_rectangle, out hit_Point))
                cursor = Cursors.Arrow;
            else if (MouseisOverRectangle(e.Location, out segment_rectangle))
                cursor = Cursors.Hand;
            else
                segment_rectangle = -1;

            //Triangulo
            if (MouseIsOverEndPointTriangle(e.Location, out segment_triangle, out hit_Point))
                cursor = Cursors.Arrow;
            else if (MouseIsOverTriangle(e.Location, out segment_triangle))
                cursor = Cursors.Hand;
            else
                segment_triangle = -1;

            //Circulo
            if (MouseIsOverEndPointCircle(e.Location, out segment_circle, out hit_Point))
                cursor = Cursors.Arrow;
            else if (MouseIsOverCircle(e.Location, out segment_circle))
                cursor = Cursors.Hand;
            else
                segment_circle = -1;


            if (Canvas.Cursor != cursor)
                Canvas.Cursor = cursor;
        }
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Point hit_point;
            if (MouseisOverMidLine(e.Location, out segment))
            {
                Canvas.MouseMove -= Canvas_MouseMove;
                Canvas.MouseMove += Canvas_MouseMove_Rotate;
                Canvas.MouseUp -= Canvas_MouseUp;
                Canvas.MouseUp += Canvas_MouseUp_Rotate;
            }
            else if (MouseIsOverEndPoint(e.Location, out segment, out hit_point))
            {
                Canvas.MouseMove -= Canvas_MouseMove;
                Canvas.MouseMove += Canvas_MouseMove_MovingEndPoint;
                Canvas.MouseUp -= Canvas_MouseUp;
                Canvas.MouseUp += Canvas_MouseUp_MovingEndPoint;

                MovingStartEndPoint = (_lines[segment].Ponto_Inicial.Equals(hit_point));

                OffsetX = hit_point.X - e.X;
                OffsetY = hit_point.Y - e.Y;
            }
            else if (MouseIsOverLine(e.Location, out segment))
            {
                Canvas.MouseMove -= Canvas_MouseMove;
                Canvas.MouseMove += Canvas_MouseMove_MoveLine;
                Canvas.MouseUp -= Canvas_MouseUp;
                Canvas.MouseUp += Canvas_MouseUp_MoveLine;

                OffsetX = _lines[segment].Ponto_Inicial.X - e.X;
                OffsetY = _lines[segment].Ponto_Inicial.Y - e.Y;
            }
            else if (MouseIsOverEndPointRet(e.Location, out segment_rectangle, out hit_point))
            {
                Canvas.MouseMove -= Canvas_MouseMove;
                Canvas.MouseMove += Canvas_MouseMove_MovingEndPointRet;
                Canvas.MouseUp -= Canvas_MouseUp;
                Canvas.MouseUp += Canvas_MouseUP_MovingEndPointRet;

                MovingStartEndPoint = (_retangulos[segment_rectangle].Ponto_TopLeft.Equals(hit_point));

                OffsetX = hit_point.X - e.X;
                OffsetY = hit_point.Y - e.Y;
            }
            else if (MouseisOverRectangle(e.Location, out segment_rectangle))
            {
                Canvas.MouseMove -= Canvas_MouseMove;
                Canvas.MouseMove += Canvas_MouseMove_Retangulo;
                Canvas.MouseUp -= Canvas_MouseUp;
                Canvas.MouseUp += Canvas_Mouseup_MoveRetangulo;



                OffsetX = _retangulos[segment_rectangle].Ponto_TopLeft.X - e.X;
                OffsetY = _retangulos[segment_rectangle].Ponto_TopLeft.Y - e.Y;
            }
            else if (MouseIsOverEndPointTriangle(e.Location, out segment_triangle, out hit_point))
            {
                Canvas.MouseMove -= Canvas_MouseMove;
                Canvas.MouseMove += Canvas_MouseMove_MovingEndPointTriangle;
                Canvas.MouseUp -= Canvas_MouseUp;
                Canvas.MouseUp += Canvas_MouseUp_MovingEndPointTriangle;

                MovingStartEndPoint = (_triangulos[segment_triangle].Ponto_A.Equals(hit_point));
                MovingStartEndPointTriangle = (_triangulos[segment_triangle].Ponto_C.Equals(hit_point));

                OffsetX = hit_point.X - e.X;
                OffsetY = hit_point.Y - e.Y;
            }
            else if (MouseIsOverTriangle(e.Location, out segment_triangle))
            {
                Canvas.MouseMove -= Canvas_MouseMove;
                Canvas.MouseMove += Canvas_MouseMove_Triangulo;
                Canvas.MouseUp -= Canvas_MouseUp;
                Canvas.MouseUp += Canvas_MouseUp_MoveTriangle;

                OffsetX = _triangulos[segment_triangle].Ponto_A.X - e.X;
                OffsetY = _triangulos[segment_triangle].Ponto_A.Y - e.Y;
            }
            else if (MouseIsOverEndPointCircle(e.Location, out segment_circle, out hit_point))
            {

                Canvas.MouseMove -= Canvas_MouseMove;
                Canvas.MouseMove += Canvas_MouseMove_MovingEndPointCircle;
                Canvas.MouseUp -= Canvas_MouseUp;
                Canvas.MouseUp += Canvas_MouseUp_MovingEndPointCircle;

                MovingStartEndPoint = _circulos[segment_circle].PRaio.Equals(hit_point);

                OffsetX = hit_point.X - e.X;
                OffsetY = hit_point.Y - e.Y;
            }
            else if (MouseIsOverCircle(e.Location, out segment_circle))
            {
                Canvas.MouseMove -= Canvas_MouseMove;
                Canvas.MouseMove += Canvas_MouseMove_Circulo;
                Canvas.MouseUp -= Canvas_MouseUp;
                Canvas.MouseUp += Canvas_MouseUp_MoveCircle;

                OffsetX = _circulos[segment_circle].Center.X - e.X;
                OffsetY = _circulos[segment_circle].Center.Y - e.Y;
            }
            else
            {
                if (rb_Reta.Checked)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        drag = true;
                        inicial = new Point(e.X, e.Y);

                    }
                    else
                    {
                        drag = false;
                        inicial = Point.Empty;
                    }
                    final = Point.Empty;
                    Canvas.Invalidate();
                }
                else if (rb_Retangulo.Checked)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        drag = true;
                        inicial = new Point(e.X, e.Y);
                    }
                    else
                    {
                        drag = false;
                        inicial = Point.Empty;
                    }
                    final = Point.Empty;
                    Canvas.Invalidate();
                }
                else if (rb_Triangulo.Checked)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        drag = true;
                        inicial = new Point(e.X, e.Y);
                    }
                    else
                    {
                        drag = false;
                        inicial = Point.Empty;
                    }
                    final = Point.Empty;
                    Canvas.Invalidate();
                }
                else if (rb_Circulo.Checked)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        drag = true;
                        inicial = new Point(e.X, e.Y);
                    }
                    else
                    {
                        drag = false;
                        inicial = Point.Empty;
                    }
                    final = Point.Empty;
                    Canvas.Invalidate();
                }

            }
        }
        private void Canvas_MouseMove_Rotate(object sender, MouseEventArgs e)
        {
            var aux = _lines[segment];
            var mat = new Matrix();

            mat.Translate(MidLine(aux).X, MidLine(aux).Y);
            mat.Rotate(0);
            mat.Invert();
            var point = mat.TransformPoint(new PointF(e.X, e.Y));
            var vecX = point.X;
            var vecY = -point.Y;

            var len = Math.Sqrt(vecX * vecX + vecY * vecY);

            var normX = vecX / len;
            var normY = vecY / len;

            //In rectangles's space, 
            //compute dot product between, 
            //Up and mouse-position vector
            var dotProduct = (0 * normX + 1 * normY);
            var angle = Math.Acos(dotProduct);

            if (point.X < 0)
                angle = -angle;

            // Add (delta-radians) to rotation as degrees
            //angle = (float)((180 / Math.PI) * angle);

            rotateLine(angle);
            Canvas.Refresh();

        }
        private void Canvas_MouseUp_Rotate(object sender, MouseEventArgs e)
        {
            Canvas.MouseMove += Canvas_MouseMove;
            Canvas.MouseMove -= Canvas_MouseMove_Rotate;
            Canvas.MouseUp -= Canvas_MouseUp_Rotate;
            Canvas.MouseUp += Canvas_MouseUp;

            Canvas.Refresh();
        }

        private void Canvas_MouseUp_MovingEndPoint(object sender, MouseEventArgs e)
        {
            Canvas.MouseMove += Canvas_MouseMove;
            Canvas.MouseMove -= Canvas_MouseMove_MovingEndPoint;
            Canvas.MouseUp -= Canvas_MouseUp_MovingEndPoint;
            Canvas.MouseUp += Canvas_MouseUp;

            Canvas.Refresh();
        }

        private void Canvas_MouseMove_MovingEndPoint(object sender, MouseEventArgs e)
        {
            var aux = _lines[segment];

            if (MovingStartEndPoint)
            {
                _lines[segment] = new Linha(new Point(e.X + OffsetX, e.Y + OffsetY), aux.Ponto_Final, aux.Algorithm, aux.Color);
                treeView1.Nodes[0].Nodes[0].Nodes[segment].Text = ($"X: {new Point(e.X + OffsetX, e.Y + OffsetY)}, Y: {aux.Ponto_Final}, Alg: {nameof(aux.Algorithm)},Cor: {aux.Color}");
                treeView1.Nodes[0].Nodes[0].ExpandAll();
            }
            else
            {
                _lines[segment] = new Linha(aux.Ponto_Inicial, new Point(e.X + OffsetX, e.Y + OffsetY), aux.Algorithm, aux.Color);
                treeView1.Nodes[0].Nodes[0].Nodes[segment].Text = ($"X: {aux.Ponto_Inicial}, Y: {new Point(e.X + OffsetX, e.Y + OffsetY)}, Alg: {nameof(aux.Algorithm)},Cor: {aux.Color}");
                treeView1.Nodes[0].Nodes[0].ExpandAll();
            }
            Canvas.Refresh();
        }
        private void Canvas_MouseUP_MovingEndPointRet(object sender, MouseEventArgs e)
        {
            Canvas.MouseMove += Canvas_MouseMove;
            Canvas.MouseMove -= Canvas_MouseMove_MovingEndPointRet;
            Canvas.MouseUp -= Canvas_MouseUP_MovingEndPointRet;
            Canvas.MouseUp += Canvas_MouseUp;

            Canvas.Refresh();
        }
        private void Canvas_MouseMove_MovingEndPointRet(object sender, MouseEventArgs e)
        {
            if (segment_rectangle >= 0)
            {
                var aux = _retangulos[segment_rectangle];

                if (MovingStartEndPoint)
                {
                    _retangulos[segment_rectangle] = new Retangulo(new Point(e.X + OffsetX, e.Y + OffsetY), aux.Ponto_BottomRight, aux.Algorithm, aux.Color);
                    treeView1.Nodes[0].Nodes[1].Nodes[segment_rectangle].Text = ($"X: {new Point(e.X + OffsetX, e.Y + OffsetY)}, Y: {aux.Ponto_BottomRight}, Alg: {nameof(aux.Algorithm)}, Cor: {aux.Color}");
                    treeView1.Nodes[0].Nodes[1].ExpandAll();
                }
                else
                {
                    _retangulos[segment_rectangle] = new Retangulo(aux.Ponto_TopLeft, new Point(e.X + OffsetX, e.Y + OffsetY), aux.Algorithm, aux.Color);
                    treeView1.Nodes[0].Nodes[1].Nodes[segment_rectangle].Text = ($"X: {aux.Ponto_TopLeft}, Y: {new Point(e.X + OffsetX, e.Y + OffsetY)}, Alg: {nameof(aux.Algorithm)}, Cor: {aux.Color}");
                    treeView1.Nodes[0].Nodes[1].ExpandAll();
                }
                Canvas.Refresh();
            }
        }

        private void Canvas_MouseUp_MovingEndPointTriangle(object sender, MouseEventArgs e)
        {
            Canvas.MouseMove += Canvas_MouseMove;
            Canvas.MouseMove -= Canvas_MouseMove_MovingEndPointTriangle;
            Canvas.MouseUp -= Canvas_MouseUp_MovingEndPointTriangle;
            Canvas.MouseUp += Canvas_MouseUp;

            Canvas.Refresh();
        }
        private void Canvas_MouseMove_MovingEndPointTriangle(object sender, MouseEventArgs e)
        {
            if (segment_triangle >= 0)
            {
                var aux = _triangulos[segment_triangle];

                if (MovingStartEndPoint && !MovingStartEndPointTriangle)
                {
                    _triangulos[segment_triangle] = new Triangulo(new Point(e.X + OffsetX, e.Y + OffsetY), aux.Ponto_B, aux.Ponto_C, aux.Algorithm, aux.Color);
                    treeView1.Nodes[0].Nodes[2].Nodes[segment_triangle].Text = ($"A: {new Point(e.X + OffsetX, e.Y + OffsetY)}, B:{aux.Ponto_B}, C:{aux.Ponto_C}, Alg: {nameof(aux.Algorithm)}, Cor: {aux.Color}");
                    treeView1.Nodes[0].Nodes[2].ExpandAll();
                }
                else if (!MovingStartEndPoint && !MovingStartEndPointTriangle)
                {
                    _triangulos[segment_triangle] = new Triangulo(aux.Ponto_A, new Point(e.X + OffsetX, e.Y + OffsetY), aux.Ponto_C, aux.Algorithm, aux.Color);
                    treeView1.Nodes[0].Nodes[2].Nodes[segment_triangle].Text = ($"A: {aux.Ponto_A}, B:{new Point(e.X + OffsetX, e.Y + OffsetY)}, C:{aux.Ponto_C}, Alg: {nameof(aux.Algorithm)}, Cor: {aux.Color}");
                    treeView1.Nodes[0].Nodes[2].ExpandAll();
                }
                else if (!MovingStartEndPoint && MovingStartEndPointTriangle)
                {
                    _triangulos[segment_triangle] = new Triangulo(aux.Ponto_A, aux.Ponto_B, new Point(e.X + OffsetX, e.Y + OffsetY), aux.Algorithm, aux.Color);
                    treeView1.Nodes[0].Nodes[2].Nodes[segment_triangle].Text = ($"A: {aux.Ponto_A}, B:{ aux.Ponto_B}, C:{ new Point(e.X + OffsetX, e.Y + OffsetY)}, Alg: {nameof(aux.Algorithm)}, Cor: {aux.Color}");
                    treeView1.Nodes[0].Nodes[2].ExpandAll();
                }
                Canvas.Refresh();
            }
        }

        private void Canvas_MouseUp_MovingEndPointCircle(object sender, MouseEventArgs e)
        {
            Canvas.MouseMove += Canvas_MouseMove;
            Canvas.MouseMove -= Canvas_MouseMove_MovingEndPointCircle;
            Canvas.MouseUp -= Canvas_MouseUp_MovingEndPointCircle;
            Canvas.MouseUp += Canvas_MouseUp;

            Canvas.Refresh();
        }
        private void Canvas_MouseMove_MovingEndPointCircle(object sender, MouseEventArgs e)
        {
            if (segment_circle >= 0)
            {
                var aux = _circulos[segment_circle];

                if (MovingStartEndPoint)
                {
                    Point nRaio = new Point(e.X + OffsetX, e.Y + OffsetY);
                    _circulos[segment_circle] = new Circulo(aux.Center, nRaio, Radio(aux.Center, nRaio), aux.Algorithm, aux.Color);
                    treeView1.Nodes[0].Nodes[3].Nodes[segment_circle].Text = ($"Center: {aux.Center}, Raio: {Radio(aux.Center, nRaio)},Alg: {nameof(aux.Algorithm)}, Cor: {aux.Color}");
                    treeView1.Nodes[0].Nodes[3].ExpandAll();
                }
            }
            Canvas.Refresh();
        }

        private void Canvas_Mouseup_MoveRetangulo(object sender, MouseEventArgs e)
        {
            Canvas.MouseMove += Canvas_MouseMove;
            Canvas.MouseMove -= Canvas_MouseMove_Retangulo;
            Canvas.MouseUp -= Canvas_MouseMove_Retangulo;
            Canvas.MouseUp += Canvas_MouseUp;
            Canvas.Refresh();
        }
        private void Canvas_MouseMove_Retangulo(object sender, MouseEventArgs e)
        {
            if (segment_rectangle >= 0)
            {

                var aux = _retangulos[segment_rectangle];
                int new_x1 = e.X + OffsetX;
                int new_y1 = e.Y + OffsetY;

                int dx = new_x1 - _retangulos[segment_rectangle].Ponto_TopLeft.X;
                int dy = new_y1 - _retangulos[segment_rectangle].Ponto_TopLeft.Y;

                if (dx == 0 && dy == 0) return;

                _retangulos[segment_rectangle] = new Retangulo(new Point(new_x1, new_y1), new Point(_retangulos[segment_rectangle].Ponto_BottomRight.X + dx, _retangulos[segment_rectangle].Ponto_BottomRight.Y + dy), aux.Algorithm, aux.Color);
                treeView1.Nodes[0].Nodes[1].Nodes[segment_rectangle].Text = ($"X: {new Point(new_x1, new_y1)}, Y: {new Point(_retangulos[segment_rectangle].Ponto_BottomRight.X + dx, _retangulos[segment_rectangle].Ponto_BottomRight.Y + dy)}, Alg: {nameof(aux.Algorithm)}, Cor: {aux.Color}");
                treeView1.Nodes[0].Nodes[1].ExpandAll();



                Canvas.Refresh();
            }
        }
        private void Canvas_MouseMove_Triangulo(object sender, MouseEventArgs e)
        {
            if (segment_triangle >= 0)
            {
                var aux = _triangulos[segment_triangle];
                int new_x1 = e.X + OffsetX;
                int new_y1 = e.Y + OffsetY;

                int dx = new_x1 - _triangulos[segment_triangle].Ponto_A.X;
                int dy = new_y1 - _triangulos[segment_triangle].Ponto_A.Y;

                if (dx == 0 && dy == 0) return;
                Point A = new Point(new_x1, new_y1);
                Point B = new Point(_triangulos[segment_triangle].Ponto_B.X + dx, _triangulos[segment_triangle].Ponto_B.Y + dy);
                Point C = new Point(_triangulos[segment_triangle].Ponto_C.X + dx, _triangulos[segment_triangle].Ponto_C.Y + dy);

                _triangulos[segment_triangle] = new Triangulo(A, B,C, aux.Algorithm, aux.Color);
                treeView1.Nodes[0].Nodes[2].Nodes[segment_triangle].Text = ($"A: {A}, B:{B}, C:{C}, Alg: {nameof(aux.Algorithm)}, Cor: {aux.Color}");
                treeView1.Nodes[0].Nodes[2].ExpandAll();

                Canvas.Refresh();
            }
        }
        private void Canvas_MouseUp_MoveTriangle(object sender, MouseEventArgs e)
        {
            Canvas.MouseMove += Canvas_MouseMove;
            Canvas.MouseMove -= Canvas_MouseMove_Triangulo;
            Canvas.MouseUp -= Canvas_MouseUp_MoveTriangle;
            Canvas.MouseUp += Canvas_MouseUp;
            Canvas.Refresh();
        }

        private void Canvas_MouseMove_Circulo(object sender, MouseEventArgs e)
        {
            if (segment_circle >= 0)
            {
                var aux = _circulos[segment_circle];
                int new_x1 = e.X + OffsetX;
                int new_y1 = e.Y + OffsetY;

                int dx = new_x1 - aux.Center.X;
                int dy = new_y1 - aux.Center.Y;

                if (dx == 0 && dy == 0) return;

                _circulos[segment_circle] = new Circulo(new Point(new_x1, new_y1), new Point(aux.PRaio.X + dx, aux.PRaio.Y + dy), aux.Raio, aux.Algorithm, aux.Color);

                treeView1.Nodes[0].Nodes[3].Nodes[segment_circle].Text = ($"Center: {new Point(new_x1, new_y1)}, Raio: {aux.Raio},Alg: {nameof(aux.Algorithm)}, Cor: {aux.Color}");
                treeView1.Nodes[0].Nodes[3].ExpandAll();

                Canvas.Refresh();
            }
        }
        private void Canvas_MouseUp_MoveCircle(object sender, MouseEventArgs e)
        {
            Canvas.MouseMove += Canvas_MouseMove;
            Canvas.MouseMove -= Canvas_MouseMove_Circulo;
            Canvas.MouseUp -= Canvas_MouseUp_MoveCircle;
            Canvas.MouseUp += Canvas_MouseUp;
            Canvas.Refresh();
        }


        private void Canvas_MouseUp_MoveLine(object sender, MouseEventArgs e)
        {
            Canvas.MouseMove += Canvas_MouseMove;
            Canvas.MouseMove -= Canvas_MouseMove_MoveLine;
            Canvas.MouseUp -= Canvas_MouseUp_MoveLine;
            Canvas.MouseUp += Canvas_MouseUp;
            Canvas.Refresh();
        }

        private void Canvas_MouseMove_MoveLine(object sender, MouseEventArgs e)
        {
            if (segment >= 0)
            {
                var aux = _lines[segment];
                int new_x1 = e.X + OffsetX;
                int new_y1 = e.Y + OffsetY;

                int dx = new_x1 - _lines[segment].Ponto_Inicial.X;
                int dy = new_y1 - _lines[segment].Ponto_Inicial.Y;

                if (dx == 0 && dy == 0) return;

                // Move the segment to its new location.
                _lines[segment] = new Linha(new Point(new_x1, new_y1), new Point(_lines[segment].Ponto_Final.X + dx, _lines[segment].Ponto_Final.Y + dy), aux.Algorithm, aux.Color);
                treeView1.Nodes[0].Nodes[0].Nodes[segment].Text = ($"X: {new Point(new_x1, new_y1)}, Y: {new Point(_lines[segment].Ponto_Final.X + dx, _lines[segment].Ponto_Final.Y + dy)}, Alg: {nameof(aux.Algorithm)},Cor: {aux.Color}");
                treeView1.Nodes[0].Nodes[0].ExpandAll();

                Canvas.Refresh();
            }
        }



        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect;
            foreach (var line in _lines)
            {
                if (line.Algorithm == Algorithm.bresenham)
                    drawLineWithBresenham(line.Ponto_Inicial, line.Ponto_Final, e.Graphics, line.Color);
                else if (line.Algorithm == Algorithm.DDA)
                {
                    drawLineWithDDA(line.Ponto_Inicial, line.Ponto_Final, e.Graphics, line.Color);
                }
                //Primeiro circle
                rect = new Rectangle(line.Ponto_Inicial.X - circle_radius, line.Ponto_Inicial.Y - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1);
                e.Graphics.FillEllipse(Brushes.White, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);

                //Segundo Circle
                rect = new Rectangle(line.Ponto_Final.X - circle_radius, line.Ponto_Final.Y - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1);
                e.Graphics.FillEllipse(Brushes.White, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);

                //Terceiro Circle
                var midpoint = ((line.Ponto_Inicial.X + line.Ponto_Final.X) / 2, (line.Ponto_Inicial.Y + line.Ponto_Final.Y) / 2);
                rect = new Rectangle(midpoint.Item1 - circle_radius, midpoint.Item2 - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1);
                e.Graphics.FillEllipse(Brushes.Green, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);

            }
            foreach (var retangulo in _retangulos)
            {
                if (retangulo.Algorithm == Algorithm.bresenham)
                {
                    drawLineWithBresenham(retangulo.Ponto_TopLeft, retangulo.Ponto_BottomLeft, e.Graphics, retangulo.Color);
                    drawLineWithBresenham(retangulo.Ponto_BottomLeft, retangulo.Ponto_BottomRight, e.Graphics, retangulo.Color);
                    drawLineWithBresenham(retangulo.Ponto_BottomRight, retangulo.Ponto_TopRigth, e.Graphics, retangulo.Color);
                    drawLineWithBresenham(retangulo.Ponto_TopRigth, retangulo.Ponto_TopLeft, e.Graphics, retangulo.Color);

                    //Primeiro circle
                    rect = new Rectangle(retangulo.Ponto_TopLeft.X - circle_radius, retangulo.Ponto_TopLeft.Y - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1); ;
                    e.Graphics.FillEllipse(Brushes.White, rect);
                    e.Graphics.DrawEllipse(Pens.Black, rect);

                    //Segundo Circle
                    rect = new Rectangle(retangulo.Ponto_BottomRight.X - circle_radius, retangulo.Ponto_BottomRight.Y - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, rect);
                    e.Graphics.DrawEllipse(Pens.Black, rect);


                    rect = new Rectangle((retangulo.Ponto_TopLeft.X + retangulo.Ponto_BottomRight.X) / 2 - circle_radius, (retangulo.Ponto_TopLeft.Y + retangulo.Ponto_BottomRight.Y) / 2 - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1);
                    e.Graphics.FillEllipse(Brushes.Green, rect);
                    e.Graphics.DrawEllipse(Pens.Black, rect);

                }
                else if (retangulo.Algorithm == Algorithm.DDA)
                {
                    drawLineWithDDA(retangulo.Ponto_TopLeft, retangulo.Ponto_BottomLeft, e.Graphics, retangulo.Color);
                    drawLineWithDDA(retangulo.Ponto_BottomLeft, retangulo.Ponto_BottomRight, e.Graphics, retangulo.Color);
                    drawLineWithDDA(retangulo.Ponto_BottomRight, retangulo.Ponto_TopRigth, e.Graphics, retangulo.Color);
                    drawLineWithDDA(retangulo.Ponto_TopRigth, retangulo.Ponto_TopLeft, e.Graphics, retangulo.Color);

                    //Primeiro circle
                    rect = new Rectangle(retangulo.Ponto_TopLeft.X - circle_radius, retangulo.Ponto_TopLeft.Y - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1); ;
                    e.Graphics.FillEllipse(Brushes.White, rect);
                    e.Graphics.DrawEllipse(Pens.Black, rect);

                    //Segundo Circle
                    rect = new Rectangle(retangulo.Ponto_BottomRight.X - circle_radius, retangulo.Ponto_BottomRight.Y - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, rect);
                    e.Graphics.DrawEllipse(Pens.Black, rect);


                    rect = new Rectangle((retangulo.Ponto_TopLeft.X + retangulo.Ponto_BottomRight.X) / 2 - circle_radius, (retangulo.Ponto_TopLeft.Y + retangulo.Ponto_BottomRight.Y) / 2 - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1);
                    e.Graphics.FillEllipse(Brushes.Green, rect);
                    e.Graphics.DrawEllipse(Pens.Black, rect);
                }
            }
            foreach (var triangulo in _triangulos)
            {
                if (triangulo.Algorithm == Algorithm.bresenham)
                {

                    drawLineWithBresenham(triangulo.Ponto_A, triangulo.Ponto_B, e.Graphics, triangulo.Color);
                    drawLineWithBresenham(triangulo.Ponto_B, triangulo.Ponto_C, e.Graphics, triangulo.Color);
                    drawLineWithBresenham(triangulo.Ponto_A, triangulo.Ponto_C, e.Graphics, triangulo.Color);


                }
                else if (triangulo.Algorithm == Algorithm.DDA)
                {
                    drawLineWithDDA(triangulo.Ponto_A, triangulo.Ponto_B, e.Graphics, triangulo.Color);
                    drawLineWithDDA(triangulo.Ponto_B, triangulo.Ponto_C, e.Graphics, triangulo.Color);
                    drawLineWithDDA(triangulo.Ponto_A, triangulo.Ponto_C, e.Graphics, triangulo.Color);
                }

                //Ponto A
                rect = new Rectangle(triangulo.Ponto_A.X - circle_radius, triangulo.Ponto_A.Y - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1); ;
                e.Graphics.FillEllipse(Brushes.White, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);

                //Ponto B
                rect = new Rectangle(triangulo.Ponto_B.X - circle_radius, triangulo.Ponto_B.Y - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1); ;
                e.Graphics.FillEllipse(Brushes.White, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);

                //Ponto A
                rect = new Rectangle(triangulo.Ponto_C.X - circle_radius, triangulo.Ponto_C.Y - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1); ;
                e.Graphics.FillEllipse(Brushes.White, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);

                //Center 
                rect = new Rectangle((triangulo.Ponto_A.X + triangulo.Ponto_B.X + triangulo.Ponto_C.X) / 3 - circle_radius, (triangulo.Ponto_A.Y + triangulo.Ponto_B.Y + triangulo.Ponto_C.Y) / 3 - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1); ;
                e.Graphics.FillEllipse(Brushes.Green, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);

            }
            foreach (var circulo in _circulos)
            {
                if (circulo.Algorithm == Algorithm.bresenham)
                {
                    DrawCircleBreseham(circulo.Center, circulo.Raio, e.Graphics, circulo.Color);
                }
                else if (circulo.Algorithm == Algorithm.DDA)
                {
                    DrawCircleDDA(circulo.Center, circulo.Raio, e.Graphics, circulo.Color);
                }
                // Center
                rect = new Rectangle(circulo.Center.X - circle_radius, circulo.Center.Y - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1); ;
                e.Graphics.FillEllipse(Brushes.Green, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);

                // Resize
                rect = new Rectangle(circulo.PRaio.X - circle_radius, circulo.PRaio.Y - circle_radius, 2 * circle_radius + 1, 2 * circle_radius + 1); ;
                e.Graphics.FillEllipse(Brushes.White, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);

            }
            if (!inicial.IsEmpty && !final.IsEmpty)
            {
                if (rb_Reta.Checked)
                {
                    if (rb_Bresh.Checked)
                        drawLineWithBresenham(inicial, final, e.Graphics);
                    else if (rb_DDA.Checked)
                    {
                        drawLineWithDDA(inicial, final, e.Graphics);
                    }
                }
                else if (rb_Retangulo.Checked)
                {
                    if (rb_Bresh.Checked)
                    {
                        drawLineWithBresenham(inicial, new Point(inicial.X, final.Y), e.Graphics);
                        drawLineWithBresenham(new Point(inicial.X, final.Y), final, e.Graphics);
                        drawLineWithBresenham(final, new Point(final.X, inicial.Y), e.Graphics);
                        drawLineWithBresenham(new Point(final.X, inicial.Y), inicial, e.Graphics);
                    }
                    else if (rb_DDA.Checked)
                    {
                        drawLineWithDDA(inicial, new Point(inicial.X, final.Y), e.Graphics);
                        drawLineWithDDA(new Point(inicial.X, final.Y), final, e.Graphics);
                        drawLineWithDDA(final, new Point(final.X, inicial.Y), e.Graphics);
                        drawLineWithDDA(new Point(final.X, inicial.Y), inicial, e.Graphics);
                    }
                }
                else if (rb_Triangulo.Checked)
                {
                    if (rb_Bresh.Checked)
                    {
                        drawLineWithBresenham(new Point(final.X - 50, final.Y + 25), new Point(final.X, final.Y - 50), e.Graphics);
                        drawLineWithBresenham(new Point(final.X + 50, final.Y + 25), new Point(final.X, final.Y - 50), e.Graphics);
                        drawLineWithBresenham(new Point(final.X - 50, final.Y + 25), new Point(final.X + 50, final.Y + 25), e.Graphics);

                    }
                    else if (rb_DDA.Checked)
                    {
                        drawLineWithDDA(new Point(final.X - 50, final.Y + 25), new Point(final.X, final.Y - 50), e.Graphics);
                        drawLineWithDDA(new Point(final.X + 50, final.Y + 25), new Point(final.X, final.Y - 50), e.Graphics);
                        drawLineWithDDA(new Point(final.X - 50, final.Y + 25), new Point(final.X + 50, final.Y + 25), e.Graphics);
                    }
                }
                else if (rb_Circulo.Checked)
                {
                    if (rb_Bresh.Checked)
                    {
                        DrawCircleBreseham(inicial, Radio(inicial, final), e.Graphics);
                    }
                    else if (rb_DDA.Checked)
                    {
                        DrawCircleDDA(inicial, Radio(inicial, final), e.Graphics);
                    }
                }
            }
        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MouseIsOverLine(e.Location, out segment))
                {
                    _lines.RemoveAt(segment);
                    treeView1.Nodes[0].Nodes[0].Nodes.RemoveAt(segment);
                }
                if (MouseisOverRectangle(e.Location, out segment_rectangle))
                {
                    _retangulos.RemoveAt(segment_rectangle);
                    treeView1.Nodes[0].Nodes[1].Nodes.RemoveAt(segment_rectangle);
                }
                if (MouseIsOverTriangle(e.Location, out segment_triangle))
                {
                    _triangulos.RemoveAt(segment_triangle);
                    treeView1.Nodes[0].Nodes[2].Nodes.RemoveAt(segment_triangle);
                }
                if (MouseIsOverCircle(e.Location, out segment_circle))
                {
                    _circulos.RemoveAt(segment_circle);
                    treeView1.Nodes[0].Nodes[3].Nodes.RemoveAt(segment_circle);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var res = colorDialog1.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            _lines = new List<Linha>();
            treeView1.Nodes[0].Nodes[0].Nodes.Clear();
            _retangulos = new List<Retangulo>();
            treeView1.Nodes[0].Nodes[1].Nodes.Clear();
            _triangulos = new List<Triangulo>();
            treeView1.Nodes[0].Nodes[2].Nodes.Clear();
            _circulos = new List<Circulo>();
            treeView1.Nodes[0].Nodes[3].Nodes.Clear();
            Canvas.Invalidate();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (rb_Reta.Checked)
            {
                if (drag && !inicial.IsEmpty && !final.IsEmpty)
                {
                    _lines.Add(new Linha(inicial, final, rb_Bresh.Checked == true ? Algorithm.bresenham : Algorithm.DDA, colorDialog1.Color));
                    treeView1.Nodes[0].Nodes[0].Nodes.Add($"X: {inicial}, Y: {final}, Alg: {(rb_Bresh.Checked == true ? "Bresenham" : "DDA")},Cor: {colorDialog1.Color}");
                    treeView1.Nodes[0].Nodes[0].ExpandAll();
                }
                drag = false;
                inicial = Point.Empty;
                final = Point.Empty;
                Canvas.Invalidate();
            }
            else if (rb_Retangulo.Checked)
            {
                if (drag && !inicial.IsEmpty && !final.IsEmpty)
                {
                    _retangulos.Add(new Retangulo(inicial, final, rb_Bresh.Checked == true ? Algorithm.bresenham : Algorithm.DDA, colorDialog1.Color));
                    treeView1.Nodes[0].Nodes[1].Nodes.Add($"X: {inicial}, Y: {final}, Alg: {(rb_Bresh.Checked == true ? "Bresenham" : "DDA")}, Cor: {colorDialog1.Color}");
                    treeView1.Nodes[0].Nodes[1].ExpandAll();
                }
                drag = false;
                inicial = Point.Empty;
                final = Point.Empty;
                Canvas.Invalidate();
            }
            else if (rb_Triangulo.Checked)
            {
                if (drag && !inicial.IsEmpty && !final.IsEmpty)
                {
                    Point A = new Point(final.X - 50, final.Y + 25);
                    Point B = new Point(final.X, final.Y - 50);
                    Point C = new Point(final.X + 50, final.Y + 25);
                    _triangulos.Add(new Triangulo(A, B, C, rb_Bresh.Checked == true ? Algorithm.bresenham : Algorithm.DDA, colorDialog1.Color));
                    treeView1.Nodes[0].Nodes[2].Nodes.Add($"A: {A}, B:{B}, C:{C}, Alg: {(rb_Bresh.Checked == true ? "Bresenham" : "DDA")}, Cor: {colorDialog1.Color}");
                    treeView1.Nodes[0].Nodes[2].ExpandAll();
                }
                drag = false;
                inicial = Point.Empty;
                final = Point.Empty;
                Canvas.Invalidate();
            }
            else if (rb_Circulo.Checked)
            {
                if (drag && !inicial.IsEmpty && !final.IsEmpty)
                {
                    _circulos.Add(new Circulo(inicial, final, Radio(inicial, final), rb_Bresh.Checked == true ? Algorithm.bresenham : Algorithm.DDA, colorDialog1.Color));
                    treeView1.Nodes[0].Nodes[3].Nodes.Add($"Center: {inicial}, Raio: {Radio(inicial, final)},Alg: {(rb_Bresh.Checked == true ? "Bresenham" : "DDA")}, Cor: {colorDialog1.Color}");
                    treeView1.Nodes[0].Nodes[3].ExpandAll();
                }
                drag = false;
                inicial = Point.Empty;
                final = Point.Empty;
                Canvas.Invalidate();
            }

        }
    }
}
