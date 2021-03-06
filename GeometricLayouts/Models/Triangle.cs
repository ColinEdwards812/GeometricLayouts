﻿using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Newtonsoft.Json;

namespace GeometricLayouts.Models
{
    public class Triangle
    {
        private Point _vertex1;
        private Point _vertex2;
        private Point _vertex3;

        public Triangle(Point vertex1, Point vertex2, Point vertex3)
        {
            this._vertex1 = vertex1;
            this._vertex2 = vertex2;
            this._vertex3 = vertex3;
        }

        public Point Vertex1
        {
            get { return _vertex1; }
        }

        public Point Vertex2
        {
            get { return _vertex2; }
        }

        public Point Vertex3
        {
            get { return _vertex3; }
        }

        public void Offset(int x, int y)
        {
            _vertex1.Offset(x, y);
            _vertex2.Offset(x, y);
            _vertex3.Offset(x, y);
        }

        public bool IsIsoscelesRightTriangle()
        {
            //If vertex 3 > 2 we have a left triangle
            //else a right reflected triangle
            if (_vertex3.X > _vertex2.X)
            {
                bool isValid = (_vertex1.X == _vertex2.X)
                               && (_vertex1.X == _vertex3.X - 10)
                               && (_vertex1.Y == _vertex3.Y)
                               && (_vertex1.Y == _vertex2.Y + 10);

                return isValid;
            }
            else
            {
                bool isValid = (_vertex1.X == _vertex2.X)
                               && (_vertex1.X == _vertex3.X + 10)
                               && (_vertex1.Y == _vertex3.Y)
                               && (_vertex1.Y == _vertex2.Y - 10);

                return isValid;
            }
        }

    }
}