using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometricLayouts.Utility
{
    public class CharacterMapping
    {
        public static readonly Dictionary<char, int> ToMappedInt =
            new Dictionary<char, int>()
                { {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4}, {'F', 5} };

        public static readonly Dictionary<int, char> ToMappedChar =
            new Dictionary<int, char>()
                { { 0,'A' }, {  1, 'B' }, { 2, 'C' }, { 3,'D' }, { 4,'E'}, { 5,'F' } };
    }
}
