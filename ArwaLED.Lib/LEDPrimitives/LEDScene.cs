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
        private readonly List<byte> _LEDPrimitiveAlphas;
        private readonly List<Point> _LEDPrimitivePosition;
        private readonly List<LEDPrimitive> _LEDPrimitives;

        public LEDScene(Color backgroundColor, int height, int width)
            : base(height, width)
        {
            _backgroundColor = backgroundColor;
            _LEDPrimitiveAlphas = new List<byte>();
            _LEDPrimitivePosition = new List<Point>();
            _LEDPrimitives = new List<LEDPrimitive>();
        }

        public Point[] LEDPrimitivePositions => _LEDPrimitivePosition.ToArray();

        public byte[] LEDPrimitiveAlpha => _LEDPrimitiveAlphas.ToArray();

        public LEDPrimitive[] LEDPrimitives => _LEDPrimitives.ToArray();

        private Color MixColor(Color back, Color top, byte alpha)
        {
            return Color.FromArgb(MixByte(back.R, top.R, alpha), MixByte(back.G, top.G, alpha),
                MixByte(back.B, top.B, alpha));
        }

        private byte MixByte(byte back, byte top, byte alpha)
        {
            return Convert.ToByte((top * (double) alpha + (double) back * (255 - alpha)) / 255);
        }

        public override Color GetColor(int x, int y)
        {
            Color c = _backgroundColor;
            for (int i = _LEDPrimitives.Count - 1; i > 0; i--)
                if (_LEDPrimitivePosition[i].X <= x && _LEDPrimitivePosition[i].Y <= y &&
                    _LEDPrimitivePosition[i].X + LEDPrimitives[i].Width - 1 >= x &&
                    _LEDPrimitivePosition[i].Y + LEDPrimitives[i].Height - 1 >= y
                )
                    c = MixColor(c,
                        _LEDPrimitives[i].GetColor(x - _LEDPrimitivePosition[i].X, y - _LEDPrimitivePosition[i].Y),
                        _LEDPrimitiveAlphas[i]);

            return c;
        }

        public void AddPrimitive(Point position, byte alpha, LEDPrimitive primitive, int index = 0)
        {
            if (index > _LEDPrimitives.Count)
                index = _LEDPrimitives.Count;
            _LEDPrimitives.Insert(index, primitive);
            _LEDPrimitivePosition.Insert(index, position);
            _LEDPrimitiveAlphas.Insert(index, alpha);
        }

        public void RemovePrimitive(int index)
        {
            _LEDPrimitives.RemoveAt(index);
            _LEDPrimitivePosition.RemoveAt(index);
            _LEDPrimitiveAlphas.RemoveAt(index);
        }
    }
}
