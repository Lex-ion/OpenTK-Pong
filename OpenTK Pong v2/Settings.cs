using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OpenTK.Mathematics;


namespace OpenTK_Pong_v2
{
    internal class Settings
    {
        public static bool Sounds = true;
        public static Vector2 Score;


        public static void Beep(int frequency, int duration)
        {
            if (!Sounds)
                return;

            Console.Beep(frequency, duration);
        }
    }
}
