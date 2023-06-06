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
        public static bool Sounds = false;
        public static Vector2 Score;

        public static int AIDif = 2;

        public static void Beep(int frequency, int duration)
        {
            if (!Sounds)
                return;

            Console.Beep(frequency, duration);
        }

        public static void Menu()
        {
            int Choice = 0;

           

            string[] Lines = {
               "  Zvuky  ",
               "Obtížnost",
               "  Zpět  "
            };

            Render();

            while (true)
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W or ConsoleKey.UpArrow:
                        if (Choice <= 0)
                        {
                            Choice = Lines.Length - 1;
                        }
                        else
                            Choice--;
                        new Thread(() => Settings.Beep(5000, 25)).Start();
                        Render();
                        break;

                    case ConsoleKey.S or ConsoleKey.DownArrow:
                        if (Choice >= Lines.Length - 1)
                        {
                            Choice = 0;
                        }
                        else
                            Choice++;
                        new Thread(() => Settings.Beep(5000, 25)).Start();
                        Render();
                        break;

                    case ConsoleKey.Enter:
                        Select();
                        Render();
                        break;
                }

            void Render()
            {
                Object[] Values = {Sounds, AIDif," "};

                Console.Clear();
                for (int i = 0; i < Lines.Length; i++)
                {
                    if (Choice == i)
                    {
                        Console.Write("[ ");
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(Lines[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ]");
                    }
                    else
                    {

                        Console.Write("  " + Lines[i]+"  ");
                    }

                    Console.WriteLine("  "+Values[i]);
                }
            }

            void Select()
            {
                switch (Choice)
                {
                    case 0:
                        Sounds = !Sounds;
                        break;
                    case 1:
                        if (AIDif >= 3)
                        {
                            AIDif = 0;
                        }
                        else
                            AIDif++;
                        break;


                    case 2:
                        Program.Menu();
                        break;

                }
            }
        }
    }
}
