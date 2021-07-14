using System;
using System.Drawing;
using ArwaLED.Lib;
using ArwaLED.Lib.LEDPrimitives;

namespace ArwaLED
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            LEDDisplay matrix = new LEDDisplay(16, 16);
            LEDDisplay strip = new LEDDisplay(1, 60);


            matrix.LEDScene.AddPrimitive(new Point(0,0),255,new LEDSquare(16,16,Color.Blue));
            strip.LEDScene.AddPrimitive(new Point(0, 0), 255, new LEDSquare(1, 60, Color.Red));


            LEDControl control = new LEDControl(matrix, strip);


            control.Render();
        }
    }
}
