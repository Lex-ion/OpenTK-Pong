namespace OpenTK_Pong_v2
{
    internal class Program
    {
        public static int Choice = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Menu();

            //  using (Game game = new Game(800, 800, "Test"))
            //  {
            //      game.Run();
            //  }

            



            //vypisuje připomenutí zkopírování shader
            #region JsemTrouba

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                if (i % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(' ');
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write(' ');
                }

            }
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("JESTLI SE DIVÍŠ PROČ TI NEFUNGUJOU SHADERY, TAK TO PROTOŽE SI JE NEZKOPÍROVAL DO DEBUG SLOŽKY ODKUD PROGRAM KOMPILUJE");

            for (int i = 0; i < Console.WindowWidth; i++)
            {
                if (i % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(' ');
                }else
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write(' ');
                }

            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ReadKey(true);
            #endregion 
            
        }

        public static void Menu()
        {
            string[] Lines = {
               "  Hra pro dva hráče  ",
               "Hra pro jednoho hráče",
               "      Nastavení      ",
               "      Jak hrát?      ",
               "       Ukončit       "
            };

            Render();

            while(true)
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.W or ConsoleKey.UpArrow:
                    if(Choice<=0)
                    {
                        Choice=Lines.Length-1;
                    }else
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
                
                Console.Clear();
                for (int i = 0; i < Lines.Length; i++)
                {
                    if (Choice == i)
                    {
                        Console.Write("[ ");
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(Lines[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" ]");
                    }
                    else
                    {
                       
                        Console.WriteLine("  "+Lines[i]);
                    }
                }
            }

            void Select()
            {
                switch (Choice)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Inicializace...");
                        using (Game game = new Game(800, 800, "Test"))
                        {
                            game.Run();
                        }
                        Console.WriteLine($"Hra ukončena! Skóre: {Settings.Score} \nStiskni klávesu pro návrat...");
                        Console.ReadKey();

                        break;

                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}