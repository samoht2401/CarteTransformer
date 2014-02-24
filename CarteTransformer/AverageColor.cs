using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CarteTransformer
{
    public class AverageColor
    {
        private const int diff = 2;

        public Color Color { get; protected set; }

        public AverageColor(Color col)
        {
            Color = col;
        }

        public override bool Equals(object obj)
        {
            if (obj is AverageColor)
            {
                AverageColor other = (AverageColor)obj;
                return Math.Abs(Color.A - other.Color.A) < diff &&
                    Math.Abs(Color.R - other.Color.R) < diff &&
                    Math.Abs(Color.G - other.Color.G) < diff &&
                    Math.Abs(Color.B - other.Color.B) < diff;
            }
            else if (obj is Color)
            {
                Color other = (Color)obj;
                return Math.Abs(Color.A - other.A) < diff &&
                    Math.Abs(Color.R - other.R) < diff &&
                    Math.Abs(Color.G - other.G) < diff &&
                    Math.Abs(Color.B - other.B) < diff;
            }
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Color.A << 24 + Color.R << 16 + Color.G << 8 + Color.B;
        }
    }
}
