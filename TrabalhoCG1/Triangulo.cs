using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoCG1
{
    class Triangulo
    {
        private Point Ponto_a;
        private Point Ponto_b;
        private Point Ponto_c;

        public Triangulo(Point ponto_a, Point ponto_b, Point ponto_c, Algorithm algorithm, Color color)
        {
            Ponto_a = ponto_a;
            Ponto_b = ponto_b;
            Ponto_c = ponto_c;
            Algorithm = algorithm;
            Color = color;
        }

        public Algorithm Algorithm { get; set; }
        public Color Color { get; set; }
        public Point Ponto_A { get => Ponto_a; set => Ponto_a = value; }
        public Point Ponto_B { get => Ponto_b; set => Ponto_b = value; }
        public Point Ponto_C { get => Ponto_c; set => Ponto_c = value; }
    }
}
