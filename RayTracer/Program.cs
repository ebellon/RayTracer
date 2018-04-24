using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace RayTracer
{
    class Program
    {
        private const int Widht = 200;

        private const int Height = 100;

        static void Main()
        {
            var stopWatch = Stopwatch.StartNew();
            using (var output = new StreamWriter(@"d:\out.ppm", false))
            {

                WriteFileHeader(output);

                var lowerLeftCorner = new Vector3D(-2, -1, -1);
                var horizontalOffset = new Vector3D(4, 0, 0);
                var verticalOffset = new Vector3D(0, 2, 0);
                var origin = new Vector3D(0, 0, 0);


                List<IIntersect> world = new List<IIntersect> { new Sphere(new Vector3D(0, 0, -1), 0.5), new Sphere(new Vector3D(0, -100.5, -1), 100) };

                for (int y = Height - 1; y >= 0; y--)
                for (int x = 0; x < Widht; x++)
                {
                    double u = (double)x / Widht;
                    double v = (double)y / Height;
                    Ray ray = new Ray(origin, lowerLeftCorner + u * horizontalOffset + v * verticalOffset);


                    Color color = CalculatePointColor(ray, world);
                    WriteRgb(output, color.R, color.G, color.B);
                }
            }

            Console.WriteLine("Finished! Took (ms)" + stopWatch.ElapsedMilliseconds);
            Console.ReadKey();
        }

        private static void WriteFileHeader(StreamWriter file)
        {
            file.WriteLine("P3");
            file.WriteLine(Widht + " " + Height);
            file.WriteLine(255);
        }

        private static void WriteRgb(StreamWriter file, int R, int G, int B)
        {
            file.WriteLine(R + " " + G + " " + B);
        }

        private static Color CalculatePointColor(Ray ray, List<IIntersect> intersectables)
        {
            var minIntersect = double.MaxValue;
            Vector3D normalAtIntersection = new Vector3D(0, 0, 0);
            bool intersectionExists = false;

            foreach (var intersectable in intersectables)
            {
                var intersections = intersectable.GetIntersection(ray);

                if (intersections == null)
                {
                    continue;
                }

                foreach (var intersection in intersections)
                {
                    if (intersection < minIntersect)
                    {
                        intersectionExists = true;
                        minIntersect = intersection;
                        normalAtIntersection = intersectable.GetNormalAtIntersection(ray.GetPosition(minIntersect));
                    }
                }
            }

            if (intersectionExists)
            {
                Vector3D rgb = 255 * 0.5 * (normalAtIntersection + new Vector3D(1, 1, 1));
                return Color.FromArgb(1, (byte)rgb.X, (byte)rgb.Y, (byte)rgb.Z);
            }

            // Interpolated Y Background (1-t)*255 *(Desired RGB 1) + t*255 (Desired RGB 2). Simplified
            ray.Direction.Normalize();
            var t = (ray.Direction.Y + 1) * 0.5;
            return Color.FromArgb(1, (byte)(255 * (1 - 0.5 * t)), (byte)(255 * (1 - 0.3 * t)), 255);
        }
    }
}