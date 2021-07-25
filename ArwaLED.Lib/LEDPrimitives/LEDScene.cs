#region

using System;
using System.Collections.Generic;
using System.Drawing;

#endregion

namespace ArwaLED.Lib.LEDPrimitives
{
    public sealed class LEDScene : LEDPrimitive
    {
        private readonly Color _backgroundColor;
        private readonly List<byte> _ledPrimitiveAlphas;
        private readonly List<Point> _ledPrimitivePosition;
        private readonly List<LEDPrimitive> _ledPrimitives;

        public LEDScene(Color backgroundColor, int height, int width)
            : base(height, width)
        {
            _backgroundColor = backgroundColor;
            _ledPrimitiveAlphas = new List<byte>();
            _ledPrimitivePosition = new List<Point>();
            _ledPrimitives = new List<LEDPrimitive>();
        }

        public Point[] LEDPrimitivePositions => _ledPrimitivePosition.ToArray();

        public byte[] LEDPrimitiveAlpha => _ledPrimitiveAlphas.ToArray();

        public LEDPrimitive[] LEDPrimitives => _ledPrimitives.ToArray();

        private Color MixColor(Color back, Color top, byte alpha)
        {
            return Color.FromArgb(MixByte(back.R, top.R, alpha), MixByte(back.G, top.G, alpha),
                MixByte(back.B, top.B, alpha));
        }

        private byte MixByte(byte back, byte top, byte alpha)
        {
            return Convert.ToByte((top * (double) alpha + (double) back * (255 - alpha)) / 255);
        }

        public override void Render()
        {
            foreach (LEDPrimitive ledPrimitive in LEDPrimitives) ledPrimitive.Render();
            for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                Color c = _backgroundColor;
                for (int i = _ledPrimitives.Count - 1; i >= 0; i--)
                    if (_ledPrimitivePosition[i].X <= x && _ledPrimitivePosition[i].Y <= y &&
                        _ledPrimitivePosition[i].X + _ledPrimitives[i].Width - 1 >= x &&
                        _ledPrimitivePosition[i].Y + _ledPrimitives[i].Height - 1 >= y
                    )
                        c = MixColor(c,
                            _ledPrimitives[i].GetColor(x - _ledPrimitivePosition[i].X,
                                y - _ledPrimitivePosition[i].Y),
                            _ledPrimitiveAlphas[i]);

                Image[x, y] = c;
            }
        }

        public void AddPrimitive(Point position, byte alpha, LEDPrimitive primitive, int index = 0)
        {
            if (index > _ledPrimitives.Count)
                index = _ledPrimitives.Count;
            _ledPrimitives.Insert(index, primitive);
            _ledPrimitivePosition.Insert(index, position);
            _ledPrimitiveAlphas.Insert(index, alpha);
        }

        public void RemovePrimitive(int index)
        {
            _ledPrimitives.RemoveAt(index);
            _ledPrimitivePosition.RemoveAt(index);
            _ledPrimitiveAlphas.RemoveAt(index);
        }
    }
}
