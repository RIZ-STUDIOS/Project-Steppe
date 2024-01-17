using UnityEngine;

namespace RicTools.Attributes
{
    public class MaxValueAttribute : PropertyAttribute
    {
        public readonly float X, Y, Z, W;

        public MaxValueAttribute(float value) : this(value, value, value, value)
        {

        }

        public MaxValueAttribute(float x, float y) : this(x, y, 0)
        {

        }

        public MaxValueAttribute(float x, float y, float z) : this(x, y, z, 0)
        {
        }

        public MaxValueAttribute(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }
    }
}
