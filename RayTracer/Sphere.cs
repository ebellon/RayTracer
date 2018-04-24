using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace RayTracer
{
    class Sphere : IIntersect
    {
        Vector3D Center { get; set; }

        double Radius { get; set; }

        public Sphere(Vector3D center, double radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public Vector3D? GetIntersection(Ray ray)
        {
            //Intersection Ray / Sphere
            //Sphere eq = dot((P - c)(P - c)) = RR (P = [x, y, z], c = [cx, cy, cz])
            //Intersection then: dot((ray - c)(ray - c)) = RR ->  dot((A + Bt -C)(A+Bt-C)) = RR
            //tt*dot(B,B) +2t *dot(B, A-C ) + dot(A-C, A-C) - RR = 0;

            var oc = ray.Origin - this.Center;
            var a = Vector3D.DotProduct(ray.Direction, ray.Direction);
            var b = 2 * Vector3D.DotProduct(oc, ray.Direction);
            var c = Vector3D.DotProduct(oc, oc) - this.Radius * this.Radius;

            var discriminant = b * b - 4 * a * c;

            // No solution!
            if (discriminant < 0)
            {
                return null;
            }

            //var posSolution = (-b + Math.Sqrt(discriminant) / 2 * a);
            var negSolution = (-b - Math.Sqrt(discriminant)) / 2 * a;

            return ray.GetPosition(negSolution);
        }


        public Vector3D GetNormalAtIntersection(Vector3D intersectionPoint)
        {
            var normal = (intersectionPoint - this.Center);
            normal.Normalize();

            return normal;
        }
    }
}
