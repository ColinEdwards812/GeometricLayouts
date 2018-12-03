using GeometricLayouts.Models;

namespace GeometricLayouts.Interfaces
{
    public interface ITriangleMapCalculator
    {
        Triangle CalculateCoordinatesFromMapReference(MapReference mapReference);
        MapReference CalculateMapReferenceFromCoordinates(Triangle t);
        bool TriangleIsValid(Triangle t);
    }
}