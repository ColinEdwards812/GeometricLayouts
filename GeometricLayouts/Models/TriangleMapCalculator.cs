using GeometricLayouts.Interfaces;
using GeometricLayouts.Utility;
using System.Drawing;
using System.Linq;

namespace GeometricLayouts.Models
{
    public class TriangleMapCalculator : ITriangleMapCalculator
    {
        private const int WIDTH = 10;

        public MapReference CalculateMapReferenceFromCoordinates(Triangle t)
        {
            int xCharacter;
            int yNumber;

            //If Vertex 3 > 2 we have a left triangle
            //else a right reflected triangle
            if (t.Vertex3.X > t.Vertex2.X)
            {
                xCharacter = (t.Vertex1.Y / WIDTH) - 1;
                yNumber = (t.Vertex1.X / WIDTH);
                yNumber += yNumber + 1;
            }
            else
            {
                xCharacter = (t.Vertex1.Y / WIDTH);
                yNumber = (t.Vertex1.X / WIDTH);
                yNumber += yNumber;
            }

            char letter = CharacterMapping.ToMappedChar.First(v => v.Key == xCharacter).Value;

            return new MapReference(letter, yNumber);
        }

        public Triangle CalculateCoordinatesFromMapReference(MapReference mapReference)
        {
            int yOffset = CharacterMapping.ToMappedInt[mapReference.Letter];

            int xOffset = mapReference.Number % 2 == 0 
                ? mapReference.Number / 2 - 1
                : mapReference.Number / 2;

            //Create a square
            Point a = new Point(0, WIDTH);
            Point b = new Point(0, 0);
            Point c = new Point(WIDTH, WIDTH);
            Point d = new Point(WIDTH, 0);

            //if mapReference.Number is odd then we have a left triangle
            //if mapReference.Number is even then we have a right reflected triangle
            if (mapReference.Number % 2 != 0)
            {
                //Calculate offsets
                Triangle t = new Triangle(a, b, c);
                t.Offset(xOffset * WIDTH, yOffset * WIDTH);
                return t;
            }
            else
            {
                Triangle t = new Triangle(d, c, b);
                t.Offset(xOffset * WIDTH, yOffset * WIDTH);
                return t;
            }
        }

        public bool TriangleIsValid(Triangle t)
        {
            //If Vertex 3 > 2 we have a left triangle
            //else a right reflected triangle
            if (t.Vertex3.X > t.Vertex2.X)
            {
                bool isValid = (t.Vertex1.X == t.Vertex2.X)
                               && (t.Vertex1.X == t.Vertex3.X - 10)
                               && (t.Vertex1.Y == t.Vertex3.Y)
                               && (t.Vertex1.Y == t.Vertex2.Y + 10);

                return isValid;
            }
            else
            {
                bool isValid = (t.Vertex1.X == t.Vertex2.X)
                               && (t.Vertex1.X == t.Vertex3.X + 10)
                               && (t.Vertex1.Y == t.Vertex3.Y)
                               && (t.Vertex1.Y == t.Vertex2.Y - 10);

                return isValid;
            }
        }
    }
}
