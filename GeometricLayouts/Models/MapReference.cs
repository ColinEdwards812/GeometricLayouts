using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GeometricLayouts.Models
{
    /// <summary>
    /// Holds a map reference e.g. A,11
    /// </summary>
    public struct MapReference
    { 
        private readonly char _letter;
        private readonly int _number;

        public MapReference(char letter, int number)
        {
            _letter = letter;
            _number = number;
        }
     
        public char Letter { get { return _letter; } }

        public int Number { get { return _number;} }

        public override string ToString()
        {
            return $"{_letter}{_number}";
        }
    }
}