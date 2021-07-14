#region

using System.Drawing;

#endregion

namespace ArwaLED.Lib.LEDPrimitives
{
    public class LEDSquare : LEDPrimitive
    {
        private readonly Color _color;
        public LEDSquare(int height, int width, Color color)
        :base(height, width)
        {
            _color = color;
        }

        public override Color GetColor(int x, int y)
        {
            return _color;
        }
    }
}
