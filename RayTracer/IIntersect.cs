using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace RayTracer
{
    public interface IIntersect
    {
        Vector3D? GetIntersection(Ray ray);

        Vector3D GetNormalAtIntersection(Vector3D intersectionPoint);
    }
}
