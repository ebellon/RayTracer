using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace RayTracer
{
    public interface IIntersect
    {
        List<double> GetIntersection(Ray ray);

        Vector3D GetNormalAtIntersection(Vector3D intersectionPoint);
    }
}
