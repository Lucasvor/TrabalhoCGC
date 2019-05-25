using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoCG1
{
    
    class Linha
    {
        private Point Ponto_inicial;
        private Point Ponto_final;

        public Linha(Point inicial, Point final,Algorithm algorithm, Color color)
        {
            Ponto_Inicial = inicial;
            Ponto_Final = final;
            Algorithm = algorithm;
            Color = color;
        }

        public Point Ponto_Inicial { get => Ponto_inicial; set => Ponto_inicial = value; }
        public Point Ponto_Final { get => Ponto_final; set => Ponto_final = value; }
        public Algorithm Algorithm { get; set; }
        public Color Color { get; set; }
    }
}
