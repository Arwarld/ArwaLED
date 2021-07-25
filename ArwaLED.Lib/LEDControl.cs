#region

using System;
using System.Device.Spi;
using System.Drawing;
using System.Linq;
using Iot.Device.Ws28xx;

#endregion

namespace ArwaLED.Lib
{
    public class LEDControl
    {
        private readonly int[] _ledDisplayOffsets;
        private readonly LEDDisplay[] _ledDisplays;
        private readonly Ws2812b _ws2812B;

        public LEDControl(params LEDDisplay[] outDisplays)
        {
            _ledDisplays = outDisplays;
            _ledDisplayOffsets = new int[outDisplays.Length];
            _ledDisplayOffsets[0] = 0;
            for (int i = 1; i < outDisplays.Length; i++)
                _ledDisplayOffsets[i] =
                    _ledDisplayOffsets[i - 1] + outDisplays[i - 1].Height * outDisplays[i - 1].Width;
            SpiConnectionSettings settings = new(0, 0)
            {
                ClockFrequency = 2_400_000,
                Mode = SpiMode.Mode0,
                DataBitLength = 8
            };

            SpiDevice dev = SpiDevice.Create(settings);
            _ws2812B = new Ws2812b(dev, outDisplays.Sum(d => d.Height * d.Width));
        }

        public void Render()
        {
            DateTime before = DateTime.Now;
            for (int i = 0; i < _ledDisplays.Length; i++)
            {
                _ledDisplays[i].LEDScene.Render();
                for (int y = 0; y < _ledDisplays[i].Height; y++)
                for (int x = 0; x < _ledDisplays[i].Width; x++)
                    _ws2812B.Image.SetPixel(_ledDisplayOffsets[i] + y * _ledDisplays[i].Height + x, 0,
                        _ledDisplays[i].LEDScene.GetColor(x, y));
            }

            _ws2812B.Update();
            Console.WriteLine("Update done - took {0}", DateTime.Now - before);
        }

        public void AllBlack()
        {
            for (int i = 0; i < _ws2812B.Image.Width; i++)
                _ws2812B.Image.SetPixel(i, 0, Color.Black);
            _ws2812B.Update();
        }
    }
}
