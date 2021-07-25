#region

using System.Drawing;

#endregion

namespace ArwaLED.Lib.LEDPrimitives
{
    public abstract class LEDPrimitive
    {
        protected Color[,] Image;

        protected LEDPrimitive(int height, int width)
        {
            Height = height;
            Width = width;
            Image = new Color[width, height];
        }

        public int Height { get; }
        public int Width { get; }

        public Color GetColor(int x, int y)
        {
            return Image[x, y];
        }

        protected void Clear()
        {
            for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                Image[x, y] = Color.Black;
        }

        public abstract void Render();
    }
}
