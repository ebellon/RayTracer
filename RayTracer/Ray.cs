using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RayTracer
{
    public class Ray
    {
        public Vector3D Origin { get; set; }

        public Vector3D Direction { get; set; }

        public Ray(Vector3D origin, Vector3D direction)
        {
            this.Origin = origin;
            this.Direction = direction;
        }

        public Vector3D GetPosition(double offset)
        {
            return this.Origin + offset * this.Direction;
        }
    }
}
