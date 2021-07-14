#region

using System;
using System.Linq;
using rpi_ws281x;

#endregion

namespace ArwaLED.Lib
{
    public class LEDControl : IDisposable
    {
        private readonly int[] _LEDDisplayOffsets;
        private readonly LEDDisplay[] _LEDDisplays;
        private readonly WS281x _ws281x;

        public LEDControl(params LEDDisplay[] outDisplays)
        {
            _LEDDisplays = outDisplays;
            _LEDDisplayOffsets = new int[outDisplays.Length];
            _LEDDisplayOffsets[0] = 0;
            for (int i = 1; i < outDisplays.Length; i++)
                _LEDDisplayOffsets[i] =
                    _LEDDisplayOffsets[i - 1] + outDisplays[i - 1].Height + outDisplays[i - 1].Width;
            Settings settings = Settings.CreateDefaultSettings();
            settings.Channels[0] = new Channel(outDisplays.Sum(d => d.Height * d.Width), 18, 255, false,
                StripType.WS2812_STRIP);
            _ws281x = new WS281x(settings);
        }

        public void Dispose()
        {
            _ws281x.Dispose();
        }

        public void Render()
        {
            for (int i = 0; i < _LEDDisplays.Length; i++)
            for (int y = 0; y < _LEDDisplays[i].Height; y++)
            for (int x = 0; x < _LEDDisplays[i].Width; x++)
                _ws281x.SetLEDColor(0, _LEDDisplayOffsets[i] + y * _LEDDisplays[i].Height + x,
                    _LEDDisplays[i].LEDScene.GetColor(x, y));
            _ws281x.Render();
        }
    }
}
