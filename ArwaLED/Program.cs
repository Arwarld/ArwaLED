#region

using System;
using System.Drawing;
using System.Threading;
using ArwaLED.Lib;
using ArwaLED.Lib.LEDPrimitives;

#endregion

namespace ArwaLED
{
    internal static class Program
    {
        private static LEDControl _control;

        private static void Main()
        {
            Console.CancelKeyPress += Console_CancelKeyPress;
            Console.WriteLine("Hello World!");
            LEDDisplay matrix = new(16, 16);
            LEDDisplay strip = new(1, 60);

            matrix.LEDScene.AddPrimitive(new Point(0, 0), 255, new LEDSquare(16, 16, Color.Blue));
            strip.LEDScene.AddPrimitive(new Point(0, 0), 255, new LEDClock(60));

            _control = new LEDControl(matrix, strip);

            TimeSpan ticklength = new TimeSpan(0, 0, 0, 1, 0);

            while (true)
            {
                DateTime before = DateTime.Now;
                _control.Render();
                TimeSpan duration = DateTime.Now - before;
                if (duration < ticklength)
                    Thread.Sleep(ticklength - duration);
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            _control.AllBlack();
        }
    }
}
