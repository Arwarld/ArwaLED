#region

using System.Drawing;
using ArwaLED.Lib.LEDPrimitives;

#endregion

namespace ArwaLED.Lib
{
    public class LEDDisplay
    {
        public LEDDisplay(int height, int width)
        {
            Width = width;
            Height = height;
            LEDScene = new LEDScene(Color.Black, height, width);
        }

        public int Width { get; }
        public int Height { get; }
        public LEDScene LEDScene { get; }
    }
}
