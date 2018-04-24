﻿using System;
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

                Sphere sphere = new Sphere(new Vector3D(0,0,-1), 0.5);

                for (int y = Height -1; y >= 0; y--)
                    for (int x = 0; x < Widht; x++)
                    {
                        float u = (float)x / Widht;
                        float v = (float)y / Height;
                        Ray ray = new Ray(origin, lowerLeftCorner + u * horizontalOffset + v * verticalOffset);


                        Color color = CalculateRayColor(ray, sphere);
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
            file.WriteLine(R + " " + G +  " " + B);
        }

        private static Color CalculateRayColor(Ray ray, Sphere sphere)
        {
            Vector3D? intersection = sphere.GetIntersection(ray);

            int r, g, b;

            if (intersection != null)
            {
                var normalAtIntersection = sphere.GetNormalAtIntersection(intersection.Value);
                r = (int)(255 * 0.5 * (normalAtIntersection.X + 1));
                g = (int)(255 * 0.5 * (normalAtIntersection.Y + 1));
                b = (int)(255 * 0.5 * (normalAtIntersection.Z + 1));
            }
            else
            {
                ray.Direction.Normalize();
                var t = (ray.Direction.Y + 1) * 0.5;

                 r = (int)(255 * (1 - t) + 255 * t * 0.5);
                 g = (int)(255 * (1 - t) + 255 * t * 0.7);
                 b = (int)(255 * (1 - t) + 255 * t);
            }

            return Color.FromArgb(1, (byte)r, (byte)g, (byte)b);
        }
    }
}