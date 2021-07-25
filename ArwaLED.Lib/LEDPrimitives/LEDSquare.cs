#region

using System.Drawing;

#endregion

namespace ArwaLED.Lib.LEDPrimitives
{
    public class LEDSquare : LEDPrimitive
    {
        private readonly Color _color;

        public LEDSquare(int height, int width, Color color)
            : base(height, width)
        {
            _color = color;
        }

        public override void Render()
        {
            for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                Image[x, y] = _color;
        }
    }
}
