#region

using System;
using System.Drawing;

#endregion

namespace ArwaLED.Lib.LEDPrimitives
{
    public class LEDClock : LEDPrimitive
    {
        protected Color HourHandColor;
        protected Color HourMarkColor;
        protected Color MinuteHandColor;
        protected Color SecondHandColor;

        public LEDClock(int height, Color? hourMarkColor = null, Color? hourHandColor = null,
            Color? minuteHandColor = null, Color? secondHandColor = null) : base(height,
            60)
        {
            HourMarkColor = hourMarkColor ?? Color.White;
            HourHandColor = hourHandColor ?? Color.Lime;
            MinuteHandColor = minuteHandColor ?? Color.Blue;
            SecondHandColor = secondHandColor ?? Color.Red;
        }

        public override void Render()
        {
            Clear();

            int hourpos = DateTime.Now.Hour % 12 * 5 + (DateTime.Now.Minute + 6) / 12;

            for (int y = 0; y < Height; y++)
            {
                Image[hourpos, y] = HourHandColor;
                if (hourpos == 0)
                    Image[59, y] = HourHandColor;
                else
                    Image[hourpos - 1, y] = HourHandColor;
                if (hourpos == 59)
                    Image[0, y] = HourHandColor;
                else
                    Image[hourpos + 1, y] = HourHandColor;

                for (int i = 0; i < 12; i++)
                    Image[i * 5, y] = HourMarkColor;

                Image[DateTime.Now.Minute, y] = MinuteHandColor;

                Image[DateTime.Now.Second, y] = SecondHandColor;
            }
        }
    }
}
