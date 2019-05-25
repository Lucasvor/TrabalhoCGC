using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoCG1
{
    class Circulo
    {
        private Point center;
        private Point Praio;
        private double raio;
        public Algorithm Algorithm;
        public Color Color;

        public Point Center { get => center; set => center = value; }
        public Point PRaio { get => Praio; set => Praio = value; }
        public double Raio { get => raio; set => raio = value; }

        public Circulo(Point center, Point pRaio, double raio, Algorithm algorithm, Color color)
        {
            Center = center;
            PRaio = pRaio;
            Raio = raio;
            Algorithm = algorithm;
            Color = color;
        }
    }
}
