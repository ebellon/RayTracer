using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RayTracer
{
    class Sphere
    {
        Vector3D Center { get; set; }

        double Radius { get; set; }

        public Sphere(Vector3D center, double radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public bool IntersectsRay(Ray ray)
        {
             /* Intersection Ray / Sphere
             * Sphere eq = dot((P - c)(P - c)) = RR (P = [x, y, z], c = [cx, cy, cz])
             * Intersection then: dot((ray - c)(ray - c)) = RR ->  dot((A + Bt -C)(A+Bt-C)) = RR
             * tt*dot(B,B) +2t *dot(B, A-C ) + dot(A-C, A-C) - RR = 0;
             */

            double a = Vector3D.DotProduct(ray.Direction, ray.Direction);
            double b =2* Vector3D.DotProduct(ray.Direction, ray.Origin - this.Center);
            double c = Vector3D.DotProduct(ray.Origin - this.Center, ray.Origin - this.Center) - this.Radius * this.Radius;

            double discriminant = b * b - 4 * a * c;

            if (discriminant >= 0)
                return true;

            return false;

        }

    }
}
