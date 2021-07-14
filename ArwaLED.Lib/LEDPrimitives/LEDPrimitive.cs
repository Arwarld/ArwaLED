#region

using System.Drawing;

#endregion

namespace ArwaLED.Lib.LEDPrimitives
{
    public abstract class LEDPrimitive
    {
        public LEDPrimitive(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public int Height { get; }
        public int Width { get; }

        public abstract Color GetColor(int x, int y);
    }
}
