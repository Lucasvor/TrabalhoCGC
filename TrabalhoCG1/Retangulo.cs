using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoCG1
{
    class Retangulo
    {
        private Point Ponto_topLeft;
        private Point Ponto_topRigth;
        private Point Ponto_bottomLeft;
        private Point Ponto_bottomRight;

        public Algorithm Algorithm { get; set; }
        public Color Color { get; set; }
        public Point Ponto_TopLeft { get => Ponto_topLeft; set => Ponto_topLeft = value; }
        public Point Ponto_TopRigth { get => Ponto_topRigth; set => Ponto_topRigth = value; }
        public Point Ponto_BottomLeft { get => Ponto_bottomLeft; set => Ponto_bottomLeft = value; }
        public Point Ponto_BottomRight { get => Ponto_bottomRight; set => Ponto_bottomRight = value; }

        public Retangulo(Point inicia, Point final, Algorithm algorithm,Color color)
        {
            Ponto_TopLeft = inicia;
            Ponto_TopRigth = new Point(inicia.X, final.Y);
            Ponto_BottomRight = final;
            Ponto_BottomLeft = new Point(final.X, inicia.Y);
            Algorithm = algorithm;
            Color = color;
        }
    }
}
